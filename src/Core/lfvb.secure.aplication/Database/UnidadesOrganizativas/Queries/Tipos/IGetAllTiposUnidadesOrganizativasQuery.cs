using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Queries.Tipos
{
    public interface IGetAllTiposUnidadesOrganizativasQuery
    {
         Task<List<TipoUnidadOrganizativaModel>> execute();
    }
}
