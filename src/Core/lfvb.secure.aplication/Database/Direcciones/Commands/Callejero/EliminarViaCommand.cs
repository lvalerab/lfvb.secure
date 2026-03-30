using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.Callejero
{
    public class EliminarViaCommand : IEliminarViaCommand
    {
        private readonly IDataBaseService _db;
        public EliminarViaCommand(IDataBaseService db)
        {
            _db = db;
        }
        public async Task<bool> execute(Guid id)
        {
            //No debe de pertene a ninguna dirección normalizada ni no normalizada, si es así, se lanzará una excepción
            bool direccion = await _db.DireccionesNormalizadas.AnyAsync(dn => dn.IdCalle == id) || await _db.DireccionesNoNormalizadas.AnyAsync(dnn => dnn.IdCalle == id);
            if (direccion)
            {
                throw new Exception("La via a eliminar pertenece a una dirección normalizada o no normalizada, no se puede eliminar");
            }
            bool calleSuperior = await _db.Callejeros.AnyAsync(c => c.IdCalleSuperior == id);
            if (calleSuperior)
            {
                throw new Exception("La via a eliminar es calle superior de otra via, no se puede eliminar");
            }
            var entity = await (from c in _db.Callejeros
                                where c.Id == id
                                select c).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new Exception("La via a eliminar no existe");
            }
            _db.Callejeros.Remove(entity);
            await _db.SaveAsync();
            return true;
        }
    }
}
