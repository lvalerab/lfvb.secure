using lfvb.secure.aplication.Database.Calendario.Queries.GetCalendariosUsuario;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers.Calendarios
{
    /// <summary>
    /// Controlador del calendario
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarioController : ControllerBase
    {
        private ILogger<CalendarioController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetCalendariosUsuarioQuery _getCalendariosUsuarioQuery;

        public CalendarioController(ILogger<CalendarioController> logger,
            IJwtTokenUtils jwtTokenUtils,
            IGetCalendariosUsuarioQuery getCalendariosUsuarioQuery)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _getCalendariosUsuarioQuery = getCalendariosUsuarioQuery;
        }

        /// <summary>
        /// Obtiene los calendarios del usuario autenticado
        /// </summary>
        /// <returns></returns>
        [HttpGet("calendarios")]
        [Authorize]
        public async Task<IActionResult> GetCalendarios()
        {
            try
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                // Aquí iría la lógica para obtener los calendarios
                var calendarios = await _getCalendariosUsuarioQuery.execute(id??Guid.Empty);
                return Ok(calendarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los calendarios");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
