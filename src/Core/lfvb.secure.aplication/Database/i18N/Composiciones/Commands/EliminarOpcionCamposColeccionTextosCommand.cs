using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Composiciones.Commands
{
    public class EliminarOpcionCamposColeccionTextosCommand : IEliminarOpcionCamposColeccionTextosCommand
    {
        private readonly IDataBaseService _db;
        public EliminarOpcionCamposColeccionTextosCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<bool> execute(Guid id, bool EliminarTextos, bool transacion = true)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else
            {
                var entity = await _db.OpcionesTextos.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    if (EliminarTextos)
                    {
                        var textos = await _db.Textos.Where(x => x.Id == entity.IdTexto).ToListAsync();
                        _db.Textos.RemoveRange(textos);
                    }
                    _db.OpcionesTextos.Remove(entity);
                    
                    if(transacion)
                        await _db.SaveAsync();

                    //Eliminamos el elemento
                    var elemento = await _db.Elementos.Where(e => e.Id == id).FirstOrDefaultAsync();
                    if (elemento != null)
                    {
                        _db.Elementos.Remove(elemento);
                    }

                    if (transacion)
                        await _db.SaveAsync();
                    return true;
                }
                else
                {
                    throw new Exception("No se ha encontrado la opción del campo de la colección de texto");
                }

            }
        }
    }
}
