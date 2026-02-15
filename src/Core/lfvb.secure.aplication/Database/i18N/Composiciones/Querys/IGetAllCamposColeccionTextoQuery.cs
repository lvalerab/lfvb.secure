using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Querys
{
    public interface IGetAllCamposColeccionTextoQuery
    {
        public Task<List<CampoColeccionTextoModel>> execute(Guid idColeccion);
    }
}
