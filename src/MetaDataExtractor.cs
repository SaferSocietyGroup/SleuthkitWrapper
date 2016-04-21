using EWF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace SleuthKit
{
    /// <summary>
    /// A class to extract meta data from E01 file
    /// </summary>
    public class MetaDataExtractor
    {
        /// <summary>
        /// Extracts meta data of E01 at the time of development
        /// </summary>
        /// <param name="fileName">
        /// absolute file path
        /// </param>
        /// <returns>
        /// The <see cref="IDictionary{TKey, TValue}"/>.
        /// </returns>
        public static IDictionary<string, string> Extract(string fileName)
        {
            var handle = new Handle();

            // dictionary/map to store the metadata as key value pairs
            var metaDataDictionary = new Dictionary<string, string>();

            try
            {
                handle.Open(new string[] { fileName }, 1);

                // get the headers available in the file.
                int headerCount = handle.GetNumberOfHeaderValues();

                // loop through the headers and extract the headers and thier respective values
                for (int i = 0; i < headerCount; i++)
                {
                    try
                    {
                        // extract the header
                        string headerId = handle.GetHeaderValueIdentifier(i);

                        // get the header value
                        string headerValue = handle.GetHeaderValue(headerId);

                        // add to dictionary. id value NOT FOUND. enter UNKNOWN
                        metaDataDictionary.Add(headerId, string.IsNullOrEmpty(headerValue) ? "UNKNOWN" : headerValue);
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine("ERROR : Couldnt Retrieve info due to error:{0}", exception);
                    }
                }

                // get/extract meta data via the handle returned from ewflib
                metaDataDictionary.Add("Format", handle.GetFormat().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Number_Of_Sectors", handle.GetNumberOfSectors().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Bytes_Per_Sector", handle.GetBytesPerSector().ToString(CultureInfo.InvariantCulture));

                metaDataDictionary.Add("Number_Of_Tracks", handle.GetNumberOfTracks().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Chunk_Size", handle.GetChunkSize().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Sectors_Per_Chunk", handle.GetSectorsPerChunk().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Error_Granularity", handle.GetErrorGranularity().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Media_Size", handle.GetMediaSize().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Media_Type", handle.GetMediaType().ToString(CultureInfo.InvariantCulture));
                metaDataDictionary.Add("Number_Of_Sessions", handle.GetNumberOfSessions().ToString(CultureInfo.InvariantCulture));

                metaDataDictionary.Add(
                    handle.GetHashValueIdentifier(0), handle.GetHashValue(handle.GetHashValueIdentifier(0)));

                return metaDataDictionary;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("ERROR : Couldnt Retrieve info due to error:{0}", exception);
                return metaDataDictionary;
            }
            finally
            {
                handle.Close();
                handle.Dispose();
            }
        }
    }
}
