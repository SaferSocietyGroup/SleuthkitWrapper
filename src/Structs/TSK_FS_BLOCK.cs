using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// struct TSK_FS_BLOCK
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_FS_BLOCK
    {
        /// <summary>
        /// internal Will be set to TSK_FS_BLOCK_TAG if structure is valid / allocated 
        /// </summary>
        internal StructureMagic tag;
        /// <summary>
        ///Pointer to file system that block is from
        /// </summary>
        internal IntPtr ptr_fs_info; //TSK_FS_INFO *fs_info;   

        /// <summary>
        ///  Buffer with block data (of size TSK_FS_INFO::block_size)
        /// </summary>
        internal IntPtr ptr_block_data; //char *buf;  

        /// <summary>
        /// Address of block
        /// </summary>
        internal long addr; //TSK_DADDR_T addr;       

        /// <summary>
        /// Flags for block (alloc or unalloc)
        /// </summary>
        internal FileSystemBlockFlags flags;

        /// <summary>
        /// validates the tag contains the proper constant
        /// </summary>
        public bool AppearsValid
        {
            get
            {
                return tag == StructureMagic.FilesystemBlockTag;
            }
        }

    }
}
