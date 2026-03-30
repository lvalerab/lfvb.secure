using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Direcciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.TipoEntidadTerritorial
{
    public class AltaTipoEntidadTerritorialCommand : IAltaTipoEntidadTerritorialCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _cmdAltaElemento;

        public AltaTipoEntidadTerritorialCommand(IDataBaseService db, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _cmdAltaElemento = cmdAltaElemento;
        }

        public async Task<TipoEntidadTerritorialModel> execute(TipoEntidadTerritorialModel model)
        {
            TipoEntidadTerritorialModel? result = await (from tet in _db.TiposEntidadesTerritoriales
                                                         where tet.Codigo == model.Codigo
                                                         select new TipoEntidadTerritorialModel
                                                         {
                                                             Codigo = tet.Codigo,
                                                             Nombre = tet.Nombre
                                                         }).FirstOrDefaultAsync();
            if (result == null)
            {
                //Generamos el guid
                Guid? id=await _cmdAltaElemento.execute("tnte",false);
                _db.TiposEntidadesTerritoriales.Add(new TipoEntidadTerritorialEntity
                {
                    Codigo = model.Codigo,
                    Nombre = model.Nombre,
                    Id = id
                });
                result = model;
            }
            else
            {
                //La actualizamos
                result.Nombre = model.Nombre;
                _db.TiposEntidadesTerritoriales.Update(new TipoEntidadTerritorialEntity
                {
                    Codigo = result.Codigo,
                    Nombre = result.Nombre
                });
            }
            await _db.SaveAsync();
            return result;
        }
    }
}
