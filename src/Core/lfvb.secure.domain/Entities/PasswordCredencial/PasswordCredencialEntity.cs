using lfvb.secure.domain.Entities.Credencial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.PasswordCredencial
{
    public class PasswordCredencialEntity
    {
        public Int64 Id { get; set; }

        public string Password { get; set; }

        public CredencialEntity Credencial { get; set; }

    }
}
