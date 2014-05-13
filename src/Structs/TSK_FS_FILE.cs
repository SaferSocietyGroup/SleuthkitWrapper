using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// represents TSK_FS_FILE.  Generic structure used to refer to files in the file system.  A file will typically have a name and metadata.  This structure holds that type of information.
    /// When deleted files are being processed, this structure may have the name defined but not metadata because it no longer exists. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_FS_FILE
    {
        public static TSK_FS_FILE FromStream(Stream stream)
        {
            var ret = new TSK_FS_FILE();
            var br = new BinaryReader(stream);
            ret.tag = (StructureMagic)br.ReadUInt32();
            return ret;
        }

        /// <summary>
        /// tag, can be used to validate that we have the right kind of struct.  a magic header for the struct, essentially.
        /// </summary>
        StructureMagic tag;

        /// <summary>
        /// pointer to filename struct - or null if file was opened using metadata address
        /// </summary>
        IntPtr ptr_filename;

        /// <summary>
        /// Pointer to metadata of file (or NULL if name has invalid metadata address)
        /// </summary>
        IntPtr ptr_meta;

        /// <summary>
        /// Pointer to file system that the file is located in.
        /// </summary>
        IntPtr ptr_fsinfo;

        /// <summary>
        /// filename
        /// </summary>
        public TSK_FS_NAME? Name
        {
            get
            {
                TSK_FS_NAME? ret = null;
                if (ptr_filename != IntPtr.Zero)
                {
                    ret = (TSK_FS_NAME)Marshal.PtrToStructure(ptr_filename, typeof(TSK_FS_NAME));
                }
                return ret;
            }
        }

        /// <summary>
        /// metadata
        /// </summary>
        public TSK_FS_META? Metadata
        {
            get
            {
                TSK_FS_META? ret = null;
                if (ptr_meta != IntPtr.Zero)
                {
                    ret = (TSK_FS_META)Marshal.PtrToStructure(ptr_meta, typeof(TSK_FS_META));
                }
                return ret;
            }
        }

        /// <summary>
        /// validates the tag contains the proper constant
        /// </summary>
        public bool AppearsValid
        {
            get
            {
                return tag == StructureMagic.FilesystemFileTag;
            }
        }

        /// <summary>
        /// Looks at the name and metadata struct, and attempts to determine if the metadata actually belongs to the name.
        /// Returns true if the Metadata appears to belong to the name (the name points to the correct data), false otherwise
        /// </summary>
        /*
        public bool MetadataAppearsValid(bool isNtfs)
        { 
            if (!Name.HasValue || !Metadata.HasValue)
            {
                return false;
            }

            //We have to check if the metadata has a name that actually matches the found name
            String maybeLongName = (Name.Value.LongName ?? String.Empty).TrimEnd(new char[] { '\0' });
            String maybeShortName = (Name.Value.ShortName ?? String.Empty).TrimEnd(new char[] { '\0' });
            
            ulong maybeParentAddress = Name.Value.ParentAddress;

            if (Metadata.Value.NameListHead.HasValue)
            {
                TSK_FS_META_NAME_LIST realName = Metadata.Value.NameListHead.Value;

                for (;;)
                {
                    if ((!isNtfs || realName.ParentAddress == maybeParentAddress) &&
                        (realName.Name.Equals(maybeShortName) || realName.Name.Equals(maybeLongName)))
                    {
                        return true;
                    }

                    if (realName.HasNext) 
                    {
                        realName = realName.Next;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return false;
        }
        //*/
    }
}
