using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// represents TSK_FS_META
    /// </summary>
    [StructLayout(LayoutKind.Explicit, 
#if Bit32
    Size = 216
#elif Bit64
    Size = 240
#endif
)]
    public struct TSK_FS_META
    {
        #region fields
        /// <summary>
        /// tag, can be used to validate that we have the right kind of struct.  a magic header for the struct, essentially.
        /// </summary>
        [FieldOffset(0)]
        StructureMagic tag;

        /// <summary>
        /// Flags for this file for its allocation status etc.
        /// </summary>
        [FieldOffset(4)]
        internal MetadataFlags flags;

        /// <summary>
        /// Address of the meta data structure for this file
        /// </summary>
        [FieldOffset(8)]
        internal long addr;

        /// <summary>
        /// File type
        /// </summary>
        [FieldOffset(16)]
        internal MetadataType type;

        /// <summary>
        /// Unix-style permissions
        /// </summary>
        [FieldOffset(20)]
        internal MetadataMode mode;

        /// <summary>
        /// link count (number of file names pointing to this)
        /// </summary>
        [FieldOffset(24)]
        long nlink;

        /// <summary>
        /// file size (in bytes) - yes this is a signed 64-bit integer, despite it being unsigned in sleuthkit (?).  It is easier this way.
        /// </summary>
        [FieldOffset(32)]
        long size;

        /// <summary>
        /// user id
        /// </summary>
        [FieldOffset(40)]
        uint uid;

        /// <summary>
        /// group id
        /// </summary>
        [FieldOffset(44)]
        uint gid;

        #region times

        /// <summary>
        /// last file content modification time (stored in number of seconds since Jan 1, 1970 UTC)
        /// </summary>
        [FieldOffset(48)]
        long mtime;

        /// <summary>
        /// nano-second resolution in addition to m_time
        /// </summary>
        [FieldOffset(56)]
        long mtime_nano;

        /// <summary>
        /// last file content accessed time (stored in number of seconds since Jan 1, 1970 UTC)
        /// </summary>
        [FieldOffset(64)]
        long atime;

        /// <summary>
        /// nano-second resolution in addition to a_time
        /// </summary>
        [FieldOffset(72)]
        long atime_nano;

        /// <summary>
        ///  last file / metadata status change time (stored in number of seconds since Jan 1, 1970 UTC)
        /// </summary>
        [FieldOffset(80)]
        long ctime;

        /// <summary>
        /// nano-second resolution in addition to c_time
        /// </summary>
        [FieldOffset(88)]
        long ctime_nano;

        /// <summary>
        /// Created time (stored in number of seconds since Jan 1, 1970 UTC)
        /// </summary>
        [FieldOffset(96)]
        long crtime;

        /// <summary>
        /// nano-second resolution in addition to cr_time
        /// </summary>
        [FieldOffset(104)]
        long crtime_nano;

        #region filesystem specific times

        /* lots of stuff here, not mapped
        int special_time;

        uint special_nano;
        //*/
        #endregion

        #endregion

        /// <summary>
        /// Pointer to file system specific data that is used to store references to file content
        /// </summary>
        [FieldOffset(184)]
        IntPtr content_ptr;

        /// <summary>
        /// size of content  buffer
        /// </summary>
#if Bit32
        [FieldOffset(188)]
#elif Bit64
        [FieldOffset(192)]
#endif
        UIntPtr content_len;

        /// <summary>
        /// The content type
        /// </summary>
#if Bit32
        [FieldOffset(192)]
#elif Bit64
        [FieldOffset(200)]
#endif
        FileSystemMetaContentType content_type;

        /// <summary>
        /// Sequence number for file (NTFS only, is incremented when entry is reallocated) 
        /// </summary>
#if Bit32
        [FieldOffset(196)]
#elif Bit64
        [FieldOffset(204)]
#endif
        uint seq;

        /// <summary>
        /// Contains run data on the file content (specific locations where content is stored).  
        /// Check attr_state to determine if data in here is valid because not all file systems 
        /// load this data when a file is loaded.  It may not be loaded until needed by one
        /// of the APIs. Most file systems will have only one attribute, but NTFS will have several. 
        /// </summary>
#if Bit32
        [FieldOffset(200)]
#elif Bit64
        [FieldOffset(208)]
#endif
        IntPtr attr_ptr; //TSK_FS_ATTRLIST *attr;

        /// <summary>
        /// State of the data in the TSK_FS_META::attr structure
        /// </summary>
#if Bit32
        [FieldOffset(204)]
#elif Bit64
        [FieldOffset(216)]
#endif
        MetadataAttributeFlags attr_state;

        /// <summary>
        /// Name of file stored in metadata (FAT and NTFS Only)
        /// </summary>
#if Bit32
        [FieldOffset(208)]
#elif Bit64
        [FieldOffset(224)]
#endif
        IntPtr name2;  //TSK_FS_META_NAME_LIST* name2;   ///< 

        //char* link;             
        /// <summary>
        /// Name of target file if this is a symbolic link
        /// </summary>
#if Bit32
        [FieldOffset(212)]
#elif Bit64
        [FieldOffset(232)]
#endif
        IntPtr link;

        #endregion

        #region properties

        /// <summary>
        /// validates the tag contains the proper constant
        /// </summary>
        public bool AppearsValid
        {
            get
            {
                return this.tag == StructureMagic.FilesystemMetadataTag;
            }
        }

        /// <summary>
        /// Metadata Address
        /// </summary>
        public long Address
        {
            get
            {
                return this.addr;
            }
        }

        /// <summary>
        /// Metadata flags
        /// </summary>
        public MetadataFlags MetadataFlags
        {
            get
            {
                return this.flags;
            }
        }

        /// <summary>
        /// Metadata type
        /// </summary>
        public MetadataType MetadataType
        {
            get
            {
                return this.type;
            }
        }

        public MetadataMode Mode
        {
            get
            {
                return this.mode;
            }
        }

        public long LinkCount
        {
            get
            {
                return this.nlink;
            }
        }

        /// <summary>
        /// File size, in bytes
        /// </summary>
        public long Size
        {
            get
            {
                return this.size;
            }
        }

        /// <summary>
        /// Modified time in unix epoch ticks
        /// </summary>
        public long MTime
        {
            get
            {
                return (long)this.mtime;
            }
        }

        /// <summary>
        /// Access time in unix epoch ticks
        /// </summary>
        public long ATime
        {
            get
            {
                return (long)this.atime;
            }
        }

        /// <summary>
        /// Metadata change time in unix epoch ticks
        /// </summary>
        public long CTime
        {
            get
            {
                return (long)this.ctime;
            }
        }


        /// <summary>
        /// Kreayshawn time in unix epoch ticks
        /// </summary>
        public long CRTime
        {
            get
            {
                return (long)this.crtime;
            }
        }

        public uint Sequence
        {
            get
            {
                return this.seq;
            }
        }

        internal IntPtr attrPtr
        {
            get
            {
                return attr_ptr;
            }
        }

        public bool HasAttributeList
        {
            get
            {
                return attr_ptr != IntPtr.Zero;
            }
        }

        public TSK_FS_ATTRLIST AttributeList
        {
            get
            {
                return ((TSK_FS_ATTRLIST)Marshal.PtrToStructure(attr_ptr, typeof(TSK_FS_ATTRLIST)));
            }
        }

        public MetadataAttributeFlags AttributeState
        {
            get
            {
                return this.attr_state;
            }
        }

        public IEnumerable<TSK_FS_META_NAME_LIST> NameList
        {
            get
            {
                if (name2 != IntPtr.Zero)
                {
                    TSK_FS_META_NAME_LIST entry = (TSK_FS_META_NAME_LIST)Marshal.PtrToStructure(name2, typeof(TSK_FS_META_NAME_LIST));

                    for (; ; )
                    {
                        yield return entry;

                        if (entry.HasNext)
                        {
                            entry = entry.Next;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        /*
        public TSK_FS_META_NAME_LIST? NameListHead
        {
            get
            {
                TSK_FS_META_NAME_LIST? ret = null;
                if (name2 != IntPtr.Zero)
                {
                    ret = (TSK_FS_META_NAME_LIST)Marshal.PtrToStructure(name2, typeof(TSK_FS_META_NAME_LIST));
                }
                return ret;
            }
        }
        //*/

        //public DateTime LastAccessed
        //{
        //    get
        //    {
        //        var val = this.atime.ToUInt64();
        //        return DateTime.MinValue;
        //    }
        //}

        #endregion
    }
}
