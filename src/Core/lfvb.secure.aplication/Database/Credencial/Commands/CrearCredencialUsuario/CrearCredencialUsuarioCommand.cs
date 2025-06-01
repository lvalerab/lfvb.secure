using AutoMapper;
using lfvb.secure.aplication.Database.Credencial.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common.PASSWORD;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Credencial.Commands.CrearCredencialUsuario
{
    public class CrearCredencialUsuarioCommand : ICrearCredencialUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;
        private readonly ISecurePassword _securePassword;
        public bool transaccion { get; set; }

        public CrearCredencialUsuarioCommand(IDataBaseService db, IMapper mp, ISecurePassword securePassword)
        {
            _db = db;
            _mp = mp;
            _securePassword = securePassword;
            transaccion = false;
        }

        public async Task<CredencialModel> execute(CredencialModel credencialModel)
        {
            CredencialEntity credencial = new CredencialEntity
            {
                IdUsuario = credencialModel.Usuario.Id ?? Guid.Empty,
                CodigoTipoCredencial = credencialModel.Tipo.Codigo,
                VigenteDesde = DateTime.Now,
                VigenteHasta = null
            };            
            switch(credencialModel.Tipo.Codigo)
            {
                case "PASS":
                    credencial.Password = new PasswordCredencialEntity
                    {
                      Password = this._securePassword.Crypt(credencialModel.Password.Password)
                    };                    
                    break;
                case "TOKEN":
                    credencial.Token = new TokenCredencialEntity
                    {   
                        Token = this._securePassword.Crypt(credencialModel.Token.Token)
                    };                    
                    break;
            }
            await _db.Credenciales.AddAsync(credencial);
            if (!transaccion)
            {
                await _db.SaveAsync();  
            }
            return _mp.Map<CredencialModel>(credencial);
        }
    }
}
