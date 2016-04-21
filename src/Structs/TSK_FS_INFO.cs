using System;
using System.Runtime.InteropServices;

namespace SleuthKit.Structs
{
    /// <summary>
    /// struct TSK_FS_INFO
    /// </summary>
    [StructLayout(LayoutKind.Explicit,
#if Bit32
    Size = 280
#elif Bit64
    Size = 408
#endif
)]
    public struct TSK_FS_INFO
    {
        /// <summary>
        /// tag, can be used to validate that we have the right kind of struct.  a magic header for the struct, essentially.
        /// </summary>
#if Bit32
        [FieldOffset(0)]
#elif Bit64
        [FieldOffset(0)]
#endif
        private StructureMagic tag;

        /// <summary>
        /// pointer to imageinfo struct
        /// </summary>
#if Bit32
        [FieldOffset(4)]
#elif Bit64
        [FieldOffset(8)]
#endif
        private IntPtr img_info_ptr;

        /// <summary>
        /// TSK_OFF_T offset, Byte offset into img_info that fs starts
        /// </summary>
#if Bit32
        [FieldOffset(8)]
#elif Bit64
        [FieldOffset(16)]
#endif
        private long offset;

        /* meta data */

        /// <summary>
        /// number of metadata addresses
        /// </summary>
#if Bit32
        [FieldOffset(16)]
#elif Bit64
        [FieldOffset(24)]
#endif
        internal long inum_count;

        /// <summary>
        /// address of root directory
        /// </summary>
#if Bit32
        [FieldOffset(24)]
#elif Bit64
        [FieldOffset(32)]
#endif
        internal long root_inum;

        /// <summary>
        /// address of first metadata
        /// </summary>
#if Bit32
        [FieldOffset(32)]
#elif Bit64
        [FieldOffset(40)]
#endif
        internal long first_inum;

        /// <summary>
        /// address of last metadata
        /// </summary>
#if Bit32
        [FieldOffset(40)]
#elif Bit64
        [FieldOffset(48)]
#endif
        internal long last_inum;

        /* content */

        /// <summary>
        /// Number of blocks in fs
        /// </summary>
#if Bit32
        [FieldOffset(48)]
#elif Bit64
        [FieldOffset(56)]
#endif
        internal long block_count;

        /// <summary>
        /// Address of first block
        /// </summary>
#if Bit32
        [FieldOffset(56)]
#elif Bit64
        [FieldOffset(64)]
#endif
        internal long first_block;

        /// <summary>
        /// Address of last block as reported by file system (could be larger than last_block in image if end of image does not exist)
        /// </summary>
#if Bit32
        [FieldOffset(64)]
#elif Bit64
        [FieldOffset(72)]
#endif
        internal long last_block;

        /// <summary>
        /// Address of last block -- adjusted so that it is equal to the last block in the image or volume (if image is not complete)
        /// </summary>
#if Bit32
        [FieldOffset(72)]
#elif Bit64
        [FieldOffset(80)]
#endif
        internal long last_block_act;

        /// <summary>
        /// Size of each block (in bytes)
        /// </summary>
#if Bit32
        [FieldOffset(80)]
#elif Bit64
        [FieldOffset(88)]
#endif
        internal int block_size;

        /// <summary>
        /// Size of device block (typically always 512)
        /// </summary>
#if Bit32
        [FieldOffset(84)]
#elif Bit64
        [FieldOffset(92)]
#endif
        internal int dev_bsize;

        /// <summary>
        /// Number of bytes that preceed each block (currently only used for RAW CDs)
        /// </summary>
#if Bit32
        [FieldOffset(88)]
#elif Bit64
        [FieldOffset(96)]
#endif
        internal uint block_pre_size;

        /// <summary>
        /// Number of bytes that follow each block (currently only used for RAW CDs)
        /// </summary>
#if Bit32
        [FieldOffset(92)]
#elif Bit64
        [FieldOffset(100)]
#endif
        internal uint block_post_size;

        /// <summary>
        /// Address of journal inode
        /// </summary>
#if Bit32
        [FieldOffset(96)]
#elif Bit64
        [FieldOffset(104)]
#endif
        private ulong journ_inum;

        /// <summary>
        /// type of file system
        /// </summary>
#if Bit32
        [FieldOffset(104)]
#elif Bit64
        [FieldOffset(112)]
#endif
        private FileSystemType ftype;

        /// <summary>
        /// string "name" of data unit type
        /// </summary>
#if Bit32
        [FieldOffset(108)]
#elif Bit64
        [FieldOffset(120)]
#endif
        private IntPtr duname_ptr;

        /// <summary>
        /// flags
        /// </summary>
#if Bit32
        [FieldOffset(112)]
#elif Bit64
        [FieldOffset(128)]
#endif
        private FilesystemInfoFlag flags;

        /// <summary>
        /// File system id (as reported in boot sector)
        /// </summary>
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
#if Bit32
        [FieldOffset(116)]
#elif Bit64
        [FieldOffset(132)]
#endif
        private IntPtr fs_id_ptr; //uint8_t[] fs_id;

        /// <summary>
        /// fs id used
        /// </summary>
#if Bit32
        [FieldOffset(148)]
#elif Bit64
        [FieldOffset(168)]
#endif
        private UIntPtr fs_id_used;

#if Bit32
        [FieldOffset(152)]
#elif Bit64
        [FieldOffset(176)]
#endif
        private Endianness endian;

        /// <summary>
        /// taken when r/w the list_inum_named list
        /// </summary>
#if Bit32
        [FieldOffset(156)]
#elif Bit64
        [FieldOffset(184)]
#endif
        private tsk_lock_t list_inum_named_lock;

        /// <summary>
        /// List of unallocated inodes that
        /// </summary>
#if Bit32
        [FieldOffset(180)]
#elif Bit64
        [FieldOffset(224)]
#endif
        private IntPtr list_inum_named_ptr; //TSK_LIST *list_inum_named;

        /// <summary>
        /// taken for the duration of orphan hunting (not just when updating orphan_dir)
        /// </summary>
#if Bit32
        [FieldOffset(184)]
#elif Bit64
        [FieldOffset(232)]
#endif
        private tsk_lock_t orphan_dir_lock;

        /// <summary>
        /// Files and dirs in the top level of the $OrphanFiles directory.  NULL if orphans have not been hunted for yet.
        /// </summary>
#if Bit32
        [FieldOffset(208)]
#elif Bit64
        [FieldOffset(272)]
#endif
        private IntPtr orphan_dir_ptr; //TSK_FS_DIR *orphan_dir;

        #region methods

#if Bit32
        [FieldOffset(212)]
#elif Bit64
        [FieldOffset(280)]
#endif
        private IntPtr block_walk_ptr;

#if Bit32
        [FieldOffset(216)]
#elif Bit64
        [FieldOffset(288)]
#endif
        private IntPtr block_getflags_ptr;

#if Bit32
        [FieldOffset(220)]
#elif Bit64
        [FieldOffset(296)]
#endif
        private IntPtr inode_Walk_ptr;

#if Bit32
        [FieldOffset(224)]
#elif Bit64
        [FieldOffset(304)]
#endif
        private IntPtr file_add_meta_ptr;

#if Bit32
        [FieldOffset(228)]
#elif Bit64
        [FieldOffset(312)]
#endif
        private IntPtr get_default_attr_type_ptr;

#if Bit32
        [FieldOffset(232)]
#elif Bit64
        [FieldOffset(320)]
#endif
        private IntPtr load_attrs_ptr;

#if Bit32
        [FieldOffset(236)]
#elif Bit64
        [FieldOffset(328)]
#endif
        private IntPtr istat_ptr;

#if Bit32
        [FieldOffset(240)]
#elif Bit64
        [FieldOffset(336)]
#endif
        private IntPtr dir_open_meta_ptr;

#if Bit32
        [FieldOffset(244)]
#elif Bit64
        [FieldOffset(344)]
#endif
        private IntPtr jopen_ptr;

#if Bit32
        [FieldOffset(248)]
#elif Bit64
        [FieldOffset(352)]
#endif
        private IntPtr jblk_walk_ptr;

#if Bit32
        [FieldOffset(252)]
#elif Bit64
        [FieldOffset(360)]
#endif
        private IntPtr jentry_walk_ptr;

#if Bit32
        [FieldOffset(256)]
#elif Bit64
        [FieldOffset(368)]
#endif
        private IntPtr fsstat_ptr;

        /*
        public fsstatDelegate fsstat;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate byte fsstatDelegate(IntPtr fs, FILE* hFile);
        //*/

#if Bit32
        [FieldOffset(260)]
#elif Bit64
        [FieldOffset(376)]
#endif
        private IntPtr name_cmp_ptr;

#if Bit32
        [FieldOffset(264)]
#elif Bit64
        [FieldOffset(384)]
#endif
        private IntPtr fscheck_ptr;

#if Bit32
        [FieldOffset(268)]
#elif Bit64
        [FieldOffset(392)]
#endif
        private IntPtr close_ptr;

#if Bit32
        [FieldOffset(272)]
#elif Bit64
        [FieldOffset(400)]
#endif
        private IntPtr fread_owner_sid_ptr;

        #endregion methods

        /// <summary>
        /// Returns nothing at the moment
        /// </summary>
        // TODO: get the ID working.
        public string ID
        {
            get
            {
                string ret = null;
                //if (ptr_fs_id != IntPtr.Zero && fs_id_used!=UIntPtr.Zero)
                //{
                //    int btr = (int)fs_id_used.ToUInt32();
                //    byte[] buf = new byte[btr];
                //    Marshal.Copy(ptr_fs_id, buf, 0, btr);
                //    ret = Encoding.ASCII.GetString(buf, 0, btr);

                //}
                return ret;
            }
        }

        /// <summary>
        /// validates the tag contains the proper constant
        /// </summary>
        public bool AppearsValid
        {
            get
            {
                return this.tag == StructureMagic.FilesystemInfoTag;
            }
        }

        /// <summary>
        /// Image information
        /// </summary>
        public TSK_IMG_INFO ImageInfo
        {
            get
            {
                var ret = new TSK_IMG_INFO();
                if (img_info_ptr != IntPtr.Zero)
                {
                    ret = (TSK_IMG_INFO)Marshal.PtrToStructure(img_info_ptr, typeof(TSK_IMG_INFO));
                }
                return ret;
            }
        }

        /// <summary>
        /// the offset where the filesystem starts
        /// </summary>
        public long Offset
        {
            get
            {
                return this.offset;
            }
        }

        /// <summary>
        /// the type of filesystem
        /// </summary>
        public FileSystemType FilesystemType
        {
            get
            {
                return this.ftype;
            }
        }

        public Endianness Endian
        {
            get
            {
                return endian;
            }
        }
    }
}
