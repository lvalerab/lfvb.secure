using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Tipos
{
    public interface IAltaTipoUnidadOrganizativaCommand
    {
        Task<TipoUnidadOrganizativaModel> execute(TipoUnidadOrganizativaModel tipo);
    }
}
