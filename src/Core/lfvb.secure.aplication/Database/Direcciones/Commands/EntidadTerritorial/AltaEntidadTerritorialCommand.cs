using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.EntidadTerritorial
{
    public class AltaEntidadTerritorialCommand: IAltaEntidadTerritorialCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _cmdAltaElemento;
        
        public AltaEntidadTerritorialCommand(IDataBaseService db,
                                             IAltaElementoCommand cmdAltaElemento
            )
        {
            _db = db;
            _cmdAltaElemento = cmdAltaElemento; 
        }

        public async Task<EntidadTerritorialModel> execute(EntidadTerritorialModel model, bool transacion=true)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if(model.Id != null)
            {
                throw new ArgumentException("El Id debe ser null para una nueva entidad territorial");
            }
            if(string.IsNullOrWhiteSpace(model.Nombre))
            {
                throw new ArgumentException("El nombre de la entidad territorial no puede ser vacío");
            }

            //Obtenemos el id del nuevo elemento
            model.Id = await _cmdAltaElemento.execute("ente", false);

            //Damos de alta la entidad territorial
            EntitdadTerritorialEntity entity = new EntitdadTerritorialEntity
            {
                Id = model.Id.Value,
                Nombre = model.Nombre,
                CodigoTipoEntidad = model.Tipo?.Codigo ?? throw new ArgumentException("El tipo de entidad territorial es obligatorio"),
                IdPadre = model.Padre?.Id
            };

            await _db.EntidadesTerritoriales.AddAsync(entity);
            if(transacion)
            {
                await _db.SaveAsync();
            }
            return model;
        }
    }
}
