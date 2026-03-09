using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.domain.Entities.Direcciones
{
    public class EntitdadTerritorialEntity
    {
        public Guid Id { get; set; }
        public Guid? IdPadre { get; set; }
        public string CodigoTipoEntidad { get; set; }   
        public string Nombre { get; set; }

        public EntitdadTerritorialEntity? Padre { get; set; }
        public ICollection<EntitdadTerritorialEntity> Hijos { get; set; }
        public ICollection<CallejeroEntity> Calles { get; set; }
        public TipoEntidadTerritorialEntity TipoEntidad { get; set; }
        public ICollection<DireccionNoNormalizadaEntity> DireccionesNoNormalizadas { get; set; }    

    }
}
