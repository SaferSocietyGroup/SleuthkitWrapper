using SleuthKit.Structs;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SleuthKit
{
    /// <summary>
    /// Used to contain common properties of File and Directory.
    /// </summary>
    public class FileSystemEntry
    {
        internal static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        #region Properties

        /// <summary>
        /// Size of file content, in bytes
        /// </summary>
        public long Size
        {
            get;
            internal set;
        }

        /// <summary>
        /// Filesystem metadata last write time
        /// </summary>
        public DateTime MetadataWriteTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// File content last write time
        /// </summary>
        public DateTime LastWriteTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// File content last access time
        /// </summary>
        public DateTime LastAccessTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// File creation time
        /// </summary>
        public DateTime CreationTime
        {
            get;
            internal set;
        }

        /// <summary>
        /// Handle to the filesystem that this file comes from
        /// </summary>
        public FileSystem FileSystem
        {
            get;
            internal set;
        }

        /// <summary>
        /// The filename
        /// </summary>
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// The type
        /// </summary>
        public MetadataType Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// The filesystem identifier, or inode of this file
        /// </summary>
        public long Address
        {
            get;
            internal set;
        }

        #endregion
    }

    /// <summary>
    /// A file from a filesystem, roughly equates to a TSK_FS_FILE.
    /// </summary>
    public class File : FileSystemEntry, IDisposable, IContent
    {
        #region Fields
        private FileSystem _fs;
        private FileHandle _handle;
        private TSK_FS_FILE _struct;
        private Directory _parentDir;
        private string path;
        #endregion

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="fh"></param>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        internal File(FileSystem fs, FileHandle fh, Directory parent, string name)
        {
            this._fs = fs;
            this._handle = fh;
            this._struct = fh.GetStruct();
            this._parentDir = parent;

            if (name == null)
            {
                if (_struct.Name.HasValue)
                {
                    this.Name = _struct.Name.Value.GetName();
                }
            }
            else
            {
                this.Name = name;
            }

            if (_struct.Metadata.HasValue)
            {
                var m = _struct.Metadata.Value;

                this.Address = m.Address;

                this.Type = m.type;
                this.Size = m.Size;
                this.CreationTime = epoch.AddSeconds(m.CRTime);
                this.LastWriteTime = epoch.AddSeconds(m.MTime);
                this.LastAccessTime = epoch.AddSeconds(m.ATime);
                this.MetadataWriteTime = epoch.AddSeconds(m.CTime);
            }

            this.FileSystem = this._fs;

        }

        #region Properties

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            this._handle.Dispose();
        }


        /// <summary>
        /// Long name of the file - use this
        /// </summary>
        public string LongName
        {
            get
            {
                return this._struct.Name.GetValueOrDefault().LongName;
            }
        }

        /// <summary>
        /// The underlying TSK_FS_FILE.
        /// </summary>
        public TSK_FS_FILE FileStruct
        {
            get
            {
                return this._struct;
            }
        }

        /// <summary>
        /// The full path
        /// </summary>
        public string Path
        {
            get
            {
                if (path == null)
                {
                    var buf = new StringBuilder();

                    if (_parentDir != null)
                    {
                        var pp = _parentDir.Path;
                        buf.Append(pp);
                        buf.Append("/");
                    }
                    buf.Append(this.Name);

                    path = buf.ToString();
                }

                return path;
            }
        }

        #endregion

        /// <summary>
        /// Reads file data
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="amt">The amt.</param>
        /// <returns></returns>
        /// <exception cref="SleuthKit.NtfsCompressionException">If a compressed file is corrupt</exception>
        /// <exception cref="System.IO.IOException">If the file reading files for another reason</exception>
        public int ReadBytes(long offset, byte[] buffer, int amt)
        {
            int bytesRead = NativeMethods.tsk_fs_file_read(this._handle, offset, buffer, amt, FileReadFlag.None);

            //On error, check for EOF
            if (bytesRead == -1)
            {
                uint errorCode = NativeMethods.tsk_error_get_errno();
                if (errorCode == 0x08000005)
                {
                    return 0; //EOF reached
                }
                else
                {
                    IntPtr ptrToMessage = NativeMethods.tsk_error_get_errstr();
                    String errorMessage = Marshal.PtrToStringAnsi(ptrToMessage);
                    String ioExceptionMessage = String.Format("{0} (0x{1,8:X8})", errorMessage, errorCode);
                    if (errorMessage.Contains("ntfs_uncompress_compunit"))
                        throw new NtfsCompressionException(String.Format("The compressed NTFS file was corrupt and could not be decompressed. Full error: {0}", ioExceptionMessage));
                    else
                        throw new IOException(ioExceptionMessage);
                }
            }

            return bytesRead;
        }

        /// <summary>
        /// Process a file and call a callback function with the file contents. 
        /// </summary>
        /// <param name="callback">Callback action to call with content.</param>
        /// <param name="a_ptr">Pointer that will be passed to callback.</param>
        /// <returns>returns 1 on error and 0 on success.</returns>
        public bool WalkFile(FileContentWalkDelegate callback, IntPtr a_ptr)
        {
            var ret = NativeMethods.tsk_fs_file_walk(this._handle, 0, callback, a_ptr);
            return ret == 0;
        }

        /// <summary>
        /// Returns a stream to this file's contents.
        /// </summary>
        /// <returns></returns>
        public System.IO.Stream OpenRead()
        {
            return new FileStream(this);
        }
    }
}