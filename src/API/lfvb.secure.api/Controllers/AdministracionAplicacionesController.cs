
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAllAplicaciones;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetArbolElementosAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetGruposAplicacion;
using lfvb.secure.aplication.Database.TipoElementoAplicacion.Queries.GetAllTiposElementosAplicacion;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers
{
    [ApiController]
    [Route("api/administracion/aplicaciones")]
    public class AdministracionAplicacionesController : ControllerBase
    {


        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllAplicacionesQuery _qryListaAplicaciones;
        private IGetAplicacionQuery _qryAplicacion;
        private IGetArbolElementosAplicacion _getArbolElementosAplicacion;
        private IGetGruposAplicacionQuery _qryGruposAplicacion;
        private IGetAllTiposElementosAplicacionQuery _qryTiposElementosAplicacion;

        public AdministracionAplicacionesController(ILogger<PermisosController> logger,
                                                    IJwtTokenUtils jwtTokenUtils,
                                                    IGetAllAplicacionesQuery qryListaAplicaciones,
                                                    IGetAplicacionQuery qryAplicacion,
                                                    IGetArbolElementosAplicacion qryArbolElementosAplicacion,
                                                    IGetGruposAplicacionQuery qryGruposAplicacion,
                                                    IGetAllTiposElementosAplicacionQuery qryTiposElementosAplicacion)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryListaAplicaciones = qryListaAplicaciones;
            this._qryAplicacion = qryAplicacion;
            this._getArbolElementosAplicacion = qryArbolElementosAplicacion;
            this._qryGruposAplicacion = qryGruposAplicacion;
            this._qryTiposElementosAplicacion = qryTiposElementosAplicacion;
        }

        /// <summary>
        /// Obtiene la lista de todas las aplicaciones disponibles en el sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet("lista")]
        [Authorize]
        public async Task<IActionResult> Lista()
        {
            List<AplicacionModel> aplicaciones = await _qryListaAplicaciones.Execute();

            return Ok(aplicaciones);
        }

        /// <summary>
        /// Obtiene los datos de una aplicacion en particular
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAplicacion(Guid id)
        {
            AplicacionModel app = await _qryAplicacion.Execute(id);
            if (app == null)
            {
                return NotFound();
            }
            return Ok(app);
        }

        /// <summary>
        /// Obtiene el arbol de elementos de una aplicacion 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("elementos/{id}")]
        [Authorize]
        public async Task<IActionResult> GetElementosAplicacion(Guid id)
        {
            var elementos = await _qryAplicacion.Execute(id);
            return Ok(elementos);
        }

        /// <summary>
        /// Obtiene los tipos de elementos de aplicacion definidos en el sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet("elementos/tipos")]
        [Authorize]
        public async Task<IActionResult> GetTiposElementosAplicacion()
        {
            var tipos = await _qryTiposElementosAplicacion.Execute();
            return Ok(tipos);
        }

        /// <summary>
        /// Obtiene los grupos de permisos definidos para una aplicacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("grupos/{id}")]
        [Authorize]
        public async Task<IActionResult> GetGruposAplicacion(Guid id)
        {
            var grupos = await _qryGruposAplicacion.Execute(id);
            return Ok(grupos);
        }
    }
}
