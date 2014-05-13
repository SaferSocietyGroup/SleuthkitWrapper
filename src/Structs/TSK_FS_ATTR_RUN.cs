using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_FS_ATTR_RUN
    {
        private IntPtr next_ptr;  ///< Pointer to the next run in the attribute (or NULL)

        private ulong offset;     ///< Offset (in blocks) of this run in the file

        private ulong addr;       ///< Starting block address (in file system) of run

        private ulong len;        ///< Number of blocks in run (0 when entry is not in use)
        
        private AttributeRunFlags flags;        ///< Flags for run

        public bool HasNext
        {
            get
            {
                return next_ptr != IntPtr.Zero;
            }
        }

        public TSK_FS_ATTR_RUN Next
        {
            get
            {
                return ((TSK_FS_ATTR_RUN)Marshal.PtrToStructure(next_ptr, typeof(TSK_FS_ATTR_RUN)));
            }
        }

        public ulong Offset
        {
            get
            {
                return offset;
            }
        }

        public ulong Address
        {
            get
            {
                return addr;
            }
        }

        public ulong Length
        {
            get
            {
                return len;
            }
        }

        public AttributeRunFlags Flags
        {
            get
            {
                return flags;
            }
        }
    }
}
