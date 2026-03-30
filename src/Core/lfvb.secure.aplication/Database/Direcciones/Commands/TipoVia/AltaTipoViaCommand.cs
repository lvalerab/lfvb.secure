using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using lfvb.secure.domain.Entities.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Direcciones.Commands.TipoVia
{
    public class AltaTipoViaCommand: IAltaTipoViaCommand
    {
        private readonly IDataBaseService _db;

        public AltaTipoViaCommand(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<TipoViaModel> execute(TipoViaModel model)
        {
            TipoViaModel? result=await (from tv in _db.TiposVias
                                       where tv.Codigo == model.Codigo
                                       select new TipoViaModel
                                       {
                                           Codigo = tv.Codigo,
                                           Nombre = tv.Nombre
                                       }).FirstOrDefaultAsync();

            if (result == null) 
            {
                _db.TiposVias.Add(new TipoViaEntity
                {
                    Codigo = model.Codigo,
                    Nombre = model.Nombre
                });                
                result = model;
            } else
            {
                //La actualizamos
                result.Nombre = model.Nombre;
                _db.TiposVias.Update(new TipoViaEntity
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
