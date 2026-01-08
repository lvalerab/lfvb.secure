using AutoMapper;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Tipos
{
    public class ModificarTipoUnidadOrganizativaCommand:IModificarTipoUnidadOrganizativaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public ModificarTipoUnidadOrganizativaCommand(IDataBaseService db,
                                                      IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<TipoUnidadOrganizativaModel> execute(TipoUnidadOrganizativaModel tipo)
        {
            if(tipo == null)
                throw new ArgumentNullException("El tipo de unidad organizativa no puede ser nulo.");

            if(tipo.Codigo == null || tipo.Codigo == Guid.Empty)
            {
                throw new ArgumentException("El tipo de unidad organizativa no existe.");
            } 
            else
            {
                var entidad = await _db.TiposUnidadesOrganizativas.FirstOrDefaultAsync(t => t.Codigo == tipo.Codigo);
                if(entidad == null)
                    throw new ArgumentException("El tipo de unidad organizativa no existe en la base de datos.");
                entidad.Nombre = tipo.Nombre!;
                entidad.Descripcion = tipo.Descripcion!;
                _db.TiposUnidadesOrganizativas.Update(entidad);
                await _db.SaveAsync();
                return tipo;
            }
        }
    }
}
