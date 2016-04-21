using System.Runtime.InteropServices;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Explicit,
#if Bit32
        Size = 17424
#elif Bit64
        Size = 17616
#endif
    )]
    public struct FATFS_INFO
    {
        [FieldOffset(0)]
        private TSK_FS_INFO fs_info;

        /// <summary>
        /// super block
        /// </summary>
#if Bit32
        [FieldOffset(16860)]
#elif Bit64
        [FieldOffset(17024)]
#endif
        //private IntPtr sb_ptr;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 512)]
        private byte[] boot_sector_buffer;

        internal TSK_FS_INFO tsk_fs_info
        {
            get
            {
                return fs_info;
            }
        }

        internal FATXXFS_SB sb
        {
            get
            {
                GCHandle handle = GCHandle.Alloc(boot_sector_buffer, GCHandleType.Pinned);
                FATXXFS_SB superblock = (FATXXFS_SB)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(FATXXFS_SB));
                handle.Free();
                return superblock;
            }
        }
    }
}
