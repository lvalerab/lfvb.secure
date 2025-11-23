using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.PasoSiguiente;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public class EliminarPasoCircuitoCommand : IEliminarPasoCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;


        public EliminarPasoCircuitoCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<bool> execute(Guid idPaso, bool interconectar=false)
        {
            //Comprobamos que no exista ningun elemento en el paso
            bool borrar = true;

            if(await (_db.EstadosElementos.Where(el=>el.IdPaso == idPaso).AnyAsync()))
            {
                borrar = false;
            };

            List<Guid> pasosSiguientes= await (from ps in _db.PasosSiguientes
                                                where ps.IdPaso == idPaso
                                                select ps.IdPasoSiguiente).ToListAsync();

           
            List<Guid> pasosAnteriores= await (from ps in _db.PasosSiguientes
                                                where ps.IdPasoSiguiente == idPaso
                                                select ps.IdPaso).ToListAsync();
            if (borrar)
            {

                if (interconectar)
                {
                                   //Interconectamos los pasos anteriores con los siguientes
                    foreach(var pasoAnterior in pasosAnteriores)
                    {
                        foreach(var pasoSiguiente in pasosSiguientes)
                        {
                            PasoSiguienteEntity nuevoPasoSiguiente = new PasoSiguienteEntity
                            {
                                IdPaso = pasoAnterior,
                                IdPasoSiguiente = pasoSiguiente
                            };
                            await _db.PasosSiguientes.AddAsync(nuevoPasoSiguiente);

                        }
                        //Borramos las relaciones con los pasos anteriores
                        await _db.PasosSiguientes.Where(ps => ps.IdPasoSiguiente == idPaso && ps.IdPaso == pasoAnterior)
                            .ExecuteDeleteAsync();
                    }
               
                }

                //Borramos las relaciones con los pasos anteriores
                foreach (var pasoAnterior in pasosAnteriores)
                {   
                    await _db.PasosSiguientes.Where(ps => ps.IdPasoSiguiente == idPaso && ps.IdPaso == pasoAnterior)
                        .ExecuteDeleteAsync();
                }

                //Borramos las relaciones con los pasos siguientes
                foreach (var pasoSiguiente in pasosSiguientes)
                {
                    await _db.PasosSiguientes.Where(ps => ps.IdPaso == idPaso && ps.IdPasoSiguiente == pasoSiguiente)
                        .ExecuteDeleteAsync();
                }

                //Borramos los grupos que puedes gestionar el paso
                await _db.PermisosPasosGrupos.Where(pg => pg.IdPaso == idPaso)
                    .ExecuteDeleteAsync();

                //Borramos los usuarios que pueden gestionar el paso
                await _db.PermisosPasosUsuarios.Where(pu => pu.IdPaso == idPaso)
                    .ExecuteDeleteAsync();

                //Borramos las acciones configuradas en el paso
                await _db.PasosAcciones.Where(ap => ap.IdPaso == idPaso).ExecuteDeleteAsync();

                //Borramos el paso
                await _db.Pasos.Where(p => p.Id == idPaso).ExecuteDeleteAsync();
            }

            return borrar;
        }
    }
}
