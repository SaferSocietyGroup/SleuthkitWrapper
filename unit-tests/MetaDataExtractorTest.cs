namespace SleuthkitSharp_UnitTests
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    using NUnit.Framework;

    using SleuthKit;

    /// <summary>
    /// This is a test class for MetaDataExtractorTest and is intended
    /// to contain all MetaDataExtractorTest Unit Tests
    /// </summary>
    [TestFixture]
    public class MetaDataExtractorTest
    {
        #region Public Methods and Operators

        /// <summary>
        /// A test for Extracting metadata from the forensic image
        /// </summary>
        [Test]
        public void ExtractTest()
        {
            string fileName = ConfigurationManager.AppSettings["E01Path"];
            
            // fill up dictionary with the expected values.
            IDictionary<string, string> expected = new Dictionary<string, string>();

            expected.Add("case_number", "UNKNOWN");

            // expected.Add("compression_method", "deflate");
            expected.Add("description", "USB-disk-image-FAT");
            expected.Add("examiner_name", "TheExaminerJohnL");
            expected.Add("evidence_number", "UNKNOWN");
            expected.Add("notes", "UNKNOWN");
            expected.Add("acquiry_operating_system", "Windows 2008 Server R2");
            expected.Add("acquiry_software_version", "7.06.02");
            expected.Add("password", "UNKNOWN");
            expected.Add("compression_level", "UNKNOWN");
            expected.Add("model", "Flash Disk");
            expected.Add("serial_number", "FBF1101120300452");
            expected.Add("device_label", "USB");
            expected.Add("process_identifier", "UNKNOWN");
            expected.Add("unknown_dc", "UNKNOWN");
            expected.Add("extents", "0");
            expected.Add("Format", "7");
            expected.Add("Number_Of_Sectors", "3915776");
            expected.Add("Bytes_Per_Sector", "512");
            expected.Add("Number_Of_Tracks", "0");
            expected.Add("Chunk_Size", "32768");
            expected.Add("Sectors_Per_Chunk", "64");
            expected.Add("Error_Granularity", "64");
            expected.Add("Media_Size", "2004877312");
            expected.Add("Media_Type", "17");
            expected.Add("Number_Of_Sessions", "0");
            expected.Add("SHA1", "99ddf8e48bd7fe507e370b232aec4e123e8dae44");

            // get the metadata from USB-disk-image-FAT.Ex01
            IDictionary<string, string> actual = MetaDataExtractor.Extract(Path.GetFullPath(fileName));

            CollectionAssert.AreEquivalent(expected, actual);
        }

        #endregion
    }
}