using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 1536)]
    public struct ffs_sb2
    {
        [FieldOffset(680)]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        String volname;

        public String VolumeName
        {
            get
            {
                return volname;
            }
        }
    }
}
