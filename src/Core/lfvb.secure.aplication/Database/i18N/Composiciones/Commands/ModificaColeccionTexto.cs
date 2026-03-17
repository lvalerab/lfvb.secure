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
    public class ModificaColeccionTexto: IModificaColeccionTexto
    {
        private readonly IDataBaseService _db;
        public ModificaColeccionTexto(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<ColeccionTextoModel> execute(ColeccionTextoModel model)
        {
            if (model == null || model.Id == null || model.Id == Guid.Empty || model.Nombre==null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var entity = await _db.ColeccionesTextos.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Nombre = model.Nombre;
                    entity.Descripcion = model.Detalle;
                    _db.ColeccionesTextos.Update(entity);
                    await _db.SaveAsync();
                    return model;
                }
                else
                {
                    throw new Exception("No se ha encontrado la colección de texto");
                }
            }
        }
    }
}
