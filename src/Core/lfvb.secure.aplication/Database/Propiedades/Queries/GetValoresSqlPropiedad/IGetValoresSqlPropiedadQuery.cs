using lfvb.secure.aplication.Database.Propiedades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetValoresSqlPropiedad
{
    public interface IGetValoresSqlPropiedadQuery
    {
        Task<List<GrupoValorEtiquetaModel>> execute(string codigoPropiedad, string idElemento); 
    }
}
