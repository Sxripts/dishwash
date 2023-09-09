using System;
using System.Diagnostics;
using System.Security.Cryptography;
using JookoObfuscate.Encryption;

namespace JookoObfuscate.AntiTamper
{
    /// <summary>
    /// Implements safeguards to detect and react to tampering of external executable files.
    /// </summary>
    public class AntiTamperMechanisms
    {
        private readonly StrongEncryption strongEncryption;

        public AntiTamperMechanisms()
        {
            strongEncryption = new StrongEncryption();
        }

        /// <summary>
        /// Generates a hash of the provided file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <returns>Hash as a string.</returns>
        public string GenerateFileHash(string filePath)
        {
            using SHA256 sha256 = SHA256.Create();
            using FileStream fileStream = File.OpenRead(filePath);
            byte[] hash = sha256.ComputeHash(fileStream);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifies if the file has been tampered with by comparing its hash with an expected hash.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="expectedEncryptedHash">Encrypted expected hash.</param>
        /// <param name="key">Encryption key.</param>
        /// <param name="iv">Initialization Vector.</param>
        /// <returns>True if tampering is detected, false otherwise.</returns>
        public bool IsFileTampered(string filePath, string expectedEncryptedHash, byte[] key, byte[] iv)
        {
            string decryptedHash = strongEncryption.Decrypt(expectedEncryptedHash, key, iv);
            string currentHash = GenerateFileHash(filePath);

            return !decryptedHash.Equals(currentHash);
        }

        /// <summary>
        /// Reacts to a tampering attempt.
        /// </summary>
        public void ReactToTampering()
        {
            // Implement your desired behavior here.
            // For instance, you could silently report this event to a monitoring service.
        }
    }
}