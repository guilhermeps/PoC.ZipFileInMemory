using System;
using System.Collections.Generic;
using System.IO;

namespace PoC.ZipFileInMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating a zip file from many files...");
            IDictionary<string, byte[]> filesToZip = new Dictionary<string, byte[]>
            {
                { "br.png", File.ReadAllBytes($@"{ Environment.CurrentDirectory }\Images\br.png") },
                { "minas_gerais.png", File.ReadAllBytes($@"{ Environment.CurrentDirectory }\Images\minas_gerais.png")}
            };
            byte[] zippedFile = ZipFileFromByteArrayList.Create(filesToZip);

            File.WriteAllBytes($@"{ Environment.CurrentDirectory }\zippedFile.zip", zippedFile);

            Console.WriteLine("Done...");
            Console.WriteLine("Generating a zip file from another zip file...");

            byte[] newZippedFile = ZipFileFromAnotherZipFile.Create(zippedFile);

            Console.WriteLine("Done...");
            Console.ReadKey();
        }
    }
}
