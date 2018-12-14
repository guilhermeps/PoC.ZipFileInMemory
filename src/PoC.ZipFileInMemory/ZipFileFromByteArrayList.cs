using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace PoC.ZipFileInMemory
{
    /// <summary>
    /// Create a zip file from a list of byte arrays.
    /// </summary>
    public static class ZipFileFromByteArrayList
    {
        /// <summary>
        /// Create a file zip with many files inside it.
        /// </summary>
        /// <param name="filesToZip">Dictionary with the full file name as item key and 
        /// byte array of the file to compress.</param>
        /// <returns>Byte array from new zip file.</returns>
        public static byte[] Create(IDictionary<string, byte[]> filesToZip)
        {
            if (filesToZip == null || filesToZip?.Count == 0)
                throw new ArgumentException("File zip list must have items.");

            try
            {
                byte[] zipByteArray = null;

                // MemoryStream for your new zip file
                using(MemoryStream memoStream = new MemoryStream())
                {
                    // Creation of the new zip file
                    using (ZipArchive zip = new ZipArchive(memoStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var file in filesToZip)
                        {
                            ZipArchiveEntry newItemInZip = zip.CreateEntry(file.Key); // full filename
                            using (MemoryStream fileMemoStream = new MemoryStream(file.Value)) // byte array
                            {
                                using (Stream zipEntryStream = newItemInZip.Open())
                                {
                                    fileMemoStream.CopyTo(zipEntryStream);
                                }
                            }
                        }
                    }
                    zipByteArray = memoStream.ToArray();
                }
                return zipByteArray;
            }
            catch (Exception ex)
            {
                // something went very bad
                throw ex;
            }
        }
    }
}