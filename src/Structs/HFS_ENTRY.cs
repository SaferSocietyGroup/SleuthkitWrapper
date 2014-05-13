using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 784)]
    public struct HFS_ENTRY
    {
        /// <summary>
        /// on-disk catalog record (either hfs_file or hfs_folder)
        /// </summary>
        //[FieldOffset(0)]
        //hfs_file cat;

        /// <summary>
        /// flags for on-disk record
        /// </summary>
        [FieldOffset(248)]
        int flags;

        /// <summary>
        /// cnid
        /// </summary>
        [FieldOffset(256)]
        ulong inum;

        /// <summary>
        /// thread record
        /// </summary>
        [FieldOffset(264)]
        hfs_thread thread;

        public hfs_thread Thread
        {
            get
            {
                return thread;
            }
        }
    }
}
