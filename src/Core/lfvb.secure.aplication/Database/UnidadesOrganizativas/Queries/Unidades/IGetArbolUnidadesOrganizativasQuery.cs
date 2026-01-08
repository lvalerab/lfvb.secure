using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Queries.Unidades
{
    public interface IGetArbolUnidadesOrganizativasQuery
    {
        Task<List<UnidadOrganizativaModel>> execute(Guid? codPadre = null, Guid? Tipo = null, int nivelMax = 999, int nivelActual = 1);
    }
}
