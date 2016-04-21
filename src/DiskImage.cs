using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SleuthKit
{
    using SleuthKit.Structs;
    using System.Runtime.InteropServices;

    /// <summary>
    /// An object encapsulating the SleuthKit disk image functions. 
    /// See http://www.sleuthkit.org/sleuthkit/docs/api-docs/imgpage.html for more info.
    /// </summary>
    public class DiskImage : IDisposable, IContent
    {
        #region Fields

        internal DiskImageHandle _handle;
        internal TSK_IMG_INFO _struct;
        internal FileInfo[] files;


        #endregion

        #region Constructors
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="files"></param>
        public DiskImage(params FileInfo[] files)
        {
            Init(files);
        }

        /// <summary>
        /// Initializes a new disk image.
        /// </summary>
        /// <param name="file">The disk image (dd, e01, etc)</param>
        public DiskImage(FileInfo file)
        {
            Init(new FileInfo[] { file });
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_files"></param>
        private DiskImage(IEnumerable<FileInfo> _files)
        {
            Init(_files);
        }

        /// <summary>
        /// Initializes sleuthkit with the specified image files (calls tsk_img_open_* methods)
        /// </summary>
        /// <param name="_files"></param>
        private void Init(IEnumerable<FileInfo> _files)
        {
            if (_files == null)
            {
                throw new ArgumentException("file must not be null.");
            }

            if (_files.Any(_ => !_.Exists))
            {
                throw new ArgumentException("file must exist, cant just be a bogus path.");
            }

            this.files = _files.ToArray();
            var count = files.Length;

            if (count == 1)
            {
                this._handle = NativeMethods.tsk_img_open_sing(this.files[0].FullName, 0, 0);
            }
            else
            {
                var image_files_array = new IntPtr[this.files.Count()];

                // Each IntPtr array element will point to a copy of a 
                // string element in the openFileDialog.FileNames array.
                // based on answered provided in this link - http://stackoverflow.com/questions/8838455/how-to-convert-c-sharp-string-to-system-intptr
                for (int i = 0; i < this.files.Count(); i++)
                {
                    image_files_array[i] = Marshal.StringToCoTaskMemUni(this.files[i].FullName);
                }

                this._handle = NativeMethods.tsk_img_open(this.files.Count(), image_files_array, 0, 0);
            }

            if (!this._handle.IsInvalid)
            {
                this._struct = this._handle.GetStruct();
            }
            else
            {
                throw new InvalidOperationException("tsk_img_open didnt work right.");
            }
        }
        
        /// <summary>
        /// A little diagnostic info about the disk image
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var buf = new StringBuilder();
            buf.AppendFormat("imgType={0},size={1},files=", _struct.itype, _struct.size);
            buf.Append(string.Join(",", files.Select(_ => _.FullName).ToArray()));
            return buf.ToString();
        }
        #endregion

        #region Properties

        /// <summary>
        /// the size of this disk image, in bytes
        /// </summary>
        public long Size
        {
            get
            {
                return _struct.size;
            }
        }

        /// <summary>
        /// the size of a sector on this disk image, in bytes
        /// </summary>
        public long SectorSize
        {
            get
            {
                return _struct.sector_size;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// allows reading specified portions of the disk image
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="buf"></param>
        /// <param name="buflen"></param>
        /// <returns></returns>
        public int ReadBytes(long offset, byte[] buf, int buflen)
        {
            /*
            IntPtr br = NativeMethods.tsk_img_read(_handle, offset, buf, buflen);
            return br.ToInt32();
            //*/
            return NativeMethods.tsk_img_read(_handle, offset, buf, buflen);
        }

        /// <summary>
        /// Returns a stream to the disk image contents.  Particularly useful if you are reading something other than a dd image.  
        /// </summary>
        /// <returns></returns>
        public Stream OpenRead()
        {
            return new DiskImageStream(this);
        }

        /// <summary>
        /// Opens a volume system.
        /// </summary>
        /// <returns></returns>
        public VolumeSystem OpenVolumeSystem()
        {
            var vs = new VolumeSystem(this);
            if (vs._handle.IsInvalid)
            {
                vs._handle.Close();
                vs = null;
            }
            return vs;
        }

        public bool HasVolumes
        {
            get
            {
                using (VolumeSystem volumeSystem = OpenVolumeSystem())
                {
                    return (volumeSystem != null && volumeSystem.PartitionCount > 0 && volumeSystem.Volumes.Count() > 0);
                }
            }
        }

        public IEnumerable<VolumeInformation> Volumes
        {
            get
            {
                using (VolumeSystem volumeSystem = OpenVolumeSystem())
                {
                    if (null == volumeSystem) 
                    {
                        yield break;
                    }

                    foreach (Volume volume in volumeSystem.Volumes)
                    {
                        yield return new VolumeInformation() {
                            Address = volume.Address,
                            Description = volume.Description,
                            Flags = volume.Flags,
                            IsAllocated = volume.IsAllocated,
                            Length = volume.Length,
                            Offset = volume.Offset,
                            SectorLength = volume.SectorLength,
                            SectorOffset = volume.SectorOffset
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Opens a filesystem - for images with no partition table, i.e. volume images, or most SD cards and thumb drives that are formatted  .
        /// </summary>
        /// <param name="fstype"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public FileSystem OpenFileSystem(FileSystemType fileSystemType = FileSystemType.Autodetect, long offset = 0)
        {
            FileSystem fs = new FileSystem(this, fileSystemType, offset);

            if (fs._handle.IsInvalid)
            {
                fs._handle.Close();

                uint errorCode = NativeMethods.tsk_error_get_errno();
                IntPtr ptrToMessage = NativeMethods.tsk_error_get_errstr();
                String errorMessage = Marshal.PtrToStringAnsi(ptrToMessage);
                String ioExceptionMessage = String.Format("{0} (0x{1,8:X8})", errorMessage, errorCode);
                throw new IOException(ioExceptionMessage);
            }
            else
            {
                return fs;
            }
        }

        /// <summary>
        /// Returns an enumeration of all filesystems on the disk image.  Here for convenience when writing LINQ queries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FileSystem> GetFileSystems()
        {
            var vs = this.OpenVolumeSystem();
            if (vs != null && vs.PartitionCount > 0)
            {
                foreach (var v in vs.Volumes)
                {
                    var fs = v.OpenFileSystem();
                    yield return fs;
                }
            }
            else
            {
                var fs = this.OpenFileSystem();
                yield return fs;
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            this._handle.Dispose();
        }


        #endregion
    }
}
