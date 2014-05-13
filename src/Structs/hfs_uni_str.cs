using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    public struct hfs_uni_str
    {
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
        byte[] length;

        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 510)]
        byte[] unicode;

        public String GetString(Endianness endian)
        {
            if (endian == Endianness.Little)
            {
                int size = 256 * length[1] + length[0];
                return UnicodeEncoding.Unicode.GetString(unicode.Take(size).ToArray());
            }
            else
            {
                int size = 256 * length[0] + length[1];

                List<byte> data = new List<byte>();

                for (int i = 0; i < size; i++)
                {
                    data.Add(unicode[2 * i + 1]);
                    data.Add(unicode[2 * i]);
                }

                return UnicodeEncoding.Unicode.GetString(data.ToArray());
            }
        }
    }
}
