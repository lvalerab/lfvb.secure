using lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento
{
    public class PropiedadElementoModel
    {
        public Int64? Id { get; set; }  
        public Guid? IdElemento { get; set; }
        public PropiedadModel Propiedad { get; set; }
        public DateTime? FechaValor { get; set; }
        public Boolean? Activo { get; set; }  
        public List<ValorPropiedadModel> Valores { get; set; }
    }
}
