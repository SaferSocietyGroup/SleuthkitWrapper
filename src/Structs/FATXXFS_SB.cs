using System;
using System.Runtime.InteropServices;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 512)]
    public struct FATXXFS_SB
    {
        //offset must be on an adress divisible by the width of a word (4b on 32bit, and 8b on 64bit)
        [FieldOffset(40)] //really 43, but some shortcoming of marshlling in C# made this ugly hack nessecary
        private fatfs_vol_lab_16 vol_lab_f16;

        [FieldOffset(64)] //really 71, see above
        private fatfs_vol_lab_32 vol_lab_f32;

        internal String VolumeName16
        {
            get
            {
                return vol_lab_f16.Name;
            }
        }

        internal String VolumeName32
        {
            get
            {
                return vol_lab_f32.Name;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct fatfs_vol_lab_16
    {
        private byte padding1; //ugly hack
        private byte padding2; //ugly hack
        private byte padding3; //ugly hack

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 11)]
        private String vol_lab;

        public String Name
        {
            get
            {
                return vol_lab;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct fatfs_vol_lab_32
    {
        private byte padding1; //ugly hack
        private byte padding2; //ugly hack
        private byte padding3; //ugly hack
        private byte padding4; //ugly hack
        private byte padding5; //ugly hack
        private byte padding6; //ugly hack
        private byte padding7; //ugly hack

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 11)]
        private String vol_lab;

        public String Name
        {
            get
            {
                return vol_lab;
            }
        }
    }
}