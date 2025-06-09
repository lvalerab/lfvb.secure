using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Models
{
    public class PasswordCredencial
    {
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password cannot be null or empty.", nameof(value));
                }
                _password = value;
            }
        }   
    }
}
