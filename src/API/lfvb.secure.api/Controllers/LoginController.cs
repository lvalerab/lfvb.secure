using lfvb.secure.aplication.Database.Usuario.Queries.LoginToken;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly string secret;
        private readonly int expires;

        public LoginController(ILogger<LoginController> logger, ILoginUsuarioPasswordQuery qryLoginUsPw, ILoginTokenQuery qryLoginToken, IJwtTokenUtils jwtUtils, IConfiguration config)
        {
            this._logger = logger;
            this._qryLoginUsPw = qryLoginUsPw;
            this._qryLoginToken = qryLoginToken;
            this._jwtTokenUtils = jwtUtils;
            secret = config.GetSection("jwt").GetSection("secret").ToString()??"";
            if(!Int32.TryParse(config.GetSection("jwt").GetSection("expires_minutes").ToString(),out expires))
            {
                expires = 5;
            }
        }

        /// <summary>
        /// Método para validar un usuario con usuario y contraseña
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("jwt")]
        public async Task<IActionResult> ValidarUsuario([FromBody] LoginUsuarioPasswordModel login)
        {
            if(login == null)
            {
                return BadRequest("No se ha indicado los datos del usuario");
            } else if(login.Usuario=="" || login.Password=="")
            {
                return BadRequest("Se ha de indicar un usuario y un password");
            } else
            {
                login=await this._qryLoginUsPw.Execute(login);
                if(login!=null)
                {
                    string token = this._jwtTokenUtils.GetToken((login.Id??Guid.Empty).ToString(), this.secret, this.expires);
                    string token2 = this._jwtTokenUtils.GetToken((login.Id??Guid.Empty).ToString() + "RF", this.secret, this.expires + 1);
                    this._logger.LogInformation("Acceso autorizado",login);
                    return StatusCode(StatusCodes.Status200OK, new { token = token, tokenrf = token2 });
                } else
                {
                    this._logger.LogWarning("Fallo en la validacion del usuario", login);
                    //Si no ha devuelto nada, no esta autorizado
                    return Unauthorized();
                }
            }
        }

        /// <summary>
        /// Validar por token fijo de maquina
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> ValidarToken([FromBody] LoginTokenModel login)
        {
            if(login==null)
            {
                return BadRequest();
            } else if(login.Token==null || login.Token.IsNullOrEmpty())
            {
                return BadRequest();
            } else
            {
                login = await this._qryLoginToken.Execute(login);
                if(login!=null)
                {
                    string token = this._jwtTokenUtils.GetToken((login.Id??Guid.Empty).ToString(), this.secret, this.expires);
                    string token2 = this._jwtTokenUtils.GetToken((login.Id ?? Guid.Empty).ToString()+"RF", this.secret, this.expires+1);
                    return StatusCode(StatusCodes.Status200OK, new { token = token, tokenrf=token2 });
                } else
                {
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
            try { 
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                return StatusCode(StatusCodes.Status200OK,new { id=id});
            } catch(Exception ex)
            {
                this._logger.LogError("Error al obtener el ID del token", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Si esta validado, obtiene un nuevo token a base del token de refresco
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("refresh")]
        public IActionResult GetNewToken()
        {
            Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext,true);
            if(id!=null)
            {
                string token = this._jwtTokenUtils.GetToken((id??Guid.Empty).ToString(), this.secret, this.expires);
                string token2 = this._jwtTokenUtils.GetToken((id??Guid.Empty).ToString() + "RF", this.secret, this.expires + 1);
                return StatusCode(StatusCodes.Status200OK, new { token = token, tokenrf = token2 });
            } else
            {
                return Unauthorized();
            }
        }
    }
}
