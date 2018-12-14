using System;
using System.IO;
using System.IO.Compression;

namespace PoC.ZipFileInMemory
{
    /// <summary>
    /// This class unzip a file, read all entries inside it, and copy those entries to a new
    /// zip file.
    /// 
    /// Why? 
    /// Sometimes there is a need to do this job, specially when there is a context that
    /// you are consuming a service who deliver a zip file and for business needs you must
    /// deliver it in a specific zip.
    /// </summary>
    public static class ZipFileFromAnotherZipFile
    {
        /// <summary>
        /// Get an byte array from the zip file and copy the entries a new zip file.
        /// </summary>
        /// <param name="zipToRead">Byte array of zip file to read.</param>
        /// <returns>Byte array of the new zip file.</returns>
        public static byte[] Create(byte[] zipToRead)
        {
            byte[] newZipFileByteArray = null;

            if (zipToRead == null || zipToRead.Length == 0)
                throw new ArgumentException("Invalid byte array.");
            
            try
            {
                Stream zipFileToReadStream = new MemoryStream(zipToRead);
                using (var zipFile = new ZipArchive(zipFileToReadStream, ZipArchiveMode.Read))
                {
                    // MemoryStream for your new zip file
                    using(MemoryStream memoStream = new MemoryStream())
                    {
                        // Creation of the new zip file
                        using (ZipArchive newZipFile = new ZipArchive(memoStream, ZipArchiveMode.Create, true))
                        {
                            // Here we copy the entries for you new zip file
                            // Here is where the stuff happen
                            foreach (var entry in zipFile.Entries)
                            {
                                using (var stream = entry.Open())
                                {
                                    ZipArchiveEntry newItemOnZip = newZipFile.CreateEntry(entry.FullName);
                                    using (Stream zipEntryStream = newItemOnZip.Open())
                                    {
                                        stream.CopyTo(zipEntryStream); // That's it
                                    }       
                                }
                            }
                        }
                        newZipFileByteArray = memoStream.ToArray();
                    }
                }
                return newZipFileByteArray;
            }
            catch (Exception ex)
            {
                // something went really bad
                throw ex;
            }
        }
    }
}