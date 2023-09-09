using System;
using JookoObfuscate.Encryption;
using JookoObfuscate.AntiTamper;

namespace JookoObfuscate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to JookoObfuscate!");

            // Initialize the StrongEncryption class
            StrongEncryption strongEncryption = new();

            // Initialize the AntiTamperMechanisms class
            AntiTamperMechanisms antiTamper = new();

            // Perform Anti-Tamper Check
            string filePath = "sample.exe";  // replace this with actual executable path
            byte[] key = strongEncryption.GenerateRandomKey();
            byte[] iv = strongEncryption.GenerateRandomIV();

            // Check for file tampering
            string fileHash = antiTamper.GenerateFileHash(filePath);
            string encryptedHash = Convert.ToBase64String(strongEncryption.EncryptStringToBytes(fileHash, key, iv));

            bool isTampered = antiTamper.IsFileTampered(filePath, encryptedHash, key, iv);

            if (isTampered)
            {
                Console.WriteLine("File has been tampered with!");
                antiTamper.ReactToTampering();
            }
            else
            {
                Console.WriteLine("File is safe.");

                // If the file is not tampered, perform strong encryption on the file
                strongEncryption.EncryptFile(filePath, key, iv);
                Console.WriteLine("File has been strongly encrypted.");
            }
        }
    }
}