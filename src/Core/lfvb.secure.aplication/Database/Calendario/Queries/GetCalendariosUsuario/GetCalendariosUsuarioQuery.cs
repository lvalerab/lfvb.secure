using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Calendario.Queries.GetCalendariosUsuario
{
    public class GetCalendariosUsuarioQuery : IGetCalendariosUsuarioQuery
    {

        private readonly IDataBaseService _db;

        public GetCalendariosUsuarioQuery(IDataBaseService db)
        {
            _db = db;
        }

        public async Task<List<CalendarioModel>> execute(Guid idUsuario)
        {
            List<CalendarioModel> calendarios = await (from cl in _db.CalendariosUsuarios
                                                              where cl.Usuario.Id == idUsuario
                                                              select new Models.CalendarioModel
                                                              {
                                                                  Id = cl.Id,
                                                                  Nombre = cl.Nombre,
                                                                  Usuario = new Usuario.Models.UsuarioModel
                                                                  {
                                                                        Id = cl.Usuario.Id,
                                                                        Usuario = cl.Usuario.Usuario,
                                                                        Nombre = cl.Usuario.Nombre,
                                                                        Apellido1 = cl.Usuario.Apellido1,
                                                                        Apellido2 = cl.Usuario.Apellido2,
                                                                        Email = cl.Usuario.Email
                                                                  },
                                                                  Entradas = new List<Models.EntradaCalendarioModel>()
                                                              }).ToListAsync();



            return calendarios;

        }
    }
}
