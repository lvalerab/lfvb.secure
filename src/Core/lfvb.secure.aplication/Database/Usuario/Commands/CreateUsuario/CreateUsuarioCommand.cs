using AutoMapper;
using lfvb.secure.aplication.Database.Credencial.Commands.CrearCredencialUsuario;
using lfvb.secure.aplication.Database.Credencial.Models;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.common.PASSWORD;
using lfvb.secure.domain.Entities.Credencial;
using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.PasswordCredencial;
using lfvb.secure.domain.Entities.TokenCredencial;
using lfvb.secure.domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario
{
    public class CreateUsuarioCommand: ICreateUsuarioCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;
        private readonly ICrearCredencialUsuarioCommand _crearCredencialCommand;
        public bool transacion { get; set; }

        public CreateUsuarioCommand(IDataBaseService db, IMapper mapper, ICrearCredencialUsuarioCommand crearCredencialCommand)
        {
            this._db = db;
            this._mapper = mapper;
            this._crearCredencialCommand = crearCredencialCommand;
            this.transacion = false;
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

                int count = await _db.Usuarios.CountAsync(x => x.Usuario == usuario.Usuario);

                if(count<=0)
                {
                    if (usuario.Password == null && usuario.Token == null)
                    {
                        throw new Exception("[DATA] Debe indicar al menos un password o un token");
                    }
                    Guid gnew = Guid.NewGuid();
                    UsuarioEntity entity = _mapper.Map<UsuarioEntity>(usuario);
                    entity.Id = gnew;
                    await _db.Usuarios.AddAsync(entity);
                    ElementoEntity elem = new ElementoEntity { Id = entity.Id, CodigoTipoElemento = "user" };
                    await _db.Elementos.AddAsync(elem); //Registramos el elemento creado, para las propiedades y demas
                    if (usuario.Password != null)
                    {
                        CredencialModel credencial = new CredencialModel
                        {
                            Usuario = new UsuarioModel
                            {
                                Id = entity.Id
                            },
                            Tipo = new TipoCrendecial.Models.TipoCredencialModel
                            {
                                Codigo = "PASS"
                            },
                            Password = new PasswordCredencial
                            {
                                Password = usuario.Password
                            }
                        };
                        credencial = await _crearCredencialCommand.execute(credencial);
                    }
                    if (usuario.Token != null)
                    {
                        CredencialModel credencial = new CredencialModel
                        {
                            Usuario = new UsuarioModel
                            {
                                Id = entity.Id
                            },
                            Tipo = new TipoCrendecial.Models.TipoCredencialModel
                            {
                                Codigo = "PASS"
                            },
                            Token = new TokenCredencial
                            {
                                Token = usuario.Token
                            }
                        };
                        credencial = await _crearCredencialCommand.execute(credencial);
                    }
                    if (!this.transacion)
                    {
                        await _db.SaveAsync();
                    }
                    usuario.IdNuevo = gnew;
                    return usuario;

                } else
                {
                   throw new Exception("[DATA] El usuario ya existe en la base de datos");
                }
            }
            catch (Exception err) {
                return null;
            }
        }
    }
}
