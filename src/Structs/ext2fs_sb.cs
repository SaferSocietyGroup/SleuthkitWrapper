using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Sequential, Size = 1024)]
    public struct ext2fs_sb
    {
        uint s_inodes_count;      /* u32 */
        uint s_blocks_count;      /* u32 */
        uint s_r_blocks_count;
        uint s_free_blocks_count; /* u32 */
        uint s_free_inode_count;  /* u32 */
        uint s_first_data_block;  /* u32 */
        uint s_log_block_size;    /* u32 */
        int s_log_frag_size;     /* s32 */
        uint s_blocks_per_group;  /* u32 */
        uint s_frags_per_group;   /* u32 */
        uint s_inodes_per_group;  /* u32 */
        uint s_mtime;     /* u32 *//* mount time */
        uint s_wtime;     /* u32 *//* write time */
        ushort s_mnt_count; /* u16 *//* mount count */
        short s_max_mnt_count;     /* s16 */
        ushort s_magic;     /* u16 */
        ushort s_state;     /* u16 *//* fs state */
        ushort s_errors;    /* u16 */
        ushort s_minor_rev_level;   /* u16 */
        uint s_lastcheck; /* u32 */
        uint s_checkinterval;     /* u32 */
        uint s_creator_os;        /* u32 */
        uint s_rev_level; /* u32 */
        ushort s_def_resuid;        /* u16 */
        ushort s_def_resgid;        /* u16 */
        uint s_first_ino; /* u32 */
        ushort s_inode_size;        /* u16 */
        ushort s_block_group_nr;    /* u16 */
        uint s_feature_compat;    /* u32 */
        uint s_feature_incompat;  /* u32 */
        uint s_feature_ro_compat; /* u32 */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
        byte[] s_uuid;     /* u8[16] */

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
        String s_volume_name;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        String s_last_mounted;
        uint s_algorithm_usage_bitmap;    /* u32 */
        byte s_prealloc_blocks;      /* u8 */
        byte s_prealloc_dir_blocks;  /* u8 */
        ushort pad_or_gdt;

        /* Valid if EXT2_FEATURE_COMPAT_HAS_JOURNAL */
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
        byte[] s_journal_uuid;     /* u8[16] */
        uint s_journal_inum;      /* u32 */
        uint s_journal_dev;       /* u32 */
        uint s_last_orphan;       /* u32 */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
        byte[] s_hash_seed;        /* u32 */
        byte s_def_hash_version;     /* u8 */
        byte s_jnl_backup_type;      /* u8 */
        ushort s_desc_size; /* u16 */
        uint s_default_mount_opts;        /* u32 */
        uint s_first_meta_bg;     /* u32 */
        uint s_mkfs_time; /* u32 */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 17)]
        uint[] s_jnl_blocks;   /* u32[17] */

        /* Valid if EXT4_FEATURE_INCOMPAT_64BIT*/
        uint s_blocks_count_hi;   /* u32 */
        uint s_r_blocks_count_hi; /* u32 */
        uint s_free_blocks_count_hi;      /* u32 */
        ushort s_min_extra_isize;   /* u16 */
        ushort s_want_extra_isize;  /* u16 */
        uint s_flags;     /* u32 */
        ushort s_raid_stride;       /* u16 */
        ushort s_mmp_interval;      /* u16 */
        ulong s_mmp_block; /* u64 */
        uint s_raid_stripe_width; /* u32 */
        byte s_log_groups_per_flex;  /* u8 */
        byte s_reserved_char_pad;    /* u8 */
        ushort s_reserved_pad;      /* u16 */
        ulong s_kbytes_written;    /* u64 */
        uint s_snapshot_inum;     /* u32 */
        uint s_snapshot_id;       /* u32 */
        ulong s_snapshot_r_blocks_count;   /* u64 */
        uint s_snapshot_list;     /* u32 */
        uint s_error_count;       /* u32 */
        uint s_first_error_time;  /* u32 */
        uint s_first_error_ino;   /* u32 */
        ulong s_first_error_block; /* u64 */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32)]
        byte[] s_first_error_func; /* u8[32] */
        uint s_first_error_line;  /* u32 */
        uint s_last_error_time;   /* u32 */
        uint s_last_error_ino;    /* u32 */
        uint s_last_error_line;   /* u32 */
        ulong s_last_error_block;  /* u64 */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32)]
        byte[] s_last_error_func;  /* u8[32] */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64)]
        byte[] s_mount_opts;       /* u8[64] */
        uint s_usr_quota_inum;    /* u32 */
        uint s_grp_quota_inum;    /* u32 */
        uint s_overhead_clusters; /* u32 */

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 109)]
        uint[] s_padding;

        internal String VolumeName
        {
            get
            {
                return s_volume_name;
            }
        }
    }
}
