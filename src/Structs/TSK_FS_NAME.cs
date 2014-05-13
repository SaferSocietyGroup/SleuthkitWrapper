using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// TSK_FS_NAME Generic structure to store the file name information that is stored in a directory. Most file systems seperate the file name from the metadata, but some do not (such as FAT). This structure contains the name and address of the metadata.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_FS_NAME
    {
        /// <summary>
        /// tag, can be used to validate that we have the right kind of struct. a magic header for the struct, essentially.
        /// </summary>
        StructureMagic tag;

        /// <summary>
        /// The name of the file (in UTF-8)
        /// </summary>
        IntPtr ptr_name;

        /// <summary>
        /// The number of bytes allocated to name
        /// </summary>
        UIntPtr name_size;

        /// <summary>
        /// The short name of the file or null (in UTF-8)
        /// </summary>
        IntPtr ptr_short_name;

        /// <summary>
        /// The number of bytes allocated to shrt_name
        /// </summary>
        UIntPtr short_name_size;

        /// <summary>
        /// Address of the metadata structure that the name points to. 
        /// </summary>
        ulong meta_addr;

        /// <summary>
        /// Sequence number for metadata structure (NTFS only) 
        /// </summary>
        uint meta_seq;

        /// <summary>
        /// Metadata address of parent directory (equal to meta_addr if this entry is for root directory). 
        /// </summary>
        ulong par_addr;

        /// <summary>
        /// Sequence number for parent directory (NTFS only)
        /// </summary>
        uint par_seq;

        /// <summary>
        /// File type information (directory, file, etc.)
        /// </summary>
        FilesystemNameType type;

        /// <summary>
        /// Flags that describe allocation status etc. 
        /// </summary>
        NameFlags flags;

        /// <summary>
        /// validates the tag contains the proper constant
        /// </summary>
        public bool AppearsValid
        {
            get
            {
                return this.tag == StructureMagic.FilesystemNameTag;
            }
        }

        /// <summary>
        /// The filename
        /// </summary>
        public string LongName
        {
            get
            {
                string str = null;
                var ns = (int)name_size.ToUInt32();
                if (ns > 0)
                {
                    var local = new byte[ns];
                    Marshal.Copy(ptr_name, local, 0, local.Length);
                    if (local[ns - 1] == 0)
                    {
                        ns--; //trim it
                    }
                    str = Encoding.UTF8.GetString(local, 0, ns);
                }
                return str;
            }
        }

        /// <summary>
        /// The short name, if any
        /// </summary>
        public string ShortName
        {
            get
            {
                string str = null;
                uint sns = short_name_size.ToUInt32();
                if (sns > 0)
                {
                    var local = new byte[short_name_size.ToUInt32()];
                    Marshal.Copy(ptr_short_name, local, 0, local.Length);
                    str = Encoding.UTF8.GetString(local);
                }
                return str;
            }
        }

        /// <summary>
        /// returns the long name or short name, depending on whats available.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            string name = null;

            //try long name
            if (ptr_name != IntPtr.Zero)
            {
                name = this.LongName;
            }
            //long name was null try shortname
            if (name == null && ptr_short_name != IntPtr.Zero)
            {
                name = this.ShortName;
            }

            //go back to the base impl, which sucks but its better than nothing 
            if (name == null)
            {
                name = base.ToString();
            }

            if (name.IndexOf((char)0) > -1)
            {
                name = name.Replace("\0", "");
            }

            return name;
        }

        /// <summary>
        /// Prints out the name, long name first, if it is null then it tries short name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return LongName != null
                ? LongName.Trim(new char[] { '\0' })
                : ShortName.Trim(new char[] { '\0' });
        }

        public ulong MetadataAddress
        {
            get
            {
                return this.meta_addr;
            }
        }

        public uint MetadataSequence
        {
            get
            {
                return this.meta_seq;
            }
        }

        public ulong ParentAddress
        {
            get
            {
                return this.par_addr;
            }
        }

        public uint ParentSequence
        {
            get
            {
                return this.par_seq;
            }
        }

        public FilesystemNameType Type
        {
            get
            {
                return type;
            }
        }

        public NameFlags Flags
        {
            get
            {
                return flags;
            }
        }
    }
}
