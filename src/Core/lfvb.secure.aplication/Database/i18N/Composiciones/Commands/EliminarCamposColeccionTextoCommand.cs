using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Commands
{
    public class EliminarCamposColeccionTextoCommand: IEliminarCamposColeccionTextoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IEliminarOpcionCamposColeccionTextosCommand _eliminarOpcion;

        public EliminarCamposColeccionTextoCommand(IDataBaseService db, IEliminarOpcionCamposColeccionTextosCommand eliminarOpcion)
        {
            _db = db;
            _eliminarOpcion = eliminarOpcion;
        }

        public async Task<bool> execute(Guid id, bool EliminarTextos, bool transacion = true)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else
            {
                var entity = await _db.CamposTextos.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    var opciones = await _db.OpcionesTextos.Where(x => x.IdCampo == id).ToListAsync();
                    foreach (var opcion in opciones)
                    {
                        await _eliminarOpcion.execute(opcion.Id, EliminarTextos, false);
                    }
                    _db.CamposTextos.Remove(entity);

                    
                    if (transacion)
                        await _db.SaveAsync();

                    //Eliminamos el elemento
                    var elemento = await _db.Elementos.Where(e => e.Id == id).FirstOrDefaultAsync();
                    if (elemento != null)
                    {
                        _db.Elementos.Remove(elemento);
                    }

                    if(transacion)
                        await _db.SaveAsync();
                    return true;
                }
                else
                {
                    throw new Exception("No se ha encontrado el campo de la colección de texto");
                }
            }
        }
    }
}
