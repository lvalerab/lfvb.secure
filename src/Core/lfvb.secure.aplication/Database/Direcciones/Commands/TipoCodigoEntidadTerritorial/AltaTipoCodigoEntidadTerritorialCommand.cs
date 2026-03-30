using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Direcciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.TipoCodigoEntidadTerritorial
{
    public class AltaTipoCodigoEntidadTerritorialCommand : IAltaTipoCodigoEntidadTerritorialCommand
    {
        private readonly IDataBaseService _db;
        public AltaTipoCodigoEntidadTerritorialCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<TipoCodigoEntidadTerritorialModel> execute(TipoCodigoEntidadTerritorialModel model)
        {
            TipoCodigoEntidadTerritorialModel? result = await (from tcet in _db.TiposCodigosGestionTerritorial
                                                               where tcet.Codigo == model.Codigo
                                                               select new TipoCodigoEntidadTerritorialModel
                                                               {
                                                                   Codigo = tcet.Codigo,
                                                                   Nombre = tcet.Nombre
                                                               }).FirstOrDefaultAsync();
            if (result == null)
            {
                _db.TiposCodigosGestionTerritorial.Add(new TipoCodigoGestionTerritorialEntity
                {
                    Codigo = model.Codigo,
                    Nombre = model.Nombre
                });
                result = model;
            }
            else
            {
                //La actualizamos
                result.Nombre = model.Nombre;
                _db.TiposCodigosGestionTerritorial.Update(new TipoCodigoGestionTerritorialEntity
                {
                    Codigo = result.Codigo,
                    Nombre = result.Nombre
                });
            }
            await _db.SaveAsync();
            return result;
        }
    }
}
