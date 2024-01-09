using lfvb.secure.domain.Entities.Credencial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.TokenCredencial
{
    public class TokenCredencialEntity
    {
        public Int64 Id { get; set; }

        public string Token { get; set; }

        public CredencialEntity Credencial { get; set; }
    }
}
