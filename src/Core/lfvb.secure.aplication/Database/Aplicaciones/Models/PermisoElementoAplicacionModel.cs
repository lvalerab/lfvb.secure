using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Models
{
    public class PermisoElementoAplicacionModel
    {
        public ElementoAplicacionModel Elemento { get; set; } = new ElementoAplicacionModel();
        public GrupoModel Grupo { get; set; } = new GrupoModel();
        public TipoPermisoElementoAplicacionModel TipoPermiso { get; set; } = new TipoPermisoElementoAplicacionModel();
    }
}
