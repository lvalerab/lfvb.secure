using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.common.JWT;
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
        private IJwtTokenUtils _jwtTokenUtils;
        private readonly string secret;

        public LoginController(ILoginUsuarioPasswordQuery qryLoginUsPw, IJwtTokenUtils jwtUtils, IConfiguration config)
        {
            this._qryLoginUsPw = qryLoginUsPw;
            this._jwtTokenUtils = jwtUtils;
            secret = config.GetSection("jwt").GetSection("secret").ToString();
        }

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
                login=await this._qryLoginUsPw.ValidarQuery(login);
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
    }
}
