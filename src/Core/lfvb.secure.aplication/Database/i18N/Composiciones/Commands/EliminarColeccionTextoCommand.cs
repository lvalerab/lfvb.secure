using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Commands
{
    public class EliminarColeccionTextoCommand : IEliminarColeccionTextoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IEliminarCamposColeccionTextoCommand _eliminarCampos;

        public EliminarColeccionTextoCommand(IDataBaseService db, IEliminarCamposColeccionTextoCommand eliminarCampos)
        {
            _db = db;
            _eliminarCampos = eliminarCampos;
        }

        public async Task<bool> execute(Guid id, bool EliminarTextos, bool transacion)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else
            {
                var entity = await _db.ColeccionesTextos.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    var campos = await _db.CamposTextos.Where(x => x.IdColeccion == id).ToListAsync();
                    foreach (var campo in campos)
                    {
                        await _eliminarCampos.execute(campo.Id, EliminarTextos, false);
                    }
                    _db.ColeccionesTextos.Remove(entity);
                    if (transacion)
                        await _db.SaveAsync();
                    return true;
                }
                else
                {
                    throw new Exception("No se ha encontrado la colección de texto");
                }
            }
        }
    }
}
