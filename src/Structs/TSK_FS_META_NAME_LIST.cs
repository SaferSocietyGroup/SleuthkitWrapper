using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_FS_META_NAME_LIST
    {
        /// <summary>
        /// Pointer to next name (or NULL)
        /// </summary>
        IntPtr next;

        /// <summary>
        /// Name in UTF-8 (does not include parent directory name)
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        byte[] name;

        /// <summary>
        /// Inode address of parent directory (NTFS only)
        /// </summary>
        ulong par_inode;

        /// <summary>
        /// Sequence number of parent directory (NTFS only)
        /// </summary>
        uint par_seq;

        public bool HasNext
        {
            get
            {
                return next != IntPtr.Zero;
            }
        }

        public TSK_FS_META_NAME_LIST Next
        {
            get
            {
                if (next != IntPtr.Zero)
                {
                    return (TSK_FS_META_NAME_LIST)Marshal.PtrToStructure(next, typeof(TSK_FS_META_NAME_LIST));
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
        }

        public ulong ParentAddress
        {
            get
            {
                return par_inode;
            }
        }

        public uint ParentSequence
        {
            get
            {
                return par_seq;
            }
        }

        public String Name
        {
            get
            {
                return Encoding.UTF8.GetString(name).Trim(new char[] { '\0' });
            }
        }
    };
}
