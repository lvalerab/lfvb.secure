using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.common.PASSWORD
{
    public class SecurePassword : ISecurePassword
    {

        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IDataProtector _protector;

        public SecurePassword(IDataProtectionProvider dataProtectionProvider) { 
            this._dataProtectionProvider = dataProtectionProvider;
            this._protector = this._dataProtectionProvider.CreateProtector("PersonalData.MainDataProtect");
        }

        public bool CanDecrypt()
        {
            return true;
        }

        public string Crypt(string password)
        {
            return this._protector.Protect(password);
        }

        public string Decrypt(string hash)
        {
            if (!this.CanDecrypt())
            {
                return null;    
            }
            {
                //Implementar el codigo en caso de contraseñas encriptadas con algoritmos reversibles
                return this._protector.Unprotect(hash);
            }
        }
    }
}
