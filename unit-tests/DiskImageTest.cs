namespace SleuthkitSharp_UnitTests
{
    using NUnit.Framework;
    using SleuthKit;
    using SleuthKit.Structs;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using Directory = SleuthKit.Directory;
    using File = SleuthKit.File;

    /// <summary>
    ///     This is a test class for DiskImageTest and is intended
    ///     to contain all DiskImageTest Unit Tests
    /// </summary>
    [TestFixture]
    public class DiskImageTest
    {
        #region Static Fields

        private static readonly IList<string> FilePaths = new List<string>();

        private static int allFileCount;

        /// <summary>
        ///     The file count.
        /// </summary>
        private static int jpgFileCount;

        #endregion Static Fields

        #region Fields

        /// <summary>
        ///     The disk image.
        /// </summary>
        private DiskImage diskImage;

        #endregion Fields

        #region Public Methods and Operators

        [SetUp]
        public void Init()
        {
            allFileCount = 0;
            jpgFileCount = 0;

            // empty the list
            FilePaths.Clear();
        }

        /// <summary>
        ///     A test for GetFileSystems
        /// </summary>
        [Test]
        public void GetFileSystemsTest()
        {
            IEnumerable<FileSystem> actual = this.diskImage.GetFileSystems();
            IEnumerable<FileSystem> fileSystems = actual as FileSystem[] ?? actual.ToArray();
            Assert.AreEqual(3, fileSystems.Count());
            foreach (FileSystem fileSystem in fileSystems)
            {
                fileSystem.Dispose();
            }
        }

        /// <summary>
        ///     A test for OpenFileSystem
        /// </summary>
        [Test]
        public void OpenFileSystemTest()
        {
            const FileSystemType Fstype = FileSystemType.Autodetect;
            const long Offset = 16384;
            using (FileSystem actual = this.diskImage.OpenFileSystem(Fstype, Offset))
            {
                Assert.AreEqual(3915744, actual.BlockCount);
                Assert.AreEqual(512, actual.BlockSize);
                Assert.AreEqual(0, actual.FirstBlock);
                Assert.AreEqual(3915743, actual.LastBlock);

                jpgFileCount = 0;
                actual.WalkDirectories(FileCount_DirectoryWalkCallback, DirWalkFlags.Recurse | DirWalkFlags.Allocated);
                Assert.AreEqual(30, jpgFileCount);

                jpgFileCount = 0;
                actual.WalkDirectories(FileCount_DirectoryWalkCallback, DirWalkFlags.Recurse | DirWalkFlags.Unallocated);
                Assert.AreEqual(30, jpgFileCount);
            }
        }

        /// <summary>
        ///     A test for OpenRead
        /// </summary>
        [Test]
        public void OpenReadTest()
        {
            using (Stream stream = this.diskImage.OpenRead())
            {
                Assert.IsNotNull(stream);
                Assert.AreEqual(2004877312, stream.Length);
            }
        }

        [Test]
        public void OpenSingleFileFromVolume()
        {
            int volumeAddress = 2;
            String filepath = @"A folder/370076.jpg";

            using (VolumeSystem volumeSystem = this.diskImage.OpenVolumeSystem())
            {
                Volume volume = volumeSystem.Volumes.SingleOrDefault(v => v.Address == volumeAddress);

                Assert.NotNull(volume);

                using (FileSystem fileSystem = volume.OpenFileSystem())
                {
                    using (File file = fileSystem.OpenFile(filepath))
                    {
                        Assert.NotNull(file);
                        Assert.AreEqual(32061, file.Size);
                        Assert.AreEqual(FilesystemNameType.Regular, file.FileStruct.Name.Value.Type);
                    }
                }
            }
        }

        [Test]
        public void OpenSingleFileAddressFromVolume()
        {
            int volumeAddress = 2;
            long fileAddress = 518;

            using (VolumeSystem volumeSystem = this.diskImage.OpenVolumeSystem())
            {
                Volume volume = volumeSystem.Volumes.SingleOrDefault(v => v.Address == volumeAddress);

                Assert.NotNull(volume);

                using (FileSystem fileSystem = volume.OpenFileSystem())
                {
                    using (File file = fileSystem.OpenFile(fileAddress))
                    {
                        Assert.NotNull(file);
                        Assert.AreEqual(38947, file.Size);
                    }
                }
            }
        }

        /// <summary>
        ///     A test for OpenVolumeSystem
        /// </summary>
        [Test]
        public void OpenVolumeSystemAndCountFiles()
        {
            using (VolumeSystem volumeSystem = this.diskImage.OpenVolumeSystem())
            {
                Assert.AreEqual(3, volumeSystem.PartitionCount);
                Assert.AreEqual(1, volumeSystem.AllocatedPartitionCount);
                Assert.AreEqual(3, volumeSystem.Volumes.Count());

                int count = 0;
                foreach (Volume volume in volumeSystem.Volumes)
                {
                    if (volume.Description == "Primary Table (#0)" ||
                        volume.Description == "Unallocated")
                    {
                        continue;
                    }

                    using (FileSystem fileSystem = volume.OpenFileSystem())
                    {
                        //count += CountFiles(fileSystem.OpenRootDirectory());
                        fileSystem.WalkDirectories(
                            FileCount_DirectoryWalkCallback,
                            DirWalkFlags.Recurse | DirWalkFlags.Unallocated);
                    }
                }

                Assert.AreEqual(30, jpgFileCount);
                //I think it should be 63 //Bala- Autopsy shows me that there are only 30 files
                Assert.AreEqual(37, allFileCount);
            }
        }

        [Test]
        public void OpenVolumeSystemAndFindFiles()
        {
            int volumeAdress = 2;

            using (VolumeSystem volumeSystem = this.diskImage.OpenVolumeSystem())
            {
                Volume volume = volumeSystem.Volumes.SingleOrDefault(v => v.Address == volumeAdress);

                Assert.NotNull(volume);

                using (FileSystem fileSystem = volume.OpenFileSystem())
                {
                    //count += CountFiles(fileSystem.OpenRootDirectory());
                    fileSystem.WalkDirectories(
                        FindFiles_DirectoryWalkCallback,
                        DirWalkFlags.Recurse | DirWalkFlags.Unallocated);
                }

                Assert.AreEqual(37, FilePaths.Count()); //I think it should be 63 || Bala: Autopsy shows me that there are only 30 files
            }
        }

        /// <summary>
        ///     A test for SectorSize
        /// </summary>
        [Test]
        public void SectorSizeTest()
        {
            Assert.AreEqual(512, this.diskImage.SectorSize);
        }

        /// <summary>
        ///     Sets up disk image tests.
        /// </summary>
        [SetUp]
        public void SetUpDiskImageTests()
        {
            var file = new FileInfo(ConfigurationManager.AppSettings["E01Path"]);
            this.diskImage = new DiskImage(file);
        }

        /// <summary>
        ///     A test for Size
        /// </summary>
        [Test]
        public void SizeTest()
        {
            Assert.AreEqual(2004877312, this.diskImage.Size);
        }

        /// <summary>
        ///     Tears down disk image tests.
        /// </summary>
        [TearDown]
        public void TearDownDiskImageTests()
        {
            if (this.diskImage != null)
            {
                this.diskImage.Dispose();
            }
        }

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>
        ///     Callback function that is called for each file name during directory walk. (FileSystem.WalkDirectories)
        /// </summary>
        /// <param name="file">
        ///     The file struct.
        /// </param>
        /// <param name="directoryPath">
        ///     The directory path.
        /// </param>
        /// <param name="dataPtr">
        ///     Pointer to data that is passed to the callback function each time.
        /// </param>
        /// <returns>
        ///     Value to control the directory walk.
        /// </returns>
        private static WalkReturnEnum FileCount_DirectoryWalkCallback(
            ref TSK_FS_FILE file,
            string directoryPath,
            IntPtr dataPtr)
        {
            if (file.Name.ToString().Contains("jpg"))
            {
                jpgFileCount++;
            }
            allFileCount++;
            return WalkReturnEnum.Continue;
        }

        /// <summary>
        ///     Callback function that is called for each file name during directory walk. (FileSystem.WalkDirectories)
        /// </summary>
        /// <param name="file">
        ///     The file struct.
        /// </param>
        /// <param name="directoryPath">
        ///     The directory path.
        /// </param>
        /// <param name="dataPtr">
        ///     Pointer to data that is passed to the callback function each time.
        /// </param>
        /// <returns>
        ///     Value to control the directory walk.
        /// </returns>
        private static WalkReturnEnum FindFiles_DirectoryWalkCallback(
            ref TSK_FS_FILE file,
            string directoryPath,
            IntPtr dataPtr)
        {
            FilePaths.Add(string.Format("{0}{1}", directoryPath, file.Name));
            return WalkReturnEnum.Continue;
        }

        private int CountFiles(Directory directory)
        {
            int count = 0;

            if (directory == null)
            {
                return 0;
            }

            foreach (Directory subDirectory in directory.Directories)
            {
                count += this.CountFiles(subDirectory);
            }

            count += directory.Files.Count();

            return count;
        }

        private IEnumerable<String> FindFiles(Directory directory)
        {
            if (directory != null)
            {
                foreach (Directory subDirectory in directory.Directories)
                {
                    foreach (String path in this.FindFiles(subDirectory))
                    {
                        yield return path;
                    }
                }

                foreach (File file in directory.Files)
                {
                    yield return file.Path;
                }
            }
        }

        #endregion Methods
    }
}