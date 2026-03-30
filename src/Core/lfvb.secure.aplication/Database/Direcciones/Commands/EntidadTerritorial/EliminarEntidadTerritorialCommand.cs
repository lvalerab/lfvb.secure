using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.EntidadTerritorial
{
    public class EliminarEntidadTerritorialCommand:IEliminarEntidadTerritorialCommand
    {
        private readonly IDataBaseService _db;

        public  EliminarEntidadTerritorialCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<bool> execute(Guid id)
        {
            //Comprobamos que no tenga hijos, ni calles asociadas, si es así, no se puede eliminar  
            bool hijos =_db.EntidadesTerritoriales.Any(et => et.IdPadre == id);
            bool calles = _db.Callejeros.Any(c => c.IdEntidadTerritorial == id); 
            if(hijos || calles)
            {
                throw new Exception("No se puede eliminar la entidad territorial porque tiene entidades territoriales hijas o calles asociadas");
            }
            var entidad = _db.EntidadesTerritoriales.FirstOrDefault(et => et.Id == id);
            if(entidad == null)
            {
                throw new Exception("La entidad territorial no existe");
            }
            _db.EntidadesTerritoriales.Remove(entidad);
            await _db.SaveAsync();
            return true;
        }
    }
}
