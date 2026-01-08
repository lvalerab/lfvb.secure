using AutoMapper;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.TipoUnidadOrganizativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Tipos
{
    public class AltaTipoUnidadOrganizativaCommand:IAltaTipoUnidadOrganizativaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private readonly IAltaElementoCommand _altaElementoCommand;

        public AltaTipoUnidadOrganizativaCommand(IDataBaseService db,
                                                 IMapper mp,
                                                 IAltaElementoCommand altaElementoCmd)
        {
            _db = db;
            _mp = mp;
            _altaElementoCommand = altaElementoCmd;
        }

        public async Task<TipoUnidadOrganizativaModel> execute(TipoUnidadOrganizativaModel tipo)
        {
            if(tipo == null)
                throw new ArgumentNullException("El tipo de unidad organizativa no puede ser nulo.");

            if(tipo.Codigo == null || tipo.Codigo == Guid.Empty)
            {
                Guid id=await _altaElementoCommand.execute("tuno");                
                TipoUnidadOrganizativaEntity entidad = new TipoUnidadOrganizativaEntity()
                {
                    Codigo = id,
                    Nombre = tipo.Nombre!,
                    Descripcion = tipo.Descripcion!
                };
                _db.TiposUnidadesOrganizativas.Add(entidad);
                await _db.SaveAsync();
                tipo.Codigo = entidad.Codigo;
                return tipo;
            } else
            {
                throw new ArgumentException("El tipo de unidad organizativa ya existe.");
            }
                
        }
    }
}
