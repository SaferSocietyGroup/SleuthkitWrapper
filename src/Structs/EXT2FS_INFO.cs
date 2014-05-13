using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Explicit,
#if Bit32
    Size = 400
#elif Bit64
    Size = 560
#endif
)]
    public struct EXT2FS_INFO
    {
        /// <summary>
        /// super class
        /// </summary>
        [FieldOffset(0)]
        TSK_FS_INFO fs_info;

        /// <summary>
        /// super block
        /// </summary>
#if Bit32
        [FieldOffset(280)]
#elif Bit64
        [FieldOffset(408)]
#endif
        IntPtr fs_ptr;

        /// <summary>
        /// lock protects grp_buf, grp_num, bmap_buf, bmap_grp_num, imap_buf, imap_grp_num
        /// </summary>
#if Bit32
        [FieldOffset(284)]
#elif Bit64
        [FieldOffset(416)]
#endif
        tsk_lock_t s_lock;

#if Bit32
        [FieldOffset(308)]
#elif Bit64
        [FieldOffset(456)]
#endif
        IntPtr v_grp_buf_ptr;


#if Bit32
        [FieldOffset(312)]
#elif Bit64
        [FieldOffset(464)]
#endif
        IntPtr ext4_grp_buf_ptr;

        /// <summary>
        /// cached group descriptor r/w shared - lock
        /// </summary>
#if Bit32
        [FieldOffset(316)]
#elif Bit64
        [FieldOffset(472)]
#endif
        IntPtr grp_buf_ptr;

        /// <summary>
        /// cached group number r/w shared - lock
        /// </summary>
#if Bit32
        [FieldOffset(320)]
#elif Bit64
        [FieldOffset(480)]
#endif
        ulong grp_num;

        /// <summary>
        /// cached block allocation bitmap r/w shared - lock
        /// </summary>
#if Bit32
        [FieldOffset(328)]
#elif Bit64
        [FieldOffset(488)]
#endif
        IntPtr bmap_buf_ptr;

        /// <summary>
        /// cached block bitmap nr r/w shared - lock
        /// </summary>
#if Bit32
        [FieldOffset(336)]
#elif Bit64
        [FieldOffset(496)]
#endif
        ulong bmap_grp_num;

        /// <summary>
        /// cached inode allocation bitmap r/w shared - lock
        /// </summary>
#if Bit32
        [FieldOffset(344)]
#elif Bit64
        [FieldOffset(504)]
#endif
        IntPtr imap_buf_ptr;

        /// <summary>
        /// cached inode bitmap nr r/w shared - lock
        /// </summary>
#if Bit32
        [FieldOffset(352)]
#elif Bit64
        [FieldOffset(512)]
#endif
        ulong imap_grp_num;

        /// <summary>
        /// offset to first group desc
        /// </summary>
#if Bit32
        [FieldOffset(360)]
#elif Bit64
        [FieldOffset(520)]
#endif
        long groups_offset;

        /// <summary>
        /// nr of descriptor group blocks
        /// </summary>
#if Bit32
        [FieldOffset(368)]
#elif Bit64
        [FieldOffset(528)]
#endif
        ulong groups_count;

        /// <summary>
        /// v1 or v2 of dentry
        /// </summary>
#if Bit32
        [FieldOffset(376)]
#elif Bit64
        [FieldOffset(536)]
#endif
        byte deentry_type;

        /// <summary>
        /// size of each inode
        /// </summary>
#if Bit32
        [FieldOffset(378)]
#elif Bit64
        [FieldOffset(538)]
#endif
        short inode_size;

    
#if Bit32
        [FieldOffset(384)]
#elif Bit64
        [FieldOffset(544)]
#endif
        ulong first_data_block;

    
#if Bit32
        [FieldOffset(392)]
#elif Bit64
        [FieldOffset(552)]
#endif
        IntPtr jinfo_ptr;


        internal TSK_FS_INFO tsk_fs_info
        {
            get
            {
                return fs_info;
            }
        }

        internal ext2fs_sb fs
        {
            get
            {
                return ((ext2fs_sb)Marshal.PtrToStructure(fs_ptr, typeof(ext2fs_sb)));
            }
        }
    }
}
