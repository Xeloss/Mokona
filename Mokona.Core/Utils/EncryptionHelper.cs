namespace Mokona.Core.Utils
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptionHelper
    {
        private const string DEFAULT_ENCRIPTION_KEY = "MOKV2SXZNI99212";
        private static readonly byte[] SALT = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        public static string Encrypt(string clearText)
        {
            return Encrypt(clearText, DEFAULT_ENCRIPTION_KEY);
        }
        public static string Encrypt(string clearText, string encriptionKey)
        {
            string result;
            var clearBytes = Encoding.Unicode.GetBytes(clearText);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encriptionKey + DEFAULT_ENCRIPTION_KEY, SALT);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    result = Convert.ToBase64String(ms.ToArray());
                }
            }

            return result;
        }

        public static string Decrypt(string cipherText)
        {
            return Decrypt(cipherText, DEFAULT_ENCRIPTION_KEY);
        }
        public static string Decrypt(string cipherText, string encryptionKey)
        {
            string result;
            var cipherBytes = Convert.FromBase64String(cipherText);

            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(encryptionKey + DEFAULT_ENCRIPTION_KEY, SALT);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    result = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return result;
        }
    }
}
