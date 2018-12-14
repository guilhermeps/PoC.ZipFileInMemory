using PoC.ZipFileInMemory;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PoC.ZipFileInMemory.Tests
{
    public class ZipFileFromAnotherZipFileTest
    {
        [Theory]
        [ClassData(typeof(ByteArrayTestInvalidData))]
        public void ShouldThrowExceptionForInvalidParameter(byte[] file)
        {
            Assert.Throws<ArgumentException>(() => ZipFileFromAnotherZipFile.Create(file));
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
            byte[] zippedFileByteArray = ZipFileFromByteArrayList.Create(filesToZip);

            // Act
            byte[] newZippedFileByteArray = ZipFileFromAnotherZipFile.Create(zippedFileByteArray);

            // Assert
            Assert.True(newZippedFileByteArray.Length > 0);
        }

        /// <summary>
        /// Invalid test data for files to be zipped.
        /// </summary>
        /// <typeparam name="byte[]"></typeparam>
        private class ByteArrayTestInvalidData : TheoryData<byte[]>
        {
            public ByteArrayTestInvalidData()
            {
                Add(null);
                Add(new byte[0]);
            }
        }
    }
}