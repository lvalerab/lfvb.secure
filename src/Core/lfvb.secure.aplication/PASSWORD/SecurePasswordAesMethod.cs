using lfvb.secure.common.PASSWORD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.PASSWORD
{
    public class SecurePasswordAesMethod : ISecurePassword
    {

        private string DefaultKey;
        private string DefaultIV;

        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(data, 0, data.Length);
                            cs.Close();
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream(cipherText))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (var sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }


        public SecurePasswordAesMethod(string key, string iv)
        {
            // Constructor logic if needed
            this.DefaultKey = key;
            this.DefaultIV = iv;
        }

        public SecurePasswordAesMethod()
        {
            // Default constructor
            this.DefaultKey = "CG3G8MwwEk+Tth7KM47JsQ==";
            this.DefaultIV = "0123456789ABCDF=";
        }

        public bool CanDecrypt()
        {
            return true;
        }

        public string Crypt(string password)
        {
            return Encoding.UTF8.GetString(Encrypt(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(DefaultKey), Encoding.UTF8.GetBytes(DefaultIV)));
        }

        public string Decrypt(string hash)
        {
            return Decrypt(Encoding.UTF8.GetBytes(hash), Encoding.UTF8.GetBytes(DefaultKey), Encoding.UTF8.GetBytes(DefaultIV));
        }
    }
}
