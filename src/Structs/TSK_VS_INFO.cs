using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// represents TSK_VS_INFO.  Data structure used to store state and basic information for open volume systems.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_VS_INFO
    {
        /// <summary>
        /// Will be set to TSK_VS_INFO_TAG if structure is still allocated.
        /// </summary>
        int tag;

        /// <summary>
        /// Pointer to disk image that VS is in
        /// </summary>
        IntPtr ptr_imageInfo; //TSK_IMG_INFO *img_info; 

        /// <summary>
        /// Type of volume system / media management
        /// </summary>
        VolumeSystemType vstype;

        /// <summary>
        /// Byte offset where VS starts in disk image
        /// </summary>
        long offset;

        /// <summary>
        /// Size of blocks in bytes
        /// </summary>
        int block_size;

        /// <summary>
        /// Endian ordering of data
        /// </summary>
        Endianness endian;

        /// <summary>
        /// Linked list of partitions
        /// </summary>
        internal IntPtr ptr_first_volumeinfo;// TSK_VS_PART_INFO *part_list;    // 

        /// <summary>
        /// number of partitions 
        /// </summary>
        int part_count;

        //void (*close) (TSK_VS_INFO *);  // \internal Progs should call tsk_vs_close().
        IntPtr funcptr_close;

        /// <summary>
        /// The offset to the start of this volume system
        /// </summary>
        public long Offset
        {
            get
            {
                return offset;
            }
        }

        /// <summary>
        /// The type of volume system (MBR, APM, GPT, etc)
        /// </summary>
        public VolumeSystemType Type
        {
            get
            {
                return vstype;
            }
        }

        /// <summary>
        /// The endianness of this volume system (little, big, etc)
        /// </summary>
        public Endianness Endianness
        {
            get
            {
                return this.endian;
            }
        }

        /// <summary>
        /// The number of partitions on this volume system
        /// </summary>
        public int PartitionCount
        {
            get
            {
                return part_count;
            }
        }

        /// <summary>
        /// The number of blocks
        /// </summary>
        public int BlockSize
        {
            get
            {
                return block_size;
            }
        }

        /// <summary>
        /// First volume info
        /// </summary>
        internal TSK_VS_PART_INFO? FirstVolumeInfo
        {
            get
            {
                return TSK_VS_PART_INFO.FromIntPtr(this.ptr_first_volumeinfo);
            }
        }

        /// <summary>
        /// All volume infos
        /// </summary>
        internal IEnumerable<TSK_VS_PART_INFO> VolumeInfos
        {
            get
            {
                var first = this.FirstVolumeInfo;

                var cur = first;
                while (cur.HasValue)
                {
                    yield return cur.Value;
                    cur = cur.Value.Next;
                }
            }
        }
    }
}
