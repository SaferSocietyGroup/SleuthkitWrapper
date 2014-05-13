using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SleuthKit.Structs
{
    [StructLayout(LayoutKind.Explicit,
#if Bit32
    Size = 4760
#elif Bit64
    Size = 4968
#endif
    )]
    public struct HFS_INFO
    {
        [FieldOffset(0)]
        TSK_FS_INFO fs_info;

#if Bit32
        [FieldOffset(280)]
#elif Bit64
        [FieldOffset(408)]
#endif
        IntPtr fs_ptr;

#if Bit32
        [FieldOffset(284)]
#elif Bit64
        [FieldOffset(416)]
#endif
        char is_case_sensitive;

#if Bit32
        [FieldOffset(288)]
#elif Bit64
        [FieldOffset(424)]
#endif
        tsk_lock_t protection_lock;

#if Bit32
        [FieldOffset(312)]
#elif Bit64
        [FieldOffset(464)]
#endif
        IntPtr blockmap_file_ptr;

#if Bit32
        [FieldOffset(316)]
#elif Bit64
        [FieldOffset(472)]
#endif
        IntPtr blockmap_attr_ptr;

#if Bit32
        [FieldOffset(320)]
#elif Bit64
        [FieldOffset(480)]
#endif
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4096)]
        byte[] blockmap_cache;

#if Bit32
        [FieldOffset(4416)]
#elif Bit64
        [FieldOffset(4576)]
#endif
        long blockmap_cache_start;

#if Bit32
        [FieldOffset(4424)]
#elif Bit64
        [FieldOffset(4584)]
#endif
        UIntPtr blockmap_cache_len;

#if Bit32
        [FieldOffset(4428)]
#elif Bit64
        [FieldOffset(4592)]
#endif
        IntPtr catalog_file_ptr;

#if Bit32
        [FieldOffset(4432)]
#elif Bit64
        [FieldOffset(4600)]
#endif
        IntPtr catalog_attr_ptr;

#if Bit32
        [FieldOffset(4436)]
#elif Bit64
        [FieldOffset(4608)]
#endif
        tsk_lock_t catalog_header; //TODO: type

#if Bit32
        [FieldOffset(4544)]
#elif Bit64
        [FieldOffset(4720)]
#endif
        IntPtr extents_file_ptr;

#if Bit32
        [FieldOffset(4548)]
#elif Bit64
        [FieldOffset(4728)]
#endif
        IntPtr extents_attr_ptr;

#if Bit32
        [FieldOffset(4552)]
#elif Bit64
        [FieldOffset(4736)]
#endif
        tsk_lock_t extents_header; //TODO: type

#if Bit32
        [FieldOffset(4664)]
#elif Bit64
        [FieldOffset(4848)]
#endif
        long hfs_wrapper_offset;

#if Bit32
        [FieldOffset(4672)]
#elif Bit64
        [FieldOffset(4856)]
#endif
        long root_crtime;

#if Bit32
        [FieldOffset(4680)]
#elif Bit64
        [FieldOffset(4864)]
#endif
        long meta_crtime;

#if Bit32
        [FieldOffset(4688)]
#elif Bit64
        [FieldOffset(4872)]
#endif
        long metadir_crtime;

#if Bit32
        [FieldOffset(4696)]
#elif Bit64
        [FieldOffset(4880)]
#endif
        bool has_root_crtime;

#if Bit32
        [FieldOffset(4697)]
#elif Bit64
        [FieldOffset(4881)]
#endif
        bool has_meta_crtime;

#if Bit32
        [FieldOffset(4698)]
#elif Bit64
        [FieldOffset(4882)]
#endif
        bool has_meta_dir_crtime;

#if Bit32
        [FieldOffset(4704)]
#elif Bit64
        [FieldOffset(4888)]
#endif
        ulong meta_inum;

#if Bit32
        [FieldOffset(4712)]
#elif Bit64
        [FieldOffset(4896)]
#endif
        ulong meta_dir_inum;

#if Bit32
        [FieldOffset(4720)]
#elif Bit64
        [FieldOffset(4904)]
#endif
        IntPtr meta_dir_ptr;

#if Bit32
        [FieldOffset(4724)]
#elif Bit64
        [FieldOffset(4912)]
#endif
        IntPtr dir_meta_dir_ptr;

#if Bit32
        [FieldOffset(4728)]
#elif Bit64
        [FieldOffset(4920)]
#endif
        tsk_lock_t metadata_dir_cache_lock;

#if Bit32
        [FieldOffset(4752)]
#elif Bit64
        [FieldOffset(4960)]
#endif
        bool has_extents_file;

#if Bit32
        [FieldOffset(4753)]
#elif Bit64
        [FieldOffset(4961)]
#endif
        bool has_startup_file;

#if Bit32
        [FieldOffset(4754)]
#elif Bit64
        [FieldOffset(4962)]
#endif
        bool has_attributes_file;

        public IEnumerable<TSK_FS_ATTR> CatalogAttributes
        { 
            get
            {
                if (catalog_attr_ptr != IntPtr.Zero)
                {
                    TSK_FS_ATTR current = ((TSK_FS_ATTR)Marshal.PtrToStructure(catalog_attr_ptr, typeof(TSK_FS_ATTR)));

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
