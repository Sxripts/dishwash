using System;
using System.IO;
using JookoObfuscate.Encryption;
using JookoObfuscate.AntiTamper;
using JookoObfuscate.Metrics;

namespace JookoObfuscate.Obfuscation
{
    /// <summary>
    /// Class responsible for performing basic obfuscation on external executable files.
    /// </summary>
    public class FileObfuscator
    {
        private readonly StrongEncryption strongEncryption;
        private readonly AntiTamperMechanisms antiTamperMechanisms;

        public FileObfuscator()
        {
            strongEncryption = new StrongEncryption();
            antiTamperMechanisms = new AntiTamperMechanisms();
        }

        /// <summary>
        /// Obfuscate a file.
        /// </summary>
        /// <param name="filePath">The file to obfuscate.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="iv">Initialization Vector.</param>
        public void ObfuscateFile(string filePath, byte[] key, byte[] iv, ObfuscationMetricsTracker metricsTracker)
        {
            metricsTracker.StartTracking();

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file does not exist.");
                return;
            }

            // Generate a hash for the file
            string originalFileHash = antiTamperMechanisms.GenerateFileHash(filePath);

            // Encrypt the file contents
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string fileContentsBase64 = Convert.ToBase64String(fileBytes);
            string encryptedContents = strongEncryption.Encrypt(fileContentsBase64, key, iv);

            // Write encrypted content to a new file
            string obfuscatedFilePath = filePath + ".obfuscated";
            File.WriteAllText(obfuscatedFilePath, encryptedContents);

            // Optionally, you can hash the obfuscated file and store it for later tamper checks
            string obfuscatedFileHash = antiTamperMechanisms.GenerateFileHash(obfuscatedFilePath);
            string encryptedHash = strongEncryption.Encrypt(obfuscatedFileHash, key, iv);
            File.WriteAllText(obfuscatedFilePath + ".hash", encryptedHash);

            metricsTracker.IncrementFilesEncrypted();

            metricsTracker.StopTracking();
            metricsTracker.LogMetrics();
        }

        /// <summary>
        /// Deobfuscate a file.
        /// </summary>
        /// <param name="filePath">The file to deobfuscate.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="iv">Initialization Vector.</param>
        public void DeobfuscateFile(string filePath, byte[] key, byte[] iv)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file does not exist.");
                return;
            }

            // Read the encrypted content from the file
            string encryptedContents = File.ReadAllText(filePath);
            string decryptedContentsBase64 = strongEncryption.Decrypt(encryptedContents, key, iv);
            byte[] decryptedFileBytes = Convert.FromBase64String(decryptedContentsBase64);

            // Write the decrypted content to a new file
            string deobfuscatedFilePath = filePath.Replace(".obfuscated", "");
            File.WriteAllBytes(deobfuscatedFilePath, decryptedFileBytes);
        }
    }
}