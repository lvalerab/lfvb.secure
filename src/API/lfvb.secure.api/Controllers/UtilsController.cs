using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginToken;
using lfvb.secure.aplication.Database.Usuario.Queries.LoginUsuarioPassword;
using lfvb.secure.common.JWT;
using lfvb.secure.common.PASSWORD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private ILogger<LoginController> _logger;
        private ISecurePassword _securePassword;

        public UtilsController(ILogger<LoginController> logger, ISecurePassword securePassword)
        {
            this._logger = logger;
            this._securePassword = securePassword;
        }


        [HttpGet]
        [Route("ping")]
        public Task<IActionResult> Ping()
        {
            return Task.FromResult<IActionResult>(Ok("Pong"));
        }

        [HttpPost]
        //[Authorize]
        [Route("pass/encrypt")]
        public Task<IActionResult> PasswordEncript([FromBody] string password)
        {   
            string encrypted = this._securePassword.Crypt(password);
            return Task.FromResult<IActionResult>(Ok(encrypted));
        }

        [HttpPost]
        //[Authorize]
        [Route("pass/decrypt")]
        public Task<IActionResult> PasswordDencript([FromBody] string encripted)
        {
            string password = this._securePassword.Decrypt(encripted);
            return Task.FromResult<IActionResult>(Ok(password));
        }
    }
}
