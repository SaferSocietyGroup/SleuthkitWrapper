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
    Size = 352
#elif Bit64
    Size = 512
#endif
    )]
    public struct FFS_INFO
    {
        [FieldOffset(0)]
        TSK_FS_INFO fs_info;

#if Bit32
        [FieldOffset(280)]
#elif Bit64
        [FieldOffset(408)]
#endif
        IntPtr sb_ptr;

        internal TSK_FS_INFO tsk_fs_info
        {
            get
            {
                return fs_info;
            }
        }

        internal ffs_sb2 sb
        {
            get
            {
                return ((ffs_sb2)Marshal.PtrToStructure(sb_ptr, typeof(ffs_sb2)));
            }
        }
    }
}
