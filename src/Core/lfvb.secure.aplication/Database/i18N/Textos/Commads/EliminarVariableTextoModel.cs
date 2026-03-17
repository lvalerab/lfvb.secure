using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Textos.Commads
{
    public class EliminarVariableTextoModel:IEliminarVariableTextoModel
    {
        private readonly IDataBaseService _db;

        public EliminarVariableTextoModel(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<bool> execute(Guid id)
        {
            var variable = await _db.VariablesTextos.Where(vt => vt.Id == id).FirstOrDefaultAsync();
            if (variable != null)
            {
                _db.VariablesTextos.Remove(variable);
                await _db.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
