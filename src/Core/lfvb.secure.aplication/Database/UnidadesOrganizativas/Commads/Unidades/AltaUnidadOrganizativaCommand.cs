using AutoMapper;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.UnidadOrganizativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Unidades
{
    public class AltaUnidadOrganizativaCommand: IAltaUnidadOrganizativaCommand
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private readonly IAltaElementoCommand _cmdAltaElemento;

        public AltaUnidadOrganizativaCommand(IDataBaseService db, IMapper mp, IAltaElementoCommand cmdAltaElemento)
        {
            _db = db;
            _mp = mp;
            _cmdAltaElemento = cmdAltaElemento;
        }

        public async Task<UnidadOrganizativaModel> execute(UnidadOrganizativaModel unidad)
        {
            if(unidad == null)
            {
                throw new ArgumentNullException("La unidad organizativa no puede ser nula");
            }

            if(!(unidad.Codigo==null || unidad.Codigo == Guid.Empty))
            {
                throw new ArgumentException("La unidad organizativa no puede tener un codigo asignado");
            }

            if(unidad.Nombre == null || unidad.Nombre.Trim() == "")
            {
                throw new ArgumentException("La unidad organizativa debe tener un nombre");
            }

            if(unidad.TipoUnidadOrganizativa == null || unidad.TipoUnidadOrganizativa.Codigo == null || unidad.TipoUnidadOrganizativa.Codigo == Guid.Empty)
            {
                throw new ArgumentException("La unidad organizativa debe tener un tipo de unidad organizativa valido");
            }

            Guid codigo=await _cmdAltaElemento.execute("unor");

            UnidadOrganizativaEntity unor=new UnidadOrganizativaEntity             {
                Codigo=codigo,
                Nombre=unidad.Nombre.Trim(),
                CodTuno=unidad.TipoUnidadOrganizativa.Codigo.Value,
                CodUnorPadre=unidad.Padre!=null?unidad.Padre.Codigo: null
            };

            await _db.UnidadesOrganizativas.AddAsync(unor);
            await _db.SaveAsync();

            unidad.Codigo=unor.Codigo;

            return unidad;
        }
    }
}
