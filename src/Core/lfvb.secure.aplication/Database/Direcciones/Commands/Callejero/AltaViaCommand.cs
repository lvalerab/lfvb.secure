using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.Callejero
{
    public class AltaViaCommand : IAltaViaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _cmdAltaElemento;

        public AltaViaCommand(IDataBaseService db, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _cmdAltaElemento = cmdAltaElemento;
        }

        public async Task<CallejeroModel> execute(CallejeroModel model, bool transacion = true)
        {
            if(model.Id!=null)
            {
                throw new Exception("La via nueva no tiene que tener identificador");  
            }
            if(model.EntidadTerritorial==null || model.EntidadTerritorial.Id==null)
            {
                throw new Exception("La via debe tener una entidad territorial asignada");
            }
            if(model.TipoVia==null || model.TipoVia.Codigo==null)
            {
                throw new Exception("La via debe tener un tipo de via asignado");
            }

            //Obtenemos el id de la via
            model.Id=await _cmdAltaElemento.execute("calle", false);

            //Insertamos la via
            CallejeroEntity entity = new CallejeroEntity
            {
                Id=model.Id.Value,
                Nombre=model.Nombre,
                CodigoTipoVia=model.TipoVia.Codigo,
                IdEntidadTerritorial=model.EntidadTerritorial.Id??Guid.Empty,
                IdCalleSuperior=model.CalleSuperior?.Id
            };  
            await _db.Callejeros.AddAsync(entity);  
            if(transacion)
            {
                await _db.SaveAsync();
            }
            return model;
        }
    }
}
