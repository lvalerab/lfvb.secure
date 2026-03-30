using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.Callejero
{
    public class ModificarViaCommand : IModificarViaCommand
    {
        private readonly IDataBaseService _db;

        public ModificarViaCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<CallejeroModel> execute(CallejeroModel model, bool transacion = true)
        {
            if (model.Id == null)
            {
                throw new Exception("La via a modificar debe tener identificador");
            }
            var entity = await (from c in _db.Callejeros
                                where c.Id == model.Id
                                select c).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new Exception("La via a modificar no existe");
            }
            if (model.EntidadTerritorial == null || model.EntidadTerritorial.Id == null)
            {
                throw new Exception("La via debe tener una entidad territorial asignada");
            }
            if (model.TipoVia == null || model.TipoVia.Codigo == null)
            {
                throw new Exception("La via debe tener un tipo de via asignado");
            }
            entity.Nombre = model.Nombre;
            entity.CodigoTipoVia = model.TipoVia.Codigo;
            entity.IdEntidadTerritorial = model.EntidadTerritorial.Id ?? Guid.Empty;
            entity.IdCalleSuperior = model.CalleSuperior?.Id ?? Guid.Empty;
            _db.Callejeros.Update(entity);
            if (transacion)
            {
                await _db.SaveAsync();
            }
            return model;
        }
    }
}
