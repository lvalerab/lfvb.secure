using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.common.PASSWORD
{
    public interface ISecurePassword
    {
        bool CanDecrypt();
        string Crypt(string password);
        string Decrypt(string hash);
    }
}
