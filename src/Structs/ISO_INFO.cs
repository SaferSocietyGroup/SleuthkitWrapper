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
        Size = 304
#elif Bit64
        Size = 448
#endif
    )]
    public struct ISO_INFO
    {
        [FieldOffset(0)]
        TSK_FS_INFO fs_info;

#if Bit32
        [FieldOffset(288)]
#elif Bit64
        [FieldOffset(416)]
#endif
        IntPtr pvd_ptr;

        internal TSK_FS_INFO tsk_fs_info
        {
            get
            {
                return fs_info;
            }
        }

        public iso9660_pvd pvd
        {
            get
            {
                return ((iso9660_pvd)Marshal.PtrToStructure(pvd_ptr, typeof(iso9660_pvd)));                
            }
        }
    }
}
