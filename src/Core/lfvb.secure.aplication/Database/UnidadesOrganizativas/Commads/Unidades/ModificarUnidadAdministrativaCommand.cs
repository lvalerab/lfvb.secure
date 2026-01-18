using AutoMapper;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.UnidadOrganizativa;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Unidades
{
    public class ModificarUnidadAdministrativaCommand: IModificarUnidadAdministrativaCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public ModificarUnidadAdministrativaCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<UnidadOrganizativaModel> execute(UnidadOrganizativaModel unidad)
        {
            if(unidad == null)
            {
                throw new ArgumentNullException("La unidad organizativa no puede ser nula");
            }

            if(unidad.Codigo == null)
            {
                throw new ArgumentNullException("El codigo de la unidad organizativa no puede ser nulo");
            }   

            if(unidad.Nombre == null)
            {
                throw new ArgumentNullException("El nombre de la unidad organizativa no puede ser nulo");
            }   

            if(unidad.TipoUnidadOrganizativa == null || unidad.TipoUnidadOrganizativa.Codigo == null)
            {
                throw new ArgumentNullException("El tipo de la unidad organizativa no puede ser nulo");
            }

            UnidadOrganizativaEntity elemento=(from uu in _db.UnidadesOrganizativas
                                               where uu.Codigo==unidad.Codigo
                                               select uu).FirstOrDefault();

            if(elemento==null)
            {
                 throw new ArgumentException("La unidad organizativa no existe");
            } else
            {
                elemento.Nombre=unidad.Nombre;
                if(unidad.Padre==null || unidad.Padre.Codigo==null)
                {
                    //Si no tiene padre, el tipo de unidad organizativa es el que se le pasa
                    elemento.CodUnorPadre=null;
                    elemento.CodTuno = unidad.TipoUnidadOrganizativa.Codigo ?? Guid.Empty;
                } else
                {
                    //Si tiene padre, el tipo de unidad organizativa debe ser el del padre
                    elemento.CodUnorPadre = unidad.Padre?.Codigo;
                    elemento.CodTuno = unidad.Padre?.TipoUnidadOrganizativa?.Codigo ?? Guid.Empty;
                }
                _db.UnidadesOrganizativas.Update(elemento);
                await _db.SaveAsync();

                return unidad;
            }
        }
    }
}
