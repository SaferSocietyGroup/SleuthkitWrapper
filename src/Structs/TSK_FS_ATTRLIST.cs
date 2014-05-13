using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_FS_ATTRLIST
    {
        IntPtr head_ptr;

        public bool HasHead
        {
            get
            {
                return head_ptr != IntPtr.Zero;
            }
        }

        public TSK_FS_ATTR Head
        {
            get
            {
                return ((TSK_FS_ATTR)Marshal.PtrToStructure(head_ptr, typeof(TSK_FS_ATTR)));            
            }
        }

        public bool IsEmpty
        {
            get
            {
                return !HasHead;
            }
        }

        public IEnumerable<TSK_FS_ATTR> List
        {
            get
            {
                if (HasHead)
                {
                    TSK_FS_ATTR current = Head;
                    for (; ; )
                    {
                        yield return current;

                        if (current.HasNext)
                        {
                            current = current.Next;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
