using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.Circuitos.PermisoPasoUsuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public class ModificarPasoCircuitoCommand : IModificarPasoCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public ModificarPasoCircuitoCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<PasoModel> execute(PasoModel paso)
        {

            if(paso.Id == null)
                throw new ArgumentNullException("El Id del paso no puede ser nulo");    

            PasoEntity? pasoEntity = await (from p in _db.Pasos
                                          where p.Id == paso.Id
                                          select p).FirstOrDefaultAsync();

            if (pasoEntity == null)
            {
                return null;
            } else
            {
                pasoEntity.CodEstado = paso.Estado != null ? paso.Estado.Codigo : pasoEntity.CodEstado;
                pasoEntity.CodEstadoSiguiente = paso.EstadoNuevo != null ? paso.EstadoNuevo.Codigo : pasoEntity.CodEstadoSiguiente;
                pasoEntity.IdCircuitoSiguiente = paso.CircuitoSiguiente != null ? paso.CircuitoSiguiente.Id : pasoEntity.IdCircuitoSiguiente;
                pasoEntity.Nombre = paso.Nombre;

                pasoEntity.IdBandeja= paso.Bandeja != null ? paso.Bandeja.Id : null;    

                //Borramos los usuarios tramitadores actuales
                _db.PermisosPasosUsuarios.RemoveRange(_db.PermisosPasosUsuarios.Where(ppu => ppu.IdPaso == pasoEntity.Id));
                foreach (var usuario in paso.UsuariosTramitadores)
                {
                    var permisoUsuario = new PermisoPasoUsuarioEntity
                    {
                        IdPaso = pasoEntity.Id,
                        IdUsuario = usuario.Id
                    };
                    await _db.PermisosPasosUsuarios.AddAsync(permisoUsuario);
                }

                //Borramos los grupos tramitadores actuales
                _db.PermisosPasosGrupos.RemoveRange(_db.PermisosPasosGrupos.Where(ppg => ppg.IdPaso == pasoEntity.Id));

                foreach (var grupo in paso.GruposTramitadores)
                {
                    var permisoGrupo = new domain.Entities.Circuitos.PermisoPasoGrupo.PermisoPasoGrupoEntity
                    {
                        IdPaso = pasoEntity.Id,
                        IdGrupoUsuario = grupo.Id??Guid.Empty
                    };
                    await _db.PermisosPasosGrupos.AddAsync(permisoGrupo);
                }

                await _db.SaveAsync();
            }
            return paso;
        }
    }
}
