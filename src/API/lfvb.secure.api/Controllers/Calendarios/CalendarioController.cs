using lfvb.secure.aplication.Database.Calendario.Commands.AltaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.AltaEntradaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.DesrelacionaEntradaCalendarioUsuario;
using lfvb.secure.aplication.Database.Calendario.Commands.EliminaEntradaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.EliminarCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.ModificaEntradaCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.ModificarCalendario;
using lfvb.secure.aplication.Database.Calendario.Commands.RelacionaEntradaCalendarioUsuario;
using lfvb.secure.aplication.Database.Calendario.Models;
using lfvb.secure.aplication.Database.Calendario.Queries.GetCalendariosUsuario;
using lfvb.secure.aplication.Database.Calendario.Queries.GetEntradasCalendario;
using lfvb.secure.aplication.Database.Usuario.Models;
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
        private IGetEntradaCalendario _getEntradasCalendarioQuery;

        private IAltaCalendarioUsuarioCommand _altaCalendarioUsuarioCommand;
        private IModificarCalendarioCommand _modificarCalendarioCommand;
        private IEliminarCalendarioCommand _eliminarCalendarioCommand;


        private IAltaEntradaCalendarioUsuarioCommand _altaEntradaCalendarioUsuarioCommand;
        private IModificarEntradaCalendarioCommand _modificarEntradaCalendarioUsuarioCommand;
        private IEliminarEntradaCalendarioCommand _eliminarEntradaCalendarioUsuarioCommand;

        private IRelacionarEntradaCalendarioUsuarioCommand _relacionarEntradaCalendarioUsuarioCommand;
        private IDesrelacionarEntradaCalendarioUsuarioCommand _desrelacionarEntradaCalendarioUsuarioCommand;

        public CalendarioController(ILogger<CalendarioController> logger,
            IJwtTokenUtils jwtTokenUtils,
            IGetEntradaCalendario getEntradasCalendarioQuery,
            IGetCalendariosUsuarioQuery getCalendariosUsuarioQuery,
            IAltaCalendarioUsuarioCommand altaCalendarioUsuarioCommand,
            IModificarCalendarioCommand modificarCalendarioCommand,
            IEliminarCalendarioCommand eliminarCalendarioCommand,
            IAltaEntradaCalendarioUsuarioCommand altaEntradaCalendarioUsuarioCommand,
            IModificarEntradaCalendarioCommand modificarEntradaCalendarioCommand,
            IEliminarEntradaCalendarioCommand eliminarEntradaCalendarioCommand,
            IRelacionarEntradaCalendarioUsuarioCommand relacionarEntradaCalendarioUsuarioCommand,
            IDesrelacionarEntradaCalendarioUsuarioCommand desrelacionarEntradaCalendarioUsuarioCommand
            )
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _getEntradasCalendarioQuery = getEntradasCalendarioQuery;
            _getCalendariosUsuarioQuery = getCalendariosUsuarioQuery;
            _altaCalendarioUsuarioCommand = altaCalendarioUsuarioCommand;            
            _modificarCalendarioCommand = modificarCalendarioCommand;
            _eliminarCalendarioCommand = eliminarCalendarioCommand;

            _altaEntradaCalendarioUsuarioCommand = altaEntradaCalendarioUsuarioCommand;
            _modificarEntradaCalendarioUsuarioCommand = modificarEntradaCalendarioCommand;
            _eliminarEntradaCalendarioUsuarioCommand = eliminarEntradaCalendarioCommand;

            _relacionarEntradaCalendarioUsuarioCommand = relacionarEntradaCalendarioUsuarioCommand;
            _desrelacionarEntradaCalendarioUsuarioCommand = desrelacionarEntradaCalendarioUsuarioCommand;
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
                var calendarios = await _getCalendariosUsuarioQuery.execute(id ?? Guid.Empty);
                return Ok(calendarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los calendarios");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Crea un nuevo calendario para el usuario autenticado
        /// </summary>
        /// <param name="calendario"></param>
        /// <returns></returns>
        [HttpPost("calendario")]
        [Authorize]
        public async Task<IActionResult> AltaCalendario([FromBody] CalendarioModel calendario)
        {
            try
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                calendario.Usuario = new UsuarioModel { Id = id ?? Guid.Empty };
                // Aquí iría la lógica para dar de alta un calendario
                var nuevoCalendario = await _altaCalendarioUsuarioCommand.execute(calendario);
                return Ok(nuevoCalendario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta el calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Modifica un calendario existente del usuario autenticado
        /// </summary>
        /// <param name="calendario"></param>
        /// <returns></returns>
        [HttpPut("calendario")]
        [Authorize]
        public async Task<IActionResult> ModificarCalendario([FromBody] CalendarioModel calendario)
        {
            try
            {
                Guid? id = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                calendario.Usuario = new UsuarioModel { Id = id ?? Guid.Empty };
                if(calendario.Usuario.Id != id)
                {
                    return Unauthorized();
                } else { 
                    // Aquí iría la lógica para modificar un calendario
                    var calendarioModificado = await _modificarCalendarioCommand.execute(calendario);
                    return Ok(calendarioModificado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar el calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Elimina un calendario de un usuario determinado, si se indica un calendario destino, las entradas del calendario eliminado se moverán a ese calendario destino
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idCalendarioDestino"></param>
        /// <returns></returns>
        [HttpDelete("calendario/{id}")]
        [Authorize]
        public async Task<IActionResult> EliminarCalendario(Guid id, [FromQuery] Guid? idCalendarioDestino = null)
        {
            try
            {
                Guid? userId = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                var calendarios = await _getCalendariosUsuarioQuery.execute(userId ?? Guid.Empty);
                if (calendarios.Find(x => x.Id == id) == null)
                {
                    return Unauthorized();
                }
                else
                {
                    // Aquí iría la lógica para eliminar un calendario
                    var resultado = await _eliminarCalendarioCommand.execute(id, idCalendarioDestino);
                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Obtiene las entradas de un calendario específico del usuario autenticado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("calendario/{id}/entradas")]
        [Authorize]
        public async Task<IActionResult> GetEntradasCalendario(Guid id)
        {
            try
            {
                Guid? userId = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                var calendarios = await _getCalendariosUsuarioQuery.execute(userId ?? Guid.Empty);
                if (calendarios.Find(x => x.Id == id) == null)
                {
                    return Unauthorized();
                }
                else
                {
                    // Aquí iría la lógica para obtener las entradas del calendario
                    var entradas = await _getEntradasCalendarioQuery.execute(id, DateTime.Now.AddDays(-15), DateTime.Now.AddDays(15));
                    return Ok(entradas);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las entradas del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        
        
        /// <summary>
        /// Añade una nueva entrada al calendario indicado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [HttpPost("calendario/{id}/entrada")]
        [Authorize]
        public async Task<IActionResult> AltaEntradaCalendario(Guid id, [FromBody] EntradaCalendarioModel entrada)
        {
            try
            {
                Guid? userId = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                var calendarios = await _getCalendariosUsuarioQuery.execute(userId ?? Guid.Empty);
                if (calendarios.Find(x => x.Id == id) == null)
                {
                    return Unauthorized();
                }
                else
                {   
                    // Aquí iría la lógica para dar de alta una entrada del calendario
                    var nuevaEntrada = await _altaEntradaCalendarioUsuarioCommand.execute(entrada,userId??Guid.Empty, userId??Guid.Empty, id);
                    return Ok(nuevaEntrada);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta la entrada del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Modifica una entrada indicada
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        [HttpPut("calendario/entrada")]
        [Authorize]
        public async Task<IActionResult> ModificarEntradaCalendario([FromBody] EntradaCalendarioModel entrada)
        {
            try
            {
                var modificarEntrada = await _modificarEntradaCalendarioUsuarioCommand.execute(entrada);
                return Ok(modificarEntrada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la entrada del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Elimina una entrada del calendario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("calendario/entrada/{id}")]
        [Authorize]
        public async Task<IActionResult> EliminarEntradaCalendario(Guid id)
        {
            try
            {
                bool exito = await _eliminarEntradaCalendarioUsuarioCommand.execute(id);
                return Ok(exito);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la entrada del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Relaciona una entrada a un calendario existente
        /// </summary>
        /// <param name="idCalendario"></param>
        /// <param name="idEntrada"></param>
        /// <returns></returns>
        [HttpGet("calendario/{idCalendario}/entrada/{idEntrada}/relacionar")]
        [Authorize]
        public async Task<IActionResult> RelacionarEntradaCalendarioUsuario(Guid idCalendario, Guid idEntrada)
        {
            try
            {
                bool exito = await _relacionarEntradaCalendarioUsuarioCommand.execute(idEntrada, idCalendario, true);
                return Ok(exito);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al relacionar la entrada del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Relaciona una entrada a un calendario existente
        /// </summary>
        /// <param name="idCalendario"></param>
        /// <param name="idEntrada"></param>
        /// <returns></returns>
        [HttpGet("calendario/{idCalendario}/entrada/{idEntrada}/desrelacionar")]
        [Authorize]
        public async Task<IActionResult> DesrelacionarEntradaCalendarioUsuario(Guid idCalendario, Guid idEntrada)
        {
            try
            {
                bool exito = await _desrelacionarEntradaCalendarioUsuarioCommand.execute(idEntrada, idCalendario, true);
                return Ok(exito);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desrelacionar la entrada del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        /// <summary>
        /// Obtiene las entradas de un calendario específico del usuario autenticado con filtros
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpPost("calendario/entradas/buscar")]
        [Authorize]
        public async Task<IActionResult> GetEntradasCalendarioPost([FromBody] FiltroEntradaCalendarioModel filtro)
        {
            try
            {
                Guid? userId = this._jwtTokenUtils.GetIdFromToken(HttpContext);
                var calendarios = await _getCalendariosUsuarioQuery.execute(userId ?? Guid.Empty);
                if (calendarios.Find(x => x.Id == filtro.IdCalendario) == null)
                {
                    return Unauthorized();
                }
                else
                {
                    // Aquí iría la lógica para obtener las entradas del calendario
                    var entradas = await _getEntradasCalendarioQuery.execute(filtro.IdCalendario, filtro.FechaInicio, filtro.FechaFin);
                    return Ok(entradas);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las entradas del calendario");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
