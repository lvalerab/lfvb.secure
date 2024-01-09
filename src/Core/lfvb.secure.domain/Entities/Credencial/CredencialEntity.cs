using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.TipoCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Credencial
{
    public class CredencialEntity
    {
        public Int64 Id { get; set; }

        public required Guid IdUsuario { get; set;}

        public required String CodigoTipoCredencial { get; set;}

        public DateTime? VigenteDesde { get; set; }

        public DateTime? VigenteHasta { get; set; }

        public UsuarioEntity Usuario { get; set; }

        public TipoCredencialEntity TipoCredencial { get; set; }

        public PasswordCredencialEntity? Password { get; set; }

        public TokenCredencialEntity? Token { get; set; }
    }
}
