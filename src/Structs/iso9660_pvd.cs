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
        Size = 2052
#elif Bit64
        Size = 2056
#endif
    )]
    public struct iso9660_pvd
    {
        [FieldOffset(40)]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        String vol_id;

        public String VolumeName
        {
            get
            {
                return vol_id;
            }
        }
    }
}
