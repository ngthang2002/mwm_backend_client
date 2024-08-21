using BaseProject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.App.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly byte[] rgbKey;
        private static readonly byte[] rgbIV;

        static EncryptionHelper()
        {
            rgbKey = Encoding.UTF8.GetBytes(Startup.StaticConfiguration.GetSection("AESSettings:Key").Get<string>());
            rgbIV = Encoding.UTF8.GetBytes(Startup.StaticConfiguration.GetSection("AESSettings:IV").Get<string>());
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string DecodeAndDecrypt(this string cipherText)
        {
            string DecodeAndDecrypt = AesDecrypt(StringToByteArray(cipherText));
            return (DecodeAndDecrypt);
        }

        public static string EncryptAndEncode(this string plaintext)
        {
            return ByteArrayToHexString(AesEncrypt(plaintext));
        }

        public static string AesDecrypt(byte[] inputBytes)
        {
            byte[] outputBytes = inputBytes;

            string plaintext = string.Empty;

            using (MemoryStream memoryStream = new(outputBytes))
            {
                using CryptoStream cryptoStream = new(memoryStream, GetCryptoAlgorithm().CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(cryptoStream);
                plaintext = srDecrypt.ReadToEnd();
            }

            return plaintext;
        }

        public static byte[] AesEncrypt(string inputText)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputText);

            byte[] result = null;
            using (MemoryStream memoryStream = new())
            {
                using CryptoStream cryptoStream = new(memoryStream, GetCryptoAlgorithm().CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cryptoStream.Write(inputBytes, 0, inputBytes.Length);
                cryptoStream.FlushFinalBlock();

                result = memoryStream.ToArray();
            }

            return result;
        }

        private static RijndaelManaged GetCryptoAlgorithm()
        {
            RijndaelManaged algorithm = new();
            //set the mode, padding and block size
            algorithm.Padding = PaddingMode.PKCS7;
            algorithm.Mode = CipherMode.CBC;
            algorithm.KeySize = 128;
            algorithm.BlockSize = 128;
            return algorithm;
        }
    }
}
