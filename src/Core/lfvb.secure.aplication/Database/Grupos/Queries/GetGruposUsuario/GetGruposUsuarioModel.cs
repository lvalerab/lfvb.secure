using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario
{
    public class GetGruposUsuarioModel
    {
        public Guid? Id { get; set; }
        public Guid? UsuarioId { get; set; }
        public string Nombre { get; set; }
    }
}
