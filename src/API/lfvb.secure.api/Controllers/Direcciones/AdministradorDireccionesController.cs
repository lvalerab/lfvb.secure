using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lfvb.secure.api.Atributos.Secure;

namespace lfvb.secure.api.Controllers.Direcciones
{
    [ApiController]
    [Route("api/administracion/direcciones")]
    public class AdministradorDireccionesController : ControllerBase
    {
        private ILogger<AdministradorDireccionesController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;


        public AdministradorDireccionesController(ILogger<AdministradorDireccionesController> logger,
                                                IJwtTokenUtils jwtTokenUtils)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok("OK");
        }
    }
}
