using AutoMapper;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
using System.Linq.Expressions;


namespace lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario
{
    public class CreateUsuarioCommand: ICreateUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public CreateUsuarioCommand(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
       
        public async Task<CreateUsuarioModel> Execute(CreateUsuarioModel usuario)
        {
            try { 
                if (usuario == null)
                {
                    throw new Exception("[DATA] Debe indicar un objeto de usuario");
                } else if(usuario.Usuario == String.Empty)
                {
                    throw new Exception("[DATAFIELD] El usuario debe estar relleno");
                }
                {
                    if(usuario.Password == null && usuario.Token ==null)
                    {
                        throw new Exception("[DATA] Debe indicar al menos un password o un token");
                    }
                    Guid gnew = Guid.NewGuid();
                    UsuarioEntity entity = _mapper.Map<UsuarioEntity>(usuario);
                    entity.Id = gnew;
                    await _db.Usuarios.AddAsync(entity);
                    if(usuario.Password != null)
                    {                    
                        CredencialEntity credencial = new()
                        {   
                            IdUsuario = entity.Id,
                            CodigoTipoCredencial="PASS"                    
                        };
                        await _db.Credenciales.AddAsync(credencial);
                        PasswordCredencialEntity PassCredential = new()
                        {
                            Id = credencial.Id,
                            Password = usuario.Password
                        };
                        await _db.Passwords.AddAsync(PassCredential);
                    }
                    if(usuario.Token!=null)
                    {
                        CredencialEntity credencial = new()
                        {
                            IdUsuario = entity.Id,
                            CodigoTipoCredencial = "TOKEN"
                        };
                        await _db.Credenciales.AddAsync(credencial);
                        TokenCredencialEntity token = new()
                        {
                            Id = credencial.Id,
                            Token = usuario.Token
                        };
                        await _db.Tokens.AddAsync(token);
                    }
                    await _db.SaveAsync();
                    usuario.IdNuevo = gnew;
                    return usuario;
                
                }
            }
            catch (Exception err) {
                return null;
            }
        }
    }
}
