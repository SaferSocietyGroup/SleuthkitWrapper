using SleuthKit.Structs;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SleuthKit
{
    /// <summary>
    /// pinvoke
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// The framework should know to look for libtsk4.dll, libtsk4.so, libtsk3.dylib, depending on the OS.  It is that smart.
        /// </summary>
        private const string NativeLibrary = "libtsk4";

        #region pinvoke

        #region disk image functions

        #region open functions

        /// extern TSK_IMG_INFO *tsk_img_open(int,const TSK_TCHAR * const images[], TSK_IMG_TYPE_ENUM, unsigned int a_ssize);
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern DiskImageHandle tsk_img_open(int imageCount, IntPtr[] image, ImageType imageType, uint sectorSize);

        //extern TSK_IMG_INFO *tsk_img_open_sing(const TSK_TCHAR * a_image,TSK_IMG_TYPE_ENUM type, unsigned int a_ssize);
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        internal static extern DiskImageHandle tsk_img_open_sing(string image, uint imageType, uint sectorSize);

        //extern TSK_IMG_INFO *tsk_img_open_utf8(int num_img, const char *const images[], TSK_IMG_TYPE_ENUM type, unsigned int a_ssize);
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern DiskImageHandle tsk_img_open_utf8(int imageCount, byte[] image, ImageType imageType, uint sectorSize);

        //extern TSK_IMG_INFO *tsk_img_open_utf8_sing(const char *a_image, TSK_IMG_TYPE_ENUM type, unsigned int a_ssize);
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern DiskImageHandle tsk_img_open_utf8_sing([MarshalAs(UnmanagedType.LPWStr)] string image, ImageType imageType, uint sectorSize);

        #endregion

        //ssize_t tsk_img_read(TSK_IMG_INFO * a_img_info, TSK_OFF_T a_off, char *a_buf, size_t a_len)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int tsk_img_read(DiskImageHandle img, long a_off, byte[] buf, int len);

        //void tsk_img_close(TSK_IMG_INFO * a_img_info)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void tsk_img_close(IntPtr img);

        #endregion

        #region volume system functions

        //TSK_VS_INFO *tsk_vs_open(TSK_IMG_INFO * img_info, TSK_DADDR_T offset, TSK_VS_TYPE_ENUM type)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern VolumeSystemHandle tsk_vs_open(DiskImageHandle image, long offset, VolumeSystemType type);

        //void tsk_vs_close(TSK_VS_INFO * a_vs)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void tsk_vs_close(IntPtr handle);

        //ssize_t tsk_vs_part_read(const TSK_VS_PART_INFO * a_vs_part, TSK_OFF_T a_off, char *a_buf, size_t a_len)        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int tsk_vs_part_read(IntPtr handle, long offset, byte[] buffer, UIntPtr length);

        #endregion

        #region filesystem functions

        //TSK_FS_INFO *tsk_fs_open_img(TSK_IMG_INFO * a_img_info, TSK_OFF_T a_offset, TSK_FS_TYPE_ENUM a_ftype)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern FileSystemHandle tsk_fs_open_img(DiskImageHandle image, long offset, FileSystemType fstype);

        //TSK_FS_INFO *tsk_fs_open_vol(const TSK_VS_PART_INFO * a_part_info, TSK_FS_TYPE_ENUM a_ftype)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern FileSystemHandle tsk_fs_open_vol(IntPtr volinfo, FileSystemType fstype);

        //const char *tsk_fs_type_toname(TSK_FS_TYPE_ENUM ftype)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        internal static extern String tsk_fs_type_toname(FileSystemType ftype);

        //void tsk_fs_close(TSK_FS_INFO * a_fs)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void tsk_fs_close(IntPtr fsptr);

        #region block stuff

        //TSK_FS_BLOCK *tsk_fs_block_get(TSK_FS_INFO * a_fs, TSK_FS_BLOCK * a_fs_block, TSK_DADDR_T a_addr)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern FileSystemBlockHandle tsk_fs_block_get(FileSystemHandle fsinfo, IntPtr should_be_zero, long address);

        //void tsk_fs_block_free(TSK_FS_BLOCK * a_fs_block)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void tsk_fs_block_free(IntPtr fsblockptr);

        #endregion

        #region file stuff

        //TSK_FS_FILE *tsk_fs_file_open(TSK_FS_INFO * a_fs, TSK_FS_FILE * a_fs_file, const char *a_path)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern FileHandle tsk_fs_file_open(FileSystemHandle fs, [In] IntPtr should_be_zero, [In] string utf8path);

        //TSK_FS_FILE *tsk_fs_file_open_meta(TSK_FS_INFO * a_fs, TSK_FS_FILE * a_fs_file, TSK_INUM_T a_addr)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern FileHandle tsk_fs_file_open_meta(FileSystemHandle fs, [In] IntPtr should_be_zero, [In] long metadata_address);

        //ssize_t tsk_fs_file_read(TSK_FS_FILE * a_fs_file, TSK_OFF_T a_offset, char *a_buf, size_t a_len, TSK_FS_FILE_READ_FLAG_ENUM a_flags)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int tsk_fs_file_read(FileHandle ptr_fs_file, long offset, byte[] buf, int buf_len, FileReadFlag e);

        //uint8_t tsk_fs_file_walk(TSK_FS_FILE * a_fs_file, TSK_FS_FILE_WALK_FLAG_ENUM a_flags, TSK_FS_FILE_WALK_CB a_action, void *a_ptr)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte tsk_fs_file_walk(FileHandle ptr_fs_file, FileWalkFlag flag, FileContentWalkDelegate file_walk_cb, IntPtr a_ptr);

        //void tsk_fs_file_close(TSK_FS_FILE * a_fs_file)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void tsk_fs_file_close(IntPtr ptr_fs_file);

        //const TSK_FS_ATTR * tsk_fs_attrlist_get(const TSK_FS_ATTRLIST * a_fs_attrlist, TSK_FS_ATTR_TYPE_ENUM a_type)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern AttributeHandle tsk_fs_attrlist_get(IntPtr a_fs_attrlist, AttributeType a_type);

        //uint8_t hfs_cat_file_lookup(HFS_INFO * hfs, TSK_INUM_T inum, HFS_ENTRY * entry, unsigned char follow_hard_link)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte hfs_cat_file_lookup(IntPtr hfs, ulong inum, IntPtr entry, byte follow_hard_link);

        #endregion

        #region dir stuff

        //TSK_FS_DIR *tsk_fs_dir_open(TSK_FS_INFO * a_fs, const char *a_dir)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern DirectoryHandle tsk_fs_dir_open(FileSystemHandle file, string path);

        //TSK_FS_DIR *tsk_fs_dir_open_meta(TSK_FS_INFO * a_fs, TSK_INUM_T a_addr)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern DirectoryHandle tsk_fs_dir_open_meta(FileSystemHandle file, long metadata_address);

        //size_t tsk_fs_dir_getsize(const TSK_FS_DIR * a_fs_dir)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint tsk_fs_dir_getsize(DirectoryHandle fs_dir);

        //TSK_FS_FILE *tsk_fs_dir_get(const TSK_FS_DIR * a_fs_dir, size_t a_idx)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern FileHandle tsk_fs_dir_get(DirectoryHandle fs_dir, UIntPtr idx);

        //void tsk_fs_dir_close(TSK_FS_DIR * a_fs_dir)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void tsk_fs_dir_close(IntPtr handle);

        //uint8_t tsk_fs_dir_walk(TSK_FS_INFO * a_fs, TSK_INUM_T a_addr, TSK_FS_DIR_WALK_FLAG_ENUM a_flags, TSK_FS_DIR_WALK_CB a_action, void *a_ptr)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte tsk_fs_dir_walk(FileSystemHandle fs, long directory_address, DirWalkFlags walk_flags, DirWalkDelegate callback, IntPtr a_ptr);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "tsk_fs_dir_walk")]
        internal static extern byte tsk_fs_dir_walk_ptr(FileSystemHandle fs, long directory_address, DirWalkFlags walk_flags, DirWalkPtrDelegate callback, IntPtr a_ptr);

        
        #endregion

        #region meta

        //uint8_t tsk_fs_meta_walk(TSK_FS_INFO * a_fs, TSK_INUM_T a_start, TSK_INUM_T a_end, TSK_FS_META_FLAG_ENUM a_flags, TSK_FS_META_WALK_CB a_cb, void *a_ptr)
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte tsk_fs_meta_walk(FileSystemHandle fs, long start_address, long end_address, MetadataFlags walk_flags, MetaWalkDelegate callback, IntPtr a_ptr);

        #endregion

        #region error stuff

        //uint32_t tsk_error_get_errno()
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint tsk_error_get_errno();

        //char *tsk_error_get_errstr()
        [DllImport(NativeLibrary, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr tsk_error_get_errstr();

        #endregion

        #endregion

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FILE
    {
        IntPtr _ptr;
        int _cnt;
        IntPtr _base;
        int _flag;
        int _file;
        int _charbuf;
        int _bufsiz;
        IntPtr _tmpfname;
    };

    /// <summary>
    /// safe handle to work with TSK Disk Images (TSK_IMG_INFO*)
    /// </summary>
    internal class DiskImageHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DiskImageHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.tsk_img_close(this.handle);
            base.SetHandleAsInvalid();
            return true;
        }

        /// <summary>
        /// converts this pointer into an ImageInfo struct
        /// </summary>
        /// <returns></returns>
        internal TSK_IMG_INFO GetStruct()
        {
            return ((TSK_IMG_INFO)Marshal.PtrToStructure(this.handle, typeof(TSK_IMG_INFO)));
        }

        /// <summary>
        /// Opens a new handle to a VolumeSystem of the specified type, at the specified offset.
        /// </summary>
        /// <param name="vstype"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        internal VolumeSystemHandle OpenVolumeSystemHandle(VolumeSystemType vstype = VolumeSystemType.Autodetect, long offset = 0)
        {
            return NativeMethods.tsk_vs_open(this, offset, vstype);
        }

        /// <summary>
        /// Opens a new Filesystem handle of the specified filesystem type, at the specified offset.
        /// </summary>
        /// <param name="fstype"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        internal FileSystemHandle OpenFileSystemHandle(FileSystemType fstype = FileSystemType.Autodetect, long offset = 0)
        {
            return NativeMethods.tsk_fs_open_img(this, offset, fstype);
        }
    }

    /// <summary>
    /// safe handle to work with TSK Volume Systems (TSK_VS_INFO*)
    /// </summary>
    internal class VolumeSystemHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public VolumeSystemHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.tsk_vs_close(this.handle);
            base.SetHandleAsInvalid();
            return true;
        }

        /// <summary>
        /// Converts this handle into a new VolumeSystemInfo.
        /// </summary>
        /// <returns></returns>
        internal TSK_VS_INFO GetStruct()
        {
            if (this.IsInvalid)
                return new TSK_VS_INFO();
            else
                return ((TSK_VS_INFO)Marshal.PtrToStructure(this.handle, typeof(TSK_VS_INFO)));
        }
    }

    /// <summary>
    /// safe handle to work with TSK File Systems (TSK_FS_INFO*)
    /// </summary>
    internal class FileSystemHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public FileSystemHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.tsk_fs_close(this.handle);
            base.SetHandleAsInvalid();
            return true;
        }

        internal TSK_FS_INFO GetStruct()
        {
            if (this.IsInvalid)
            {
                return new TSK_FS_INFO();
            }
            else
            {
                return ((TSK_FS_INFO)Marshal.PtrToStructure(this.handle, typeof(TSK_FS_INFO)));
            }
        }

        internal EXT2FS_INFO GetStructExt2()
        {
            return ((EXT2FS_INFO)Marshal.PtrToStructure(this.handle, typeof(EXT2FS_INFO)));            
        }

        internal FATFS_INFO GetStructFat()
        {
            return ((FATFS_INFO)Marshal.PtrToStructure(this.handle, typeof(FATFS_INFO)));
        }

        internal ISO_INFO GetStructIso()
        {
            return ((ISO_INFO)Marshal.PtrToStructure(this.handle, typeof(ISO_INFO)));
        }

        internal FFS_INFO GetStructFfs()
        {
            return ((FFS_INFO)Marshal.PtrToStructure(this.handle, typeof(FFS_INFO)));
        }

        internal HFS_INFO GetStructHfs()
        {
            return ((HFS_INFO)Marshal.PtrToStructure(this.handle, typeof(HFS_INFO)));
        }
    }

    /// <summary>
    /// safe handle to work with TSK File Systems (TSK_FS_BLOCK*)
    /// </summary>
    internal class FileSystemBlockHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public FileSystemBlockHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.tsk_fs_block_free(this.handle);
            base.SetHandleAsInvalid();
            return true;
        }

        internal TSK_FS_BLOCK GetStruct()
        {
            if (this.IsInvalid)
                return new TSK_FS_BLOCK();
            else
                return ((TSK_FS_BLOCK)Marshal.PtrToStructure(this.handle, typeof(TSK_FS_BLOCK)));
        }
    }

    /// <summary>
    /// safe handle to work with TSK File (TSK_FS_FILE*)
    /// </summary>
    internal class FileHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public FileHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.tsk_fs_file_close(this.handle);
            base.SetHandleAsInvalid();
            return true;
        }

        internal TSK_FS_FILE GetStruct()
        {
            if (this.IsInvalid)
                return new TSK_FS_FILE();
            else
                return ((TSK_FS_FILE)Marshal.PtrToStructure(this.handle, typeof(TSK_FS_FILE)));
        }
    }

    /// <summary>
    /// safe handle to work with TSK Attribute (TSK_FS_ATTR*)
    /// </summary>
    internal class AttributeHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AttributeHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            base.SetHandleAsInvalid();
            return true;
        }

        internal TSK_FS_ATTR GetStruct()
        {
            if (this.IsInvalid)
                return new TSK_FS_ATTR();
            else
                return ((TSK_FS_ATTR)Marshal.PtrToStructure(this.handle, typeof(TSK_FS_ATTR)));
        }
    }

    /// <summary>
    /// safe handle to work with TSK Directory (TSK_FS_DIR*)
    /// </summary>
    internal class DirectoryHandle : SafeHandle
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DirectoryHandle()
            : base(IntPtr.Zero, true)
        {
        }

        /// <summary>
        /// invalid if pointer is zero
        /// </summary>
        public override bool IsInvalid
        {
            get
            {
                return base.handle == IntPtr.Zero;
            }
        }

        /// <summary>
        /// closes handle
        /// </summary>
        /// <returns></returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.tsk_fs_dir_close(this.handle);
            base.SetHandleAsInvalid();
            return true;
        }

        internal TSK_FS_DIR GetStruct()
        {
            if (this.IsInvalid)
                return new TSK_FS_DIR();
            else
                return ((TSK_FS_DIR)Marshal.PtrToStructure(this.handle, typeof(TSK_FS_DIR)));
        }
    }
}