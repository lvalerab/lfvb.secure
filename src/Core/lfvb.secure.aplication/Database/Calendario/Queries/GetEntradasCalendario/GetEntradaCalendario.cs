using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Queries.GetEntradasCalendario
{
    public class GetEntradaCalendario: IGetEntradaCalendario
    {
        private readonly IDataBaseService _db;

        public GetEntradaCalendario(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<EntradaCalendarioModel>> execute(Guid idCalendario, DateTime? fechaInicio = null, DateTime? FechaFin = null)
        {
            List<EntradaCalendarioModel> entradas = await (from ec in _db.EntradasCalendario.Include(e => e.TipoEntrada).Include(e=>e.UsuarioCreador)
                                                           from rc in _db.CalendariosUsuariosEntradas
                                                           where ec.Id == rc.IdEntradaCalendario
                                                             && rc.IdCalendarioUsuario == idCalendario
                                                             && (fechaInicio == null || (ec.FechaInicio >= fechaInicio))
                                                             && (FechaFin == null || (ec.FechaFin <= FechaFin))
                                                           select new EntradaCalendarioModel
                                                           {
                                                               Id=ec.Id,
                                                               TipoEntrada=new TipoEntradaCalendarioModel
                                                               {
                                                                   Id=ec.TipoEntrada.Id,
                                                                   Codigo=ec.TipoEntrada.Codigo,
                                                                   Nombre=ec.TipoEntrada.Nombre
                                                               },
                                                               FechaInicio=ec.FechaInicio,
                                                               FechaFin=ec.FechaFin,
                                                               Creador=new Usuario.Models.UsuarioModel
                                                               {
                                                                   Id=ec.UsuarioCreador.Id,
                                                                   Usuario=ec.UsuarioCreador.Usuario,
                                                                   Nombre=ec.UsuarioCreador.Nombre,
                                                                   Apellido1=ec.UsuarioCreador.Apellido1,
                                                                   Apellido2=ec.UsuarioCreador.Apellido2
                                                               },
                                                               Titulo=ec.Titulo,
                                                               Descripcion=ec.Descripcion,
                                                               Participantes=new List<ParticipanteEntradaCalendarioModel>()
                                                           }).ToListAsync();

            return entradas;
        }
    }
}
