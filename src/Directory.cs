using SleuthKit.Structs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SleuthKit
{
    /// <summary>
    /// A directory! What else..  Roughly equivalent to TSK_FS_DIR
    /// </summary>
    public class Directory : FileSystemEntry, IDisposable
    {
        private FileSystem _fs;
        private DirectoryHandle _handle;
        private TSK_FS_DIR _struct;
        private int? ec;
        private Directory _parentDir;
        private string path = null;

        internal bool isRoot;

        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        internal Directory(FileSystem fs, DirectoryHandle dh, Directory parent = null, string name = null)
        {
            this._fs = this.FileSystem = fs;
            this._handle = dh;
            this._struct = dh.GetStruct();
            this._parentDir = parent;
            
            var fileStruct = (TSK_FS_FILE)Marshal.PtrToStructure(_struct.fs_dir, typeof(TSK_FS_FILE));

            if (fileStruct.Metadata.HasValue)
            {
                var m = fileStruct.Metadata.Value;
                this.Address = m.Address;
                this.CreationTime = epoch.AddSeconds(m.CRTime);
                this.LastWriteTime = epoch.AddSeconds(m.MTime);
                this.LastAccessTime = epoch.AddSeconds(m.ATime);
                this.MetadataWriteTime = epoch.AddSeconds(m.CTime);
            }

            if (name == null)
            {
                if (fileStruct.Name.HasValue)
                {
                    this.Name = fileStruct.Name.Value.GetName();
                }
                else
                {
                    this.Name = string.Empty;
                }
            }
            else
            {
                this.Name = name;
            }
        }

        #region Properties
        
        /// <summary>
        /// The full path
        /// </summary>
        public string Path
        {
            get
            {
                if (this.isRoot)
                {
                    return null;
                }

                if (this.path == null)
                {
                    var buf = new StringBuilder();

                    if (this._parentDir != null)
                    {
                        var pp = _parentDir.Path;
                        buf.Append(pp);
                        buf.Append("/");
                    }

                    buf.Append(this.Name);

                    this.path = buf.ToString();
                }

                return this.path;
            }
        }

        /// <summary>
        /// Number of entries in this directory
        /// </summary>
        public int EntryCount
        {
            get
            {
                if (!ec.HasValue)
                {
                    ec = (int)NativeMethods.tsk_fs_dir_getsize(this._handle);
                }

                return ec.GetValueOrDefault(0);
            }
        }

        #endregion

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            this._handle.Dispose();
        }

        /// <summary>
        /// open file
        /// </summary>
        /// <returns></returns>
        public TSK_FS_FILE GetFileStruct()
        {
            var fs = (TSK_FS_FILE)Marshal.PtrToStructure(this._struct.fs_dir, typeof(TSK_FS_FILE));
            return fs;
        }

        #region enumerations

        /// <summary>
        /// Lists the entries in this directory.
        /// </summary>
        public IEnumerable<TSK_FS_NAME> EntryNames
        {
            get
            {
                for (int a = 0; a < _struct.names_used; a++)
                {
                    yield return _struct.ptr_list_names.ElementAt<TSK_FS_NAME>(a);
                }
            }
        }

        /// <summary>
        /// Files
        /// </summary>
        public IEnumerable<File> Files
        {
            get
            {
                foreach (var e in this.Entries) //no need to check validity, its already been established by Entries property
                {
                    if (e.Metadata.HasValue && (e.Metadata.Value.MetadataType == MetadataType.Regular) && e.Name.HasValue)
                    {
                        var fn = e.Name.Value.GetName();
                        var kidpath = string.Join("\\", new string[] { this.Path, fn }); ;
                        var f = this._fs.OpenFile(kidpath, this);
                        if (f != null)
                        {
                            yield return f;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Directories
        /// </summary>
        public IEnumerable<Directory> Directories
        {
            get
            {
                var path = this.Path;

                foreach (var e in this.Entries) //no need to check validity, its already been established by Entries property
                {
                    if (e.Metadata.HasValue && (e.Metadata.Value.MetadataType == MetadataType.Directory) && e.Name.HasValue)
                    {
                        var fn = e.Name.Value.GetName();

                        if (fn == "." || fn == "..")
                            continue;

                        var addr = e.Metadata.Value.Address;

                        var d = this.FileSystem.OpenDirectory(addr, this);

                        if (d != null)
                        {
                            yield return d;
                        }
                    }
                }
            }
        }

        internal Directory OpenSubdirectory(string name)
        {
            var kidpath = name;
            if (this.Path != "")
            {
                kidpath = string.Join(@"/", new string[] { this.Path, name }); ;
            }

            return this.FileSystem.OpenDirectory(kidpath, this);
        }

        internal Directory OpenSubdirectory(long address)
        {
            return this.FileSystem.OpenDirectory(address, this);
        }

        /// <summary>
        /// The entries in this directory
        /// </summary>
        public IEnumerable<TSK_FS_FILE> Entries
        {
            get
            {
                var ec = this.EntryCount;
                for (var a = 0; a < ec; a++)
                {
                    var fh = NativeMethods.tsk_fs_dir_get(this._handle, new UIntPtr((uint)a));

                    if (!fh.IsInvalid)
                    {
                        var fstr = fh.GetStruct();
                        yield return fstr;
                    }

                    if (!fh.IsClosed)
                        fh.Close();
                }
            }
        }

        #endregion
    }
}
