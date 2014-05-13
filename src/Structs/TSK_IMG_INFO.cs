using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SleuthKit.Structs
{
    /// <summary>
    /// represents TSK_IMG_INFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TSK_IMG_INFO
    {
        /// <summary>
        /// Set to TSK_IMG_INFO_TAG when struct is alloc
        /// </summary>
        internal uint tag;

        /// <summary>
        /// Type of disk image format
        /// </summary>
        internal ImageType itype;

        /// <summary>
        /// Total size of image in bytes
        /// </summary>
        internal long size;

        /// <summary>
        /// sector size of device in bytes (typically 512)
        /// </summary>
        internal uint sector_size;

        /// <summary>
        /// page size of NAND page in bytes (defaults to 2048)
        /// </summary>
        internal uint page_size;

        /// <summary>
        /// spare or OOB size of NAND in bytes (defaults to 64)
        /// </summary>
        internal uint spare_size;

        /// <summary>
        /// Place holder for lock of cache and associated values
        /// </summary>
        internal tsk_lock_t cacheLock;

        //#define TSK_IMG_INFO_CACHE_NUM  4
        //#define TSK_IMG_INFO_CACHE_LEN  65536
        IntPtr cache;//        char[][] cache[4][65536];     ///< read cache

        #region function pointers

        IntPtr cache_off;//TSK_OFF_T cache_off[TSK_IMG_INFO_CACHE_NUM];    ///< starting byte offset of corresponding cache entry
        IntPtr cache_age; //int cache_age[TSK_IMG_INFO_CACHE_NUM];  ///< "Age" of corresponding cache entry, higher means more recently used
        IntPtr cache_len; //size_t cache_len[TSK_IMG_INFO_CACHE_NUM];       ///< Length of cache entry used (0 if never used)

        IntPtr read;//ssize_t(*read) (TSK_IMG_INFO * img, TSK_OFF_T off, char *buf, size_t len);     ///< \internal External progs should call tsk_img_read() 
        IntPtr close;//void (*close) (TSK_IMG_INFO *); ///< \internal Progs should call tsk_img_close()
        IntPtr imgstat;//void (*imgstat) (TSK_IMG_INFO *, FILE *);       ///< Pointer to file type specific function 

        #endregion

        /// <summary>
        /// Checks that size and sector_size are non-zero.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return size != 0 && sector_size != 0;
            }
        }
    };
}
