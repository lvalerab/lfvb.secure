using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Database.Usuario.Commands.ActualizaUsuario;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Database.Usuario.Queries.GetUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginToken;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace lfvb.secure.api.Controllers
{
    /// <summary>
    /// Controlador para identificar los usuarios y obtener el token de seguridad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogger<LoginController> _logger;
        private ILoginUsuarioPasswordQuery _qryLoginUsPw;
        private ILoginTokenQuery _qryLoginToken;
        private IJwtTokenUtils _jwtTokenUtils;
        private IConfiguration _configuration;
        private readonly string secret;
        private readonly int expires;
        private IGetUsuarioQuery _qryGetUsuario;
        private IActualizaUsuarioCommand _cmdActualizaUsuario;
        private IGetPropiedadesElementoQuery _getPropiedadesElementoQuery;
        private INuevaActualizaPropiedadElementoCommand _nuevaActualizaPropiedadElementoCommand;

        public LoginController(ILogger<LoginController> logger,
                               ILoginUsuarioPasswordQuery qryLoginUsPw,
                               ILoginTokenQuery qryLoginToken,
                               IJwtTokenUtils jwtUtils,
                               IConfiguration config,
                               IGetUsuarioQuery qryGetUsuario,
                               IActualizaUsuarioCommand cmdActualizarUsuario,
                               IGetPropiedadesElementoQuery getPropiedadesElementoQuery,
                               INuevaActualizaPropiedadElementoCommand nuevaActualizaPropiedadElementoCommand)
        {
            this._logger = logger;
            this._qryLoginUsPw = qryLoginUsPw;
            this._qryLoginToken = qryLoginToken;
            this._jwtTokenUtils = jwtUtils;
            this._configuration = config;
            secret = config.GetSection("jwt").GetSection("secret").ToString() ?? "";
            if (!Int32.TryParse(config.GetSection("jwt").GetSection("expires_minutes").ToString(), out expires))
            {
                expires = 5;
            }
            this._qryGetUsuario = qryGetUsuario;
            this._cmdActualizaUsuario = cmdActualizarUsuario;
            this._getPropiedadesElementoQuery = getPropiedadesElementoQuery;
            this._nuevaActualizaPropiedadElementoCommand = nuevaActualizaPropiedadElementoCommand;
        }

        /// <summary>
        /// Método para validar un usuario con usuario y contraseña
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> ValidarUsuario([FromBody] LoginUsuarioPasswordModel login)
        {
            if (login == null)
            {
                return BadRequest("No se ha indicado los datos del usuario");
            }
            else if (login.Usuario == "" || login.Password == "")
            {
                return BadRequest("Se ha de indicar un usuario y un password");
            }
            else
            {
                login = await this._qryLoginUsPw.Execute(login);
                if (login != null)
                {
                    string token = this._jwtTokenUtils.GetToken(
                        login.Id.ToString(),
                        login.Usuario,
                        this._configuration.GetSection("jwt").GetValue<string>("Subject", ""),
                        this._configuration.GetSection("jwt").GetValue<string>("Issuer", ""),
                        this._configuration.GetSection("jwt").GetValue<string>("Audience", ""),
                        this._configuration.GetSection("jwt").GetValue<string>("secret", ""),
                        this._configuration.GetSection("jwt").GetValue<int>("expires_minutes", 0)
                        );

                    this._logger.LogInformation("Acceso autorizado", new { login = login });
                    return StatusCode(StatusCodes.Status200OK, new { token = token });
                }
                else
                {
                    this._logger.LogWarning("Fallo en la validacion del usuario", new { login = login });
                    //Si no ha devuelto nada, no esta autorizado
                    return Unauthorized();
                }
            }
        }

        /// <summary>
        /// Método para validar una maquina (automatizacion) con token
        /// </summary>
        /// <param name="token">Token generado para identificar la maquina</param>
        /// <returns></returns>
        [HttpPost]
        [Route("maquina/login")]
        public async Task<IActionResult> ValidaUsuarioToken([FromBody] string ptoken)
        {
            if (ptoken == null)
            {
                this._logger.LogWarning("Se ha intentando obtener un token de maquina sin especificar el token de la maquina");
                return BadRequest("Debe identificar el proceso o maquina a través de su token");
            }
            else
            {
                LoginTokenModel login = await this._qryLoginToken.Execute(new LoginTokenModel { Token = ptoken });
                if (login != null)
                {
                    string token = this._jwtTokenUtils.GetToken(
                        login.Id.ToString(),
                        ptoken,
                        this._configuration.GetSection("jwt").GetValue<string>("Subject", ""),
                        this._configuration.GetSection("jwt").GetValue<string>("Issuer", ""),
                        this._configuration.GetSection("jwt").GetValue<string>("Audience", ""),
                        this._configuration.GetSection("jwt").GetValue<string>("secret", ""),
                        this._configuration.GetSection("jwt").GetValue<int>("expires_minutes", 0)
                        );

                    this._logger.LogInformation("Acceso autorizado", new { login = login });
                    return StatusCode(StatusCodes.Status200OK, new { token = token });
                }
                else
                {
                    this._logger.LogWarning("Fallo en la validacion del proceso/maquina", new { token = ptoken });
                    //Si no ha devuelto nada, no esta autorizado
                    return Unauthorized();
                }
            }
        }

        /// <summary>
        /// Obtener el indentificador de usuario validado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("guid")]
        public IActionResult GetUserAutenticatedId()
        {
            try
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                if (id != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { id = id });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al obtener el ID del token", new { ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene los datos del usuario autenticado a través del token de seguridad
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("usuario")]
        public async Task<IActionResult> GetUsuarioToken()
        {
            try
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                if (id != null)
                {
                    UsuarioModel usuario = await this._qryGetUsuario.Execute(id);
                    if (usuario != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, usuario);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Usuario no encontrado");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al obtener el usuario del token", new { ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza los datos del usuario autenticado a través del token de seguridad
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("usuario")]
        public async Task<IActionResult> ActualizaUsuario([FromBody] ActualizaUsuarioModel usuario)
        {
            try
            {
                if (usuario == null || usuario.Id == null)
                {
                    return BadRequest("No se ha indicado el usuario a actualizar");
                }
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                if (id != null && id == usuario.Id)
                {
                    ActualizaUsuarioModel usuarioActualizado = await this._cmdActualizaUsuario.Execute(usuario);
                    if (usuarioActualizado != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, usuarioActualizado);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Usuario no encontrado o no actualizado");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al actualizar el usuario", new { ex = ex });
                return BadRequest();
            }

        }

        /// <summary>
        /// Obtiene el avatar del usuario loggeado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("usuario/avatar")]
        public async Task<IActionResult> GetAvatarUsuarioLoggeado()
        {
            try
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                if (id != null)
                {
                    PropiedadElementoModel prop = null;
                    List<PropiedadElementoModel> propiedades = await this._getPropiedadesElementoQuery.Execute(new List<Guid?> { id ?? Guid.Empty }, new List<string> { "IMG_USER" });
                    if (propiedades.Count > 0)
                    {
                        prop = propiedades[0];
                        return Ok(prop.Valores[0].Texto);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Avatar no actualizado, no se encuentra");
                    }
                } else
                {
                    return Unauthorized();
                }
            } catch (Exception ex)
            {
                this._logger.LogError("Error al obtener el avatar del usuario", new { ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza el avatar del usuario loggueado
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("usuario/avatar")]
        public async Task<IActionResult> ActualizaAvatarUsuario([FromBody] string base64)
        {

            try
            {
                if (base64 == null)
                {
                    return BadRequest("No se ha indicado el avatar a actualizar");
                }
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                if (id != null)
                {
                    PropiedadElementoModel prop = null;
                    List<PropiedadElementoModel> propiedades = await this._getPropiedadesElementoQuery.Execute(new List<Guid?> { id ?? Guid.Empty }, new List<string> { "IMG_USER"});
                    if(propiedades.Count>0)
                    {
                        prop = propiedades[0];                        
                    } else
                    {
                        prop = new PropiedadElementoModel
                        {
                            Id = null,
                            IdElemento = id,
                            Propiedad = new aplication.Database.Propiedades.Queries.GetAllPropiedades.PropiedadModel
                            {
                                Codigo = "IMG_USER"
                            },
                            Valores = new List<ValorPropiedadModel>()
                        };
                        prop.Valores.Add(new ValorPropiedadModel { Id=null,IdPropiedadElemento=null, Texto=null });
                    }
                    prop.Valores[0].Texto = base64;
                    prop=await this._nuevaActualizaPropiedadElementoCommand.Execute(prop);
                    if (prop != null)
                    {
                        return StatusCode(StatusCodes.Status200OK, prop);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Avatar no actualizado, no se encuentra");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al actualizar el avatar del usuario", new { ex = ex });
                return BadRequest();
            }
        }
    }
}
