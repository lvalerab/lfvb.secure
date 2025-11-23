using lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries.Estados;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Database.TipoElemento.Queries;
using lfvb.secure.aplication.Database.TipoElementoAplicacion.Queries.GetAllTiposElementosAplicacion;
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
    /// Peticiones generales del core de la aplicacion
    /// </summary>
    [Route("api/[controller]")]
    public class CoreController : ControllerBase
    {
        private ILogger<LoginController> _logger;

        private IGetAllTiposElementosQuery _getAllTiposElementos;
        private IEstadosElementosQuery _qryEstadosElementos;

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreController(ILogger<LoginController> logger,
            IGetAllTiposElementosQuery getAllTiposElementos,
            IEstadosElementosQuery qryEstadosElementos
            )
        {
            _logger = logger;
            _getAllTiposElementos = getAllTiposElementos;
            _qryEstadosElementos = qryEstadosElementos;
        }

        /// <summary>
        /// Obtiene los tipos de elementos existentes en la aplicacion (solo hace falta estar logueado) 
        /// </summary>
        /// <returns></returns>
        [HttpGet("elementos/tipos")]
        [Authorize]
        public async Task<IActionResult> GetAllTiposElementosAplicacion()
        {
            try
            {
                var result = await _getAllTiposElementos.Execute();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo tipos de elementos");
                return StatusCode(500, "Error interno del servidor");
            }

        }

        /// <summary>
        /// Obtiene los estados posibles de los elementos (solo hace falta estar logueado)
        /// </summary>
        /// <returns></returns>
        [HttpGet("elementos/estados")]
        [Authorize]
        public async Task<IActionResult> GetEstadosElementos()
        {
            try
            {
                var result = await _qryEstadosElementos.execute();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo estados de elementos");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
