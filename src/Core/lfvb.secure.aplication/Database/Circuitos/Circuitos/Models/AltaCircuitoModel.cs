using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.TipoElemento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Models
{
    public class AltaCircuitoModel
    {
        public Guid? Id { get; set; } = Guid.Empty;
        public TramiteModel Tramite { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string normativa { get; set; } = "";
        public List<TipoElementoModel> Tipos { get; set; }   =new List<TipoElementoModel>();
        public List<GrupoModel> Grupos { get; set; } = new List<GrupoModel>();  
    }
}
