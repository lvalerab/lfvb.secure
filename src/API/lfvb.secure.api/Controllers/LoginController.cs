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
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginUsuarioPasswordQuery _qryLoginUsPw;
        private ILoginTokenQuery _qryLoginToken;
        private IJwtTokenUtils _jwtTokenUtils;
        private readonly string secret;

        public LoginController(ILoginUsuarioPasswordQuery qryLoginUsPw, ILoginTokenQuery qryLoginToken, IJwtTokenUtils jwtUtils, IConfiguration config)
        {
            this._qryLoginUsPw = qryLoginUsPw;
            this._qryLoginToken = qryLoginToken;
            this._jwtTokenUtils = jwtUtils;
            secret = config.GetSection("jwt").GetSection("secret").ToString();
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
                    string token = this._jwtTokenUtils.GetToken(login.Id.ToString(), this.secret, 5);                  
                    return StatusCode(StatusCodes.Status200OK, new { token=token});
                } else
                {
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
                    string token = this._jwtTokenUtils.GetToken(login.Id.ToString(), this.secret, 5);
                    return StatusCode(StatusCodes.Status200OK, new { token = token });
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
                return BadRequest();
            }
        }
    }
}
