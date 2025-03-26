using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.common.PASSWORD
{
    public class SecurePassword : ISecurePassword
    {
        public bool CanDecrypt()
        {
            return false;
        }

        public string Crypt(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] calculate=md5.ComputeHash(Encoding.UTF8.GetBytes(password)); 
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < calculate.Length; i++)
            {
                stringBuilder.Append(calculate[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public string Decrypt(string hash)
        {
            if (!this.CanDecrypt())
            {
                return null;    
            }
            {
                //Implementar el codigo en caso de contraseñas encriptadas con algoritmos reversibles
                return null;
            }
        }
    }
}
