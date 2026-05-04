using lfvb.secure.aplication.Database.Censo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas
{
    public enum TipoCrucePersonas
    {
        Exacto = 0,
        Similar = 1,
        SoloApellidosNombre = 2,
        SoloIdentificadores = 3
    }

    public interface IBuscadorPersonaQuery
    {
        public Task<List<PersonaModel>> execute(FiltroBusquedaPersonasModel filtro);
        public Task<List<PersonaModel>> execute(FiltroBusquedaPersonasModel filtro, TipoCrucePersonas cruce=TipoCrucePersonas.Exacto);
    }
}
