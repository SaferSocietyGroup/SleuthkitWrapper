using System;
using System.ComponentModel;

namespace SleuthKit
{
    /// <summary>
    /// Constants for use when dealing with native structures and methods.
    /// </summary>
    internal enum StructureMagic : uint
    {
        /// <summary>
        /// TSK_FS_FILE_TAG 
        /// </summary>
        FilesystemFileTag = 0x11212212,

        /// <summary>
        /// TSK_FS_NAME_TAG 
        /// </summary>
        FilesystemNameTag = 0x23147869,

        /// <summary>
        /// TSK_FS_META_TAG
        /// </summary>
        FilesystemMetadataTag = 0x13524635,

        /// <summary>
        /// TSK_FS_INFO_TAG
        /// </summary>
        FilesystemInfoTag = 0x10101010,

        /// <summary>
        /// TSK_FS_BLOCK_TAG
        /// </summary>
        FilesystemBlockTag = 0x1b7c3f4a,

        /// <summary>
        /// TSK_FS_DIR_TAG
        /// </summary>
        FilesystemDirectoryTag = 0x97531246
    }

    /// <summary>
    /// Return value from filter menus 
    /// </summary>
    public enum FilterReturnCode
    {
        /// <summary>
        /// TSK_FILTER_CONT, Framework should continue to process this object
        /// </summary>
        Continue = 0x00,

        /// <summary>
        /// TSK_FILTER_STOP, Framework should stop processing the image
        /// </summary>
        Stop = 0x01,

        /// <summary>
        /// TSK_FILTER_SKIP, Framework should skip this object and go on to the next
        /// </summary>
        Skip = 0x02
    }

    /// <summary>
    /// Volume system type enumeration
    /// </summary>
    public enum VolumeSystemType
    {
        /// <summary>
        /// TSK_VS_TYPE_DETECT, Use autodetection methods
        /// </summary>
        Autodetect = 0x0000,

        /// <summary>
        /// TSK_VS_TYPE_DOS, DOS Partition table
        /// </summary>
        MBR = 0x0001,

        /// <summary>
        /// TSK_VS_TYPE_BSD, BSD Partition table
        /// </summary>
        BSD = 0x0002,

        /// <summary>
        /// TSK_VS_TYPE_SUN, Sun VTOC
        /// </summary>
        Sun = 0x0004,

        /// <summary>
        /// TSK_VS_TYPE_MAC, Mac partition table (APM)
        /// </summary>
        Apple = 0x0008,

        /// <summary>
        /// TSK_VS_TYPE_GPT, GPT partition table
        /// </summary>
        GPT = 0x0010,

        /// <summary>
        /// TSK_VS_TYPE_DBFILLER, fake partition table type for loaddb (for images that do not have a volume system)
        /// </summary>
        DatabaseFiller = 0x00F0,

        /// <summary>
        /// TSK_VS_TYPE_UNSUPP, Unsupported
        /// </summary>
        Unsupported = 0xffff,
    }

    /// <summary>
    /// Return value
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// Ok -- success
        /// </summary>
        OK = 0,

        /// <summary>
        /// System error -- should abort
        /// </summary>
        Error = 1,

        /// <summary>
        /// Data is corrupt, can still process another set of data
        /// </summary>
        Corrupt = 2,

        /// <summary>
        /// Stop further processing, not an error though. 
        /// </summary>
        Stop = 3
    }

    /// <summary>
    /// Managed version of TSK_FS_NAME_TYPE_ENUM
    /// </summary>
    public enum FilesystemNameType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_UNDEF </remarks>
        Unknown = 0,

        /// <summary>
        /// Named pipe
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_FIFO </remarks>
        NamedPipe = 1,

        /// <summary>
        /// Character Device
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_CHR </remarks>
        CharacterDevice = 2,

        /// <summary>
        /// Directory 
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_DIR </remarks>
        Directory = 3,

        /// <summary>
        /// Block device
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_BLK</remarks>
        BlockDevice = 4,

        /// <summary>
        /// Regular File
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_REG</remarks>
        Regular = 5,

        /// <summary>
        /// Symbolic link 
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_LNK</remarks>
        SymbolicLink = 6,

        /// <summary>
        /// Socket
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_SOCK</remarks>
        Socket = 7,

        /// <summary>
        /// Shadow inode (solaris) 
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_SHAD</remarks>
        ShadowInode = 8,

        /// <summary>
        /// Whiteout (openbsd) 
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_WHT</remarks>
        Whiteout = 9,

        /// <summary>
        /// Special (TSK added "Virtual" files)
        /// </summary>
        /// <remarks>TSK_FS_NAME_TYPE_VIRT </remarks>
        Virtual = 10,
    }

    /// <summary>
    /// Managed version of TSK_FS_META_CONTENT_TYPE_ENUM.
    /// </summary>
    public enum FileSystemMetaContentType
    {
        /// <summary>
        /// TSK_FS_META_CONTENT_TYPE_DEFAULT, Default value
        /// </summary>
        Default = 0x0,

        /// <summary>
        /// TSK_FS_META_CONTENT_TYPE_EXT4_EXTENTS, Ext4 with extents instead of individual pointers
        /// </summary>
        Ext4Extents = 0x1 
    }

    /// <summary>
    /// Managed version of TSK_FS_NAME_FLAG_ENUM, Filesystem name flags
    /// </summary>
    public enum NameFlags
    {
        /// <summary>
        /// Name is in an allocated state
        /// </summary>
        Allocated = 0x01,

        /// <summary>
        /// Name is in an unallocated state
        /// </summary>
        Unallocated = 0x02,
    }

    /// <summary>
    /// Managed version of TSK_FS_META_FLAG_ENUM, Metadata flags
    /// </summary>
    [Flags]
    public enum MetadataFlags : int
    {
        /// <summary>
        /// TSK_FS_META_FLAG_ALLOC, Metadata structure is currently in an allocated state
        /// </summary>
        Allocated = 0x01,

        /// <summary>
        /// TSK_FS_META_FLAG_UNALLOC, Metadata structure is currently in an unallocated state
        /// </summary>
        Unallocated = 0x02,

        /// <summary>
        /// TSK_FS_META_FLAG_USED, Metadata structure has been allocated at least once
        /// </summary>
        Used = 0x04,

        /// <summary>
        /// TSK_FS_META_FLAG_UNUSED, Metadata structure has never been allocated. 
        /// </summary>
        Unused = 0x08,

        /// <summary>
        /// TSK_FS_META_FLAG_COMP, The file contents are compressed. 
        /// </summary>
        Compressed = 0x10,

        /// <summary>
        /// TSK_FS_META_FLAG_ORPHAN, Return only metadata structures that have no file name pointing to the (inode_walk flag only)
        /// </summary>
        Orphan = 0x20,
    };

    /// <summary>
    /// Managed version of TSK_FS_META_ATTR_FLAG_ENUM, Meta attribute flags
    /// </summary>
    [Flags]
    public enum MetadataAttributeFlags
    {
        /// <summary>
        /// TSK_FS_META_ATTR_EMPTY, The data in the attributes (if any) is not for this file
        /// </summary>
        Empty,

        /// <summary>
        /// TSK_FS_META_ATTR_STUDIED, The data in the attributes are for this file
        /// </summary>
        Studied,

        /// <summary>
        /// TSK_FS_META_ATTR_ERROR, The attributes for this file could not be loaded
        /// </summary>
        Error,
    };


    /// <summary>
    /// Managed version of TSK_FS_META_TYPE_ENUM, Values for the mode field -- which identifies the file type and permissions.
    /// </summary>
    public enum MetadataType : int
    {
        /// <summary>
        /// TSK_FS_META_TYPE_UNDEF, Undefined
        /// </summary>
        Undefined = 0x00,

        /// <summary>
        /// TSK_FS_META_TYPE_REG, Regular file
        /// </summary>
        Regular = 0x01,

        /// <summary>
        /// TSK_FS_META_TYPE_DIR, Directory file
        /// </summary>
        Directory = 0x02,

        /// <summary>
        /// TSK_FS_META_TYPE_FIFO, Named pipe (fifo) 
        /// </summary>
        NamedPipe = 0x03,

        /// <summary>
        /// TSK_FS_META_TYPE_CHR, Character device 
        /// </summary>
        CharacterDevice = 0x04,

        /// <summary>
        /// TSK_FS_META_TYPE_BLK, Block device 
        /// </summary>
        BlockDevice = 0x05,

        /// <summary>
        /// TSK_FS_META_TYPE_LNK, Symbolic link
        /// </summary>
        Symlink = 0x06,

        /// <summary>
        /// TSK_FS_META_TYPE_SHAD, SOLARIS ONLY 
        /// </summary>
        Shadow = 0x07,

        /// <summary>
        /// TSK_FS_META_TYPE_SOCK, UNIX domain socket
        /// </summary>
        Socket = 0x08,

        /// <summary>
        /// TSK_FS_META_TYPE_WHT, Whiteout
        /// </summary>
        Whiteout = 0x09,

        /// <summary>
        /// TSK_FS_META_TYPE_VIRT, "Virtual File" created by TSK for file system areas
        /// </summary>
        VirtualFile = 0x0a,
    }

    /// <summary>
    /// Managed version of TSK_FS_META_MODE_ENUM.  The following describe the file permissions
    /// </summary>
    [Flags]
    public enum MetadataMode : int
    {
        /// <summary>
        /// set user id on execution 
        /// </summary>
        TSK_FS_META_MODE_ISUID = 0004000,

        /// <summary>
        /// set group id on execution 
        /// </summary>
        TSK_FS_META_MODE_ISGID = 0002000,

        /// <summary>
        /// sticky bit 
        /// </summary>
        TSK_FS_META_MODE_ISVTX = 0001000,

        /// <summary>
        /// R for owner 
        /// </summary>
        TSK_FS_META_MODE_IRUSR = 0000400,

        /// <summary>
        /// W for owner 
        /// </summary>
        TSK_FS_META_MODE_IWUSR = 0000200,

        /// <summary>
        /// X for owner 
        /// </summary>
        TSK_FS_META_MODE_IXUSR = 0000100,

        /// <summary>
        /// R for group 
        /// </summary>
        TSK_FS_META_MODE_IRGRP = 0000040,

        /// <summary>
        /// W for group 
        /// </summary>
        TSK_FS_META_MODE_IWGRP = 0000020,

        /// <summary>
        /// X for group 
        /// </summary>
        TSK_FS_META_MODE_IXGRP = 0000010,

        /// <summary>
        /// R for other 
        /// </summary>
        TSK_FS_META_MODE_IROTH = 0000004,

        /// <summary>
        /// W for other 
        /// </summary>
        TSK_FS_META_MODE_IWOTH = 0000002,

        /// <summary>
        /// X for other 
        /// </summary>
        TSK_FS_META_MODE_IXOTH = 0000001
    }

    /// <summary>
    /// Managed version of TSK_FS_DIR_WALK_FLAG_ENUM
    /// </summary>
    [Flags]
    public enum DirWalkFlags
    {
        /// <summary>
        /// TSK_FS_DIR_WALK_FLAG_NONE, No Flags
        /// </summary>
        NoFlags = 0x00,

        /// <summary>
        /// TSK_FS_DIR_WALK_FLAG_ALLOC, Return allocated names in callback
        /// </summary>
        Allocated = 0x01,

        /// <summary>
        /// TSK_FS_DIR_WALK_FLAG_UNALLOC, Return unallocated names in callback
        /// </summary>
        Unallocated = 0x02,

        /// <summary>
        /// TSK_FS_DIR_WALK_FLAG_RECURSE, Recurse into sub-directories 
        /// </summary>
        Recurse = 0x04,

        /// <summary>
        /// TSK_FS_DIR_WALK_FLAG_NOORPHAN, Do not return (or recurse into) the special Orphan directory
        /// </summary>
        NoOrphan = 0x08,
    }

    /// <summary>
    /// Return values from a dir walk
    /// </summary>
    public enum WalkReturnEnum
    {
        /// <summary>
        /// TSK_WALK_CONT, Walk function should continue to next object
        /// </summary>
        Continue = 0x0,

        /// <summary>
        /// TSK_WALK_STOP,  Walk function should stop processing units and return OK
        /// </summary>
        Stop = 0x1,

        /// <summary>
        /// TSK_WALK_ERROR, Walk function should stop processing units and return error
        /// </summary>
        Error = 0x2,
    }

    enum FilesystemInfoFlag
    {
        /// <summary>
        /// TSK_FS_INFO_FLAG_NONE, No Flags
        /// </summary>
        None = 0x00,

        /// <summary>
        /// TSK_FS_INFO_FLAG_HAVE_SEQ, File system has sequence numbers in the inode addresses.
        /// </summary>
        HasSequenceNumbers = 0x01,
        
        /// <summary>
        /// TSK_FS_INFO_FLAG_HAVE_NANOSEC, Nano second field in times will be set.
        /// </summary>
        HasNanoSec = 0x02
    };

    enum FileReadFlag
    {
        /// <summary>
        /// TSK_FS_FILE_READ_FLAG_NONE 
        /// </summary>
        None = 0x00,

        /// <summary>
        /// TSK_FS_FILE_READ_FLAG_SLACK, Allow read access into slack space
        /// </summary>
        Slack = 0x01,

        /// <summary>
        /// TSK_FS_FILE_READ_FLAG_NOID , Ignore the Id argument given in the API (use only the type)
        /// </summary>
        NoID = 0x02,
    }

    /// <summary>
    /// Flags that are used in TSK_FS_BLOCK and in callback of file_walk. 
    /// Note that some of these are dependent.  A block can be either TSK_FS_BLOCK_FLAG_ALLOC or TSK_FS_BLOCK_FLAG_UNALLOC.  
    /// It can be one of TSK_FS_BLOCK_FLAG_RAW, TSK_FS_BLOCK_FLAG_BAD, TSK_FS_BLOCK_FLAG_RES, TSK_FS_BLOCK_FLAG_SPARSE, or TSK_FS_BLOCK_FLAG_COMP.  
    /// Note that some of these are set only by file_walk because they are file-level details, such as compression and sparse.
    /// </summary>
    /// <remarks>TSK_FS_BLOCK_FLAG_ENUM</remarks>
    [Flags]
    public enum FileSystemBlockFlags
    {
        /// <summary>
        /// TSK_FS_BLOCK_FLAG_UNUSED, Used to show that TSK_FS_BLOCK structure has no data in it
        /// </summary>
        Unused = 0x0000,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_ALLOC, Block is allocated (and not TSK_FS_BLOCK_FLAG_UNALLOC)
        /// </summary>
        [Description("Block is allocated")]
        Allocated = 0x0001,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_UNALLOC, Block is unallocated (and not TSK_FS_BLOCK_FLAG_ALLOC)
        /// </summary>
        [Description("Block is unallocated")]
        Unallocated = 0x0002,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_CONT, Block (could) contain file content (and not TSK_FS_BLOCK_FLAG_META)
        /// </summary>
        Content = 0x0004,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_META, Block (could) contain file system metadata (and not TSK_FS_BLOCK_FLAG_CONT)
        /// </summary>
        [Description("Block could contain filesystem metadata")]
        Metadata = 0x0008,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_BAD, Block has been marked as bad by the file system
        /// </summary>
        [Description("Block has been marked as bad by the file system")]
        Bad = 0x0010,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_RAW, The data has been read raw from the disk (and not COMP or SPARSE)
        /// </summary>
        Raw = 0x0020,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_SPARSE, The data passed in the file_walk calback was stored as sparse (all zeros) (and not RAW or COMP)
        /// </summary>
        [Description("Block was stored as sparse (all zeros)")]
        Sparse = 0x0040,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_COMP, The data passed in the file_walk callback was stored in a compressed form (and not RAW or SPARSE)
        /// </summary>
        Compressed = 0x0080,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_RES , The data passed in the file_walk callback is from an NTFS resident file
        /// </summary>
        [Description("Block was stored as sparse (all zeros)")]
        NTFSResident = 0x0100,

        /// <summary>
        /// TSK_FS_BLOCK_FLAG_AONLY, TSK_FS_BLOCK has no content (it could be non-empty, but should be ignored), but the flags and such are accurate
        /// </summary>
        FlagAonly = 0x0200
    }

    /// <summary>
    /// Flags used by tsk_fs_file_walk to determine when the callback function should be used.
    /// </summary>
    public enum FileWalkFlag
    {
        /// <summary>
        /// No Flag.
        /// </summary>
        None = 0x00, 

        /// <summary>
        /// Include the file's slack space in the callback.
        /// </summary>
        Slack = 0x01,

        /// <summary>
        /// Ignore the Id argument given in the API (use only the type).
        /// </summary>
        NoId = 0x02, 

        /// <summary>
        /// Provide callback with only addresses and no file content.
        /// </summary>
        AOnly = 0x04, 

        /// <summary>
        /// Do not include sparse blocks in the callback.
        /// </summary>
        NoSparse = 0x08, 
    }

    /// <summary>
    /// Managed version of TSK_FS_TYPE_ENUM
    /// </summary>
    [Flags]
    public enum FileSystemType : uint
    {
        /// <summary>
        /// TSK_FS_TYPE_DETECT, Use autodetection methods
        /// </summary>
        Autodetect = 0x00000000,

        /// <summary>
        /// TSK_FS_TYPE_NTFS, NTFS file system
        /// </summary>
        NTFS = 0x00000001,

        /// <summary>
        /// TSK_FS_TYPE_FAT12, FAT12 file system
        /// </summary>
        FAT12 = 0x00000002,

        /// <summary>
        /// TSK_FS_TYPE_FAT16, FAT16 file system
        /// </summary>
        FAT16 = 0x00000004,

        /// <summary>
        /// TSK_FS_TYPE_FAT32, FAT32 file system
        /// </summary>
        FAT32 = 0x00000008,

        /// <summary>
        /// TSK_FS_TYPE_EXFAT, ExFAT file system
        /// </summary>
        ExFAT = 0x0000000a,

        /// <summary>
        /// TSK_FS_TYPE_FAT_DETECT, FAT auto detection
        /// </summary>
        [Description("Autodetect FAT")]
        FATAutodetect = 0x0000000e,

        /// <summary>
        /// TSK_FS_TYPE_FFS1, UFS1 (FreeBSD, OpenBSD, BSDI ...)
        /// </summary>
        UFS1 = 0x00000010,

        /// <summary>
        /// TSK_FS_TYPE_FFS1B, UFS1b (Solaris - has no type)
        /// </summary>
        UFS1b = 0x00000020,

        /// <summary>
        /// TSK_FS_TYPE_FFS2, UFS2 - FreeBSD, NetBSD 
        /// </summary>
        UFS2 = 0x00000040,

        /// <summary>
        /// TSK_FS_TYPE_FFS_DETECT, UFS auto detection
        /// </summary>
        [Description("Autodetect UFS")]
        UFSAutodetect = 0x00000070,

        /// <summary>
        /// TSK_FS_TYPE_EXT2, Ext2 file system
        /// </summary>
        ext2 = 0x00000080,

        /// <summary>
        /// TSK_FS_TYPE_EXT3, Ext3 file system
        /// </summary>
        ext3 = 0x00000100,

        /// <summary>
        /// TSK_FS_TYPE_EXT4, Ext4 file system
        /// </summary>
        ext4 = 0x00002000,

        /// <summary>
        /// TSK_FS_TYPE_EXT_DETECT, ExtX auto detection
        /// </summary>
        [Description("Autodetect ext")]
        extAutodetect = 0x00002180,

        /// <summary>
        /// TSK_FS_TYPE_SWAP, SWAP file system
        /// </summary>
        Swap = 0x00000200,

        /// <summary>
        /// TSK_FS_TYPE_RAW, RAW file system
        /// </summary>
        Raw = 0x00000400,

        /// <summary>
        /// TSK_FS_TYPE_ISO9660, ISO9660 file system
        /// </summary>
        ISO9660 = 0x00000800,

        /// <summary>
        /// TSK_FS_TYPE_HFS, HFS file system
        /// </summary>
        HFS = 0x00001000,
         
        /// <summary>
        /// TSK_FS_TYPE_YAFFS2, YAFFS2 file system
        /// </summary>
        Yaffs2 = 0x00004000,     
                                                        
        /// <summary>
        /// TSK_FS_TYPE_UNSUPP, Unsupported file system
        /// </summary>
        Unsupported = 0xffffffff,
    }

    /// <summary>
    /// Endian .. little or big .. or middle! 
    /// </summary>
    /// <remarks>
    /// The term big-endian originally comes from Jonathan Swift's satirical novel Gulliver’s Travels by way of Danny Cohen in 1980. In 1726, Swift described tensions in Lilliput and Blefuscu: whereas royal edict in Lilliput requires cracking open one's soft-boiled egg at the small end, inhabitants of the rival kingdom of Blefuscu crack theirs at the big end (giving them the moniker Big-endians). The terms little-endian and endianness have a similar intent. 
    /// http://en.wikipedia.org/wiki/Endianness
    /// </remarks>
    public enum Endianness
    {
        /// <summary>
        /// TSK_LIT_ENDIAN, Data is in little endian
        /// </summary>
        Little = 0x01,

        /// <summary>
        /// TSK_BIG_ENDIAN, Data is in big endian
        /// </summary>
        Big = 0x02
    }

    /// <summary>
    /// TSK_IMG_TYPE_ENUM, Type of image (dd, split, aff, encase, etc)
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// TSK_IMG_TYPE_DETECT, Use autodetection methods
        /// </summary>
        Autodetect = 0x0000,        

        /// <summary>
        /// TSK_IMG_TYPE_RAW_SING, Raw single disk image
        /// </summary>
        Raw = 0x0001,

        /// <summary>
        /// TSK_IMG_TYPE_RAW_SING, Raw single (backward compatibility) depreciated
        /// </summary>
        RawSing = Raw,    // AK - added

        /// <summary>
        /// TSK_IMG_TYPE_RAW_SPLIT, Raw split (backward compatibility) depreciated
        /// </summary>
        RawSplit = Raw, 

        /// <summary>
        /// TSK_IMG_TYPE_AFF_AFF, AFF AFF Format
        /// </summary>
        AFF = 0x0004,

        /// <summary>
        /// TSK_IMG_TYPE_AFF_AFD , AFD AFF Format
        /// </summary>
        AFD = 0x0008,

        /// <summary>
        /// TSK_IMG_TYPE_AFF_AFM, AFM AFF Format
        /// </summary>
        AFM = 0x0010,

        /// <summary>
        /// TSK_IMG_TYPE_AFF_ANY, Any format supported by AFFLIB (including beta ones)
        /// </summary>
        AnyAFF = 0x0020,

        /// <summary>
        /// TSK_IMG_TYPE_EWF_EWF, EnCase image
        /// </summary>
        EWF = 0x0040,

        /// <summary>
        /// TSK_IMG_TYPE_UNSUPP, Unsupported disk image type
        /// </summary>
        Unsupported = 0xffff,
    }

    /// <summary>
    /// TSK_VS_PART_FLAG_ENUM
    /// </summary>
    [Flags]
    public enum VolumeFlags
    {
        /// <summary>
        /// TSK_VS_PART_FLAG_ALLOC, Sectors are allocated to a volume in the volume system
        /// </summary>
        Allocated = 0x01,

        /// <summary>
        ///  TSK_VS_PART_FLAG_UNALLOC, Sectors are not allocated to a volume 
        /// </summary>
        Unallocated = 0x02,

        /// <summary>
        /// TSK_VS_PART_FLAG_META, Sectors contain volume system metadata and could also be ALLOC or UNALLOC
        /// </summary>
        Metadata = 0x04,

        /// <summary>
        /// TSK_VS_PART_FLAG_ALL, Show all sectors in the walk. 
        /// </summary>
        All = 0x07,
    }

    /// <summary>
    /// TSK_FS_ATTR_FLAG_ENUM
    /// </summary>
    [Flags]
    public enum AttributeFlags
    {
        /// <summary>
        /// No Flag
        /// </summary>
        None = 0x00,

        /// <summary>
        /// Data structure is in use
        /// </summary>
        InUse = 0x01,

        /// <summary>
        /// Contains non-resident data (i.e. located in blocks)
        /// </summary>
        NonResident = 0x02,

        /// <summary>
        /// Contains resident data (i.e. in a small buffer)
        /// </summary>
        Resident = 0x04,

        /// <summary>
        /// Contains encrypted data
        /// </summary>
        Encrypted = 0x10,

        /// <summary>
        /// Contains compressed data
        /// </summary>
        Compressed = 0x20,

        /// <summary>
        /// Contains sparse data
        /// </summary>
        Sparse = 0x40,

        /// <summary>
        /// Data was determined in file recovery mode
        /// </summary>
        Recovery = 0x80,
    }

    [Flags]
    public enum AttributeRunFlags
    {
        None = 0x00,       ///< No Flag
        Filler = 0x01,     ///< Entry is a filler for a run that has not been seen yet in the processing (or has been lost)
        Sparse = 0x02      ///< Entry is a sparse run where all data in the run is zeros        
    }

    /// <summary>
    /// TSK_FS_ATTR_TYPE_ENUM
    /// </summary>
    public enum AttributeType
    {
        NotFound = 0x00,      // 0
        Default = 0x01,        // 1

        NtfsSi = 0x10,        // 16
        NtfsAttrlist = 0x20,  // 32
        NtfsFName = 0x30,     // 48
        NtfsVVer = 0x40,      // 64 (NT)
        NtfsObjId = 0x40,     // 64 (2K)
        NtfsSec = 0x50,       // 80
        NtfsVName = 0x60,     // 96
        NtfsVInfo = 0x70,     // 112
        NtfsData = 0x80,      // 128
        NtfsIdxRoot = 0x90,   // 144
        NtfsIdxAlloc = 0xA0,  // 160
        NtfsBitmap = 0xB0,    // 176
        NtfsSymlink = 0xC0,    // 192 (NT)
        NtfsReparse = 0xC0,   // 192 (2K)
        NtfsEAInfo = 0xD0,    // 208
        NtfsEA = 0xE0,        // 224
        NtfsProp = 0xF0,      //  (NT)
        NtfsLog = 0x100,      //  (2K)

        UnixIndir = 0x1001,   //  Indirect blocks for UFS and ExtX file systems
        UnixExtent = 0x1002,  //  Extents for Ext4 file system

        // Types for HFS+ File Attributes
        HfsDefault = 0x01,    // 1    Data fork of fs special files and misc
        HfsData = 0x1100,     // 4352 Data fork of regular files
        HfsRSRC = 0x1101,     // 4353 Resource fork of regular files
        HfsExtAttr = 0x1102, // 4354 Extended Attributes, except compression records
        HfsCompRec = 0x1103, // 4355 Compression records        
    }
}
