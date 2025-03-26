using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common.PASSWORD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.LoginToken
{
    public class LoginTokenQuery:ILoginTokenQuery
    {

        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        private readonly ISecurePassword _securePassword;

        public LoginTokenQuery(IDataBaseService db, IMapper mapper, ISecurePassword securePassword)
        {
            this._db = db;
            this._mapper = mapper;
            this._securePassword = securePassword;
        }

        public async Task<LoginTokenModel?> Execute(LoginTokenModel parameters)
        {
            if(parameters!=null) {
                string hstk = this._securePassword.Crypt(parameters.Token);
                LoginTokenModel  encontrado = await(from us in this._db.Usuarios
                                                             join cr in this._db.Credenciales on us.Id equals cr.IdUsuario
                                                             join tk in this._db.Tokens on cr.Id equals tk.Id
                                                             where (cr.VigenteDesde <= DateTime.Now) && ((cr.VigenteHasta ?? DateTime.Now) >= DateTime.Now)
                                                                 && tk.Token==hstk
                                                             select new LoginTokenModel
                                                             {
                                                                 Id = us.Id
                                                             }
                   ).FirstOrDefaultAsync();

                return encontrado;
            } else
            {
                return null;
            }
        }
    }
}
