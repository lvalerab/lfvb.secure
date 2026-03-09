using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Commads
{
    public class EliminarTextoCommand: IEliminarTextoCommand
    {
        private readonly IDataBaseService _db;  

        public EliminarTextoCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<bool> execute(Guid id)
        {
            //borramos las agrupaciones
            var agrupaciones= await _db.ColumnasTextosIdiomas.Where(cti => cti.Id == id).ToListAsync();
            _db.ColumnasTextosIdiomas.RemoveRange(agrupaciones);

            //borramos los textos idiomas
            var textosIdiomas = await _db.TextosIdiomas.Where(ti => ti.Id == id).ToListAsync(); 
            _db.TextosIdiomas.RemoveRange(textosIdiomas);

            //borramos las variables    
            var variables = await _db.VariablesTextos.Where(vt => vt.Id == id).ToListAsync();
            _db.VariablesTextos.RemoveRange(variables);

            //Borramos el texto
            var texto = await _db.Textos.Where(t => t.Id == id).FirstOrDefaultAsync();  
            if (texto != null)
            {
                _db.Textos.Remove(texto);
            }

            //Borramos el elemento
            var elemento = await _db.Elementos.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (elemento != null)
            {
                _db.Elementos.Remove(elemento);
            }

            await _db.SaveAsync();
            return true;

        }
    }
}
