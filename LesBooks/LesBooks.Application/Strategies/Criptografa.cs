using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Strategies
{
    public class Criptografa
    {
        private static readonly byte[] salt = new byte[] { 0x26, 0x19, 0x45, 0x3B, 0x5F, 0x5A, 0x6E, 0x21 };
        private const int Iterations = 1000;
        private const int KeySize = 256;
        private string passPhrase = "chavedecriptografiautilizada";
        public string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (var aes = new AesManaged())
            {
                var key = new Rfc2898DeriveBytes(passPhrase, salt, Iterations).GetBytes(KeySize / 8);
                aes.Key = key;
                aes.GenerateIV();
                using (var ms = new MemoryStream())
                {
                    ms.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                        cryptoStream.Close();
                    }
                    byte[] encryptedBytes = ms.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }
        public string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (var aes = new AesManaged())
            {
                var key = new Rfc2898DeriveBytes(passPhrase, salt, Iterations).GetBytes(KeySize / 8);
                aes.Key = key;
                using (var ms = new MemoryStream(cipherBytes))
                {
                    byte[] ivBytes = new byte[sizeof(int)];
                    ms.Read(ivBytes, 0, ivBytes.Length);
                    aes.IV = new byte[BitConverter.ToInt32(ivBytes, 0)];
                    ms.Read(aes.IV, 0, aes.IV.Length);
                    using (var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cryptoStream))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
