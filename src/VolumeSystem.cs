using SleuthKit.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleuthKit
{
    /// <summary>
    /// Represents a volume system.  Volume systems organize contiguous sectors on a disk into volumes.
    /// See http://www.sleuthkit.org/sleuthkit/docs/api-docs/vspage.html for more details.
    /// </summary>
    public class VolumeSystem : IDisposable
    {
        internal VolumeSystemHandle _handle;
        internal TSK_VS_INFO _struct;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="image"></param>
        internal VolumeSystem(DiskImage image)
        {
            this.DiskImage = image;

            if (image == null)
            {
                throw new ArgumentException("image cannot be null.");
            }

            var ih = image._handle;
            this._handle = ih.OpenVolumeSystemHandle();
            this._struct = this._handle.GetStruct();
        }

        #region Properties
        
        /// <summary>
        /// The parent disk image
        /// </summary>
        public DiskImage DiskImage
        {
            get;
            private set;
        }

        /// <summary>
        /// Managed wrapper for TSK_VS_INFO.
        /// </summary>
        public TSK_VS_INFO VolumeSystemInfo
        {
            get
            {
                return this._struct;
            }
        }

        /// <summary>
        /// The type of volume system used
        /// </summary>
        public VolumeSystemType Type
        {
            get
            {
                return this._struct.Type;
            }
        }

        /// <summary>
        /// The number of partitions on this volume system
        /// </summary>
        public int PartitionCount
        {
            get
            {
                return this._struct.PartitionCount;
            }
        }

        /// <summary>
        /// The number of partitions on this volume system that are allocated 
        /// </summary>
        public int AllocatedPartitionCount
        {
            get
            {
                int apc =0;
                foreach (var fi in this._struct.VolumeInfos)
                {
                    if ((fi.Flags & VolumeFlags.Allocated)!=0)
                    {
                        apc++;
                    }
                }
                return apc;

            }
        }

        /// <summary>
        /// Block size used by this volume system
        /// </summary>
        public int BlockSize
        {
            get
            {
                return _struct.BlockSize;
            }
        }

        #endregion

        /// <summary>
        /// Gets volumes
        /// </summary>
        public IEnumerable<Volume> Volumes
        {
            get
            {
                var cur = this._struct.ptr_first_volumeinfo;
                while (cur != IntPtr.Zero)
                {
                    var v = new Volume(this, cur);
                    cur = v.VolumeInfo.ptr_next_part;
                    yield return v;
                }
            }
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            _handle.Dispose();
        }
    }
}
