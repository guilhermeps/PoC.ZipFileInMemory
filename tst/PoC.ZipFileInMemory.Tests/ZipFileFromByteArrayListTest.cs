using PoC.ZipFileInMemory;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PoC.ZipFileInMemory.Tests
{
    /// <summary>
    /// Class to run test scenarios for ZipFileFromByteArrayList.
    /// </summary>
    public class ZipFileFromByteArrayListTest
    {
        [Theory]
        [ClassData(typeof(ByteArrayListTestInvalidData))]
        public void ShouldThrowExceptionForInvalidParameter(IDictionary<string, byte[]> files)
        {
            Assert.Throws<ArgumentException>(() => ZipFileFromByteArrayList.Create(files));
        }

        [Fact]
        public void ShouldReturnAByteArrayFromGeneratedFileZip()
        {
            // Arrange
            IDictionary<string, byte[]> filesToZip = new Dictionary<string, byte[]>
            {
                { "br.png", File.ReadAllBytes($@"{ Environment.CurrentDirectory }\Images\br.png") },
                { "minas_gerais.png", File.ReadAllBytes($@"{ Environment.CurrentDirectory }\Images\minas_gerais.png")}
            };
            
            // Act
            byte[] zippedFileByteArray = ZipFileFromByteArrayList.Create(filesToZip);

            // Assert
            Assert.True(zippedFileByteArray.Length > 0);
        }

        /// <summary>
        /// Invalid test data for files to be zipped.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="byte[]"></typeparam>
        private class ByteArrayListTestInvalidData : TheoryData<IDictionary<string, byte[]>>
        {
            public ByteArrayListTestInvalidData()
            {
                Add(null);
                Add(new Dictionary<string, byte[]>());
            }
        }
    }
}
