using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common.PASSWORD;
using lfvb.secure.domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword
{
    public class LoginUsuarioPasswordQuery : ILoginUsuarioPasswordQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        private readonly ISecurePassword _securePassword;

        public LoginUsuarioPasswordQuery(IDataBaseService db, IMapper mapper, ISecurePassword securePassword)
        {
            this._db = db;
            this._mapper=mapper;
            this._securePassword = securePassword;
        }

        public async Task<LoginUsuarioPasswordModel> Execute(LoginUsuarioPasswordModel parameters)
        {
            string hspw = this._securePassword.Crypt(parameters.Password);
            LoginUsuarioPasswordModel encontrado = await (from us in this._db.Usuarios
                                              join cr in this._db.Credenciales on us.Id equals cr.IdUsuario
                                                          join pw in this._db.Passwords on cr.Id equals pw.Id
                                              where (cr.VigenteDesde <= DateTime.Now) && ((cr.VigenteHasta ?? DateTime.Now) >= DateTime.Now)
                                                  && us.Usuario == parameters.Usuario
                                                  && pw.Password == hspw
                                              select new LoginUsuarioPasswordModel
                                              {
                                                  Id=us.Id,
                                                  Usuario=us.Usuario,
                                                  Password=pw.Password
                                              }
                ).FirstOrDefaultAsync();

            return encontrado;
        }
    }
}
