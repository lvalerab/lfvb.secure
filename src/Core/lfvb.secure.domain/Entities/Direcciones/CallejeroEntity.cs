using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class CallejeroEntity
    {
        public Guid Id { get; set; }
        public Guid IdEntidadTerritorial { get; set; }
        public Guid? IdCalleSuperior { get; set; }   
        public string CodigoTipoVia { get; set; }   
        public string Nombre { get; set; }

        public EntitdadTerritorialEntity EntidadTerritorial { get; set; }
        public CallejeroEntity? CalleSuperior { get; set; }   
        public ICollection<CallejeroEntity> CallesInferiores { get; set; } = new List<CallejeroEntity>();
        public TipoViaEntity TipoVia { get; set; }  

        public ICollection<DireccionNormalizadaEntity> DireccionNormalizadas { get; set; } = new List<DireccionNormalizadaEntity>();    
        public ICollection<DireccionNoNormalizadaEntity> DireccionNoNormalizadas { get; set; } = new List<DireccionNoNormalizadaEntity>();
    }
}
