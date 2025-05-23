using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Models
{
    public class LoginTokenModel
    {
        public Guid? Id { get; set; }
        public string Token { get; set; }
    }
}
