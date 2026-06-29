using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Database.Calendario.Queries.GetCalendariosUsuario;
using lfvb.secure.aplication.Database.Elementos.Commands;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Calendario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Commands.AltaEntradaCalendario
{
    public class AltaEntradaCalendarioUsuarioCommand : IAltaEntradaCalendarioUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IAltaElementoCommand _altaElementoCommand;
        private readonly IGetCalendariosUsuarioQuery _getCalendariosUsuarioQuery;   
        private bool commit = true;    

        public AltaEntradaCalendarioUsuarioCommand(IDataBaseService db, IAltaElementoCommand altaElementoCommand, IGetCalendariosUsuarioQuery getCalendariosUsuarioQuery)
        {
            _db = db;
            _altaElementoCommand = altaElementoCommand; 
            _getCalendariosUsuarioQuery = getCalendariosUsuarioQuery;   
        }

        public async Task<EntradaCalendarioModel> execute(EntradaCalendarioModel entrada, Guid idCreador, List<Guid> idUsuarios, string TipoCalendario)
        {
            Guid id = await _altaElementoCommand.execute("encl", false);

            EntradaCalendarioEntity entity = new EntradaCalendarioEntity
            {
                Id = id,
                IdTipoEntradaCalendario = entrada.TipoEntrada.Id ?? Guid.Empty,
                IdUsuarioCreador = idCreador,
                Titulo = entrada.Titulo,
                Descripcion = entrada.Descripcion,
                FechaInicio = entrada.FechaInicio,
                FechaFin = entrada.FechaFin ?? DateTime.MaxValue
            };



            return entrada;
        }

        public async Task<EntradaCalendarioModel> execute(EntradaCalendarioModel entrada,Guid idCreador, Guid idUsuario, Guid idCalendario)
        {

            Guid id = await _altaElementoCommand.execute("encl", false);

            EntradaCalendarioEntity entity = new EntradaCalendarioEntity
            {
                Id = id,
                IdTipoEntradaCalendario = entrada.TipoEntrada.Id??Guid.Empty,
                IdUsuarioCreador = idCreador,
                Titulo = entrada.Titulo,
                Descripcion = entrada.Descripcion,
                FechaInicio = entrada.FechaInicio,
                FechaFin = entrada.FechaFin ?? DateTime.MaxValue
            };

            await _db.EntradasCalendario.AddAsync(entity);

            var calendarios = await _getCalendariosUsuarioQuery.execute(idUsuario);
            
            var calendario = calendarios.FirstOrDefault(c => c.Id == idCalendario);
            if (calendario != null)
            {
                //Creamos al entrada del calendario
                CalendarioUsuarioEntradasEntity calendarioUsuarioEntradasEntity = new CalendarioUsuarioEntradasEntity
                {
                   IdCalendarioUsuario = idCalendario,
                   IdEntradaCalendario = id
                };
                await _db.CalendariosUsuariosEntradas.AddAsync(calendarioUsuarioEntradasEntity);
                await _db.SaveAsync();
            } else
            {   
                throw new Exception("No tiene acceso al calendario indicado");
            }

            entrada.Id = id;
            return entrada;
        }
    }
}
