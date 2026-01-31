using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.domain.Entities.TipoElemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Models
{
    public class CircuitoModel
    {
        public Guid? Id { get; set; }=Guid.Empty;
        public TramiteModel? Tramite { get; set; }
        public string Nombre { get; set; } = string.Empty;  
        public string Descripcion { get; set; } = string.Empty; 
        public string Normativa { get; set; } = string.Empty;   
        public bool Activo { get; set; } = true;
        public DateTime? FechaAlta { get; set; } = null;
        public DateTime? FechaModificacion { get; set; } = null;
        public DateTime? FechaBaja { get; set; } = null;
        public List<TipoElementoEntity> Tipos { get; set; } = new List<TipoElementoEntity>();   
        public List<GrupoModel> Grupos { get; set; } = new List<GrupoModel>();  
    }
}
