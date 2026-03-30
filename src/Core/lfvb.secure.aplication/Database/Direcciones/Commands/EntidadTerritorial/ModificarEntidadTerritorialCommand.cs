using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Direcciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.EntidadTerritorial
{
    public class ModificarEntidadTerritorialCommand: IModificarEntidadTerritorialCommand
    {
        private readonly IDataBaseService _db;

        public ModificarEntidadTerritorialCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<EntidadTerritorialModel> execute(EntidadTerritorialModel model, bool transacion = true)
        {
            if (model.Id == null)
            {
                throw new Exception("El id de la entidad territorial es requerido para modificarla");
            }
            if (model.Tipo == null || model.Tipo.Id == null)
            {
                throw new Exception("El tipo de la entidad territorial es requerido para modificarla");
            }
            if (string.IsNullOrEmpty(model.Nombre))
            {
                throw new Exception("El nombre de la entidad territorial es requerido para modificarla");
            }

            EntitdadTerritorialEntity? entidad = await (from et in _db.EntidadesTerritoriales
                                                        where et.Id == model.Id
                                                        select et).FirstOrDefaultAsync();
            if (entidad == null)
            {
                throw new Exception("La entidad territorial no existe");
            }
            else
            {
                entidad.Nombre = model.Nombre;
                entidad.CodigoTipoEntidad = model.Tipo.Codigo;
                if (model.Padre != null && model.Padre.Id != null)
                {
                    entidad.IdPadre = model.Padre.Id;
                }
                else
                {
                    entidad.IdPadre = null;
                }
                _db.EntidadesTerritoriales.Update(entidad);
                if (transacion)
                {
                    await _db.SaveAsync();
                }
                return model;
            }
        }
    }
}
