using System;
using System.IO;
using System.Security.Cryptography;

namespace JookoObfuscate.Encryption
{
    public class StrongEncryption
    {
        public byte[] GenerateRandomKey()
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.GenerateKey();
            return aesAlg.Key;
        }

        public byte[] GenerateRandomIV()
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.GenerateIV();
            return aesAlg.IV;
        }

        public void EncryptFile(string filePath, byte[] key, byte[] iv)
        {
            // Load the file into a byte array
            byte[] fileBytes = File.ReadAllBytes(filePath);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
            {
                csEncrypt.Write(fileBytes, 0, fileBytes.Length);
            }

            byte[] encryptedFile = msEncrypt.ToArray();

            // Save the encrypted byte array back as a file
            File.WriteAllBytes(filePath + ".encrypted", encryptedFile);
        }

        public void DecryptFile(string encryptedFilePath, string decryptedFilePath, byte[] key, byte[] iv)
        {
            byte[] encryptedFileBytes = File.ReadAllBytes(encryptedFilePath);

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(encryptedFileBytes);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            {
                byte[] fromEncrypt = new byte[encryptedFileBytes.Length];
                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);

                File.WriteAllBytes(decryptedFilePath, fromEncrypt);
            }
        }
    }
}