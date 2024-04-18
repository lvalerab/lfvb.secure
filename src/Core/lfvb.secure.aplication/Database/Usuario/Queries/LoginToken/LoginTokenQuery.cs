using AutoMapper;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.aplication.Interfaces;
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

        public LoginTokenQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<LoginTokenModel> Execute(LoginTokenModel parameters)
        {
            LoginTokenModel  encontrado = await(from us in this._db.Usuarios
                                                         join cr in this._db.Credenciales on us.Id equals cr.IdUsuario
                                                         join tk in this._db.Tokens on cr.Id equals tk.Id
                                                         where (cr.VigenteDesde <= DateTime.Now) && ((cr.VigenteHasta ?? DateTime.Now) >= DateTime.Now)
                                                             && tk.Token==parameters.Token
                                                         select new LoginTokenModel
                                                         {
                                                             Id = us.Id,
                                                             Token=tk.Token
                                                         }
               ).FirstOrDefaultAsync();

            return encontrado;
        }
    }
}
