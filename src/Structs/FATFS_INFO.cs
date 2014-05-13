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
        Size = 16832
#elif Bit64
        Size = 17008
#endif
    )]
    public struct FATFS_INFO
    {
        [FieldOffset(0)]
        TSK_FS_INFO fs_info;

        /// <summary>
        /// super block
        /// </summary>
#if Bit32
        [FieldOffset(16724)]
#elif Bit64
        [FieldOffset(16872)]
#endif
        IntPtr sb_ptr;


        internal TSK_FS_INFO tsk_fs_info
        {
            get
            {
                return fs_info;
            }
        }

        internal fatfs_sb sb
        {
            get
            {
                return ((fatfs_sb)Marshal.PtrToStructure(sb_ptr, typeof(fatfs_sb)));
            }
        }
    }
}
