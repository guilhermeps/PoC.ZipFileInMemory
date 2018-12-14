using PoC.ZipFileInMemory;
using System;
using System.Collections.Generic;
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
