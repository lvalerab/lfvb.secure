using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Commands
{
    public class ModificarCampoColeccionTextoCommand: IModificarCampoColeccionTextoCommand
    {
        private readonly IDataBaseService _db;

        public ModificarCampoColeccionTextoCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<CampoColeccionTextoModel> execute(CampoColeccionTextoModel model)
        {
            if (model == null || model.Id == null || model.Id == Guid.Empty || model.Nombre == null || model.Coleccion == null || model.Coleccion.Id == null || model.Coleccion.Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var entity = await _db.CamposTextos.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Nombre = model.Nombre;
                    _db.CamposTextos.Update(entity);
                    await _db.SaveAsync();
                    return model;
                }
                else
                {
                    throw new Exception("No se ha encontrado el campo de la colección de texto");
                }
            }
        }
    }
}
