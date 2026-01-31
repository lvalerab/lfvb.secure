using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Models
{
    public class TokenCredencial
    {
        private string _token;
        public string Token
        {
            get => _token;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Token cannot be null or empty.", nameof(value));
                }
                _token = value;
            }
        }
        public DateTime? Expiration { get; set; } // Optional expiration date for the token
    }
}
