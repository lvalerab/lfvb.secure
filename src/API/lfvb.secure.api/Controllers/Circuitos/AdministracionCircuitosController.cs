using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetAllTramites;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Queries.GetTramite;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers.Circuitos
{
    /// <summary>
    /// Administracion de los circuitos de tramitacion disponibles en la aplicacion para cualquier elementos configurable. El grupo, tiene que tener permisos en MOD_ADM_CIRC
    /// </summary>
    [ApiController]
    [Route("api/modulos/circuitos/administracion")]
    public class AdministracionCircuitosController:ControllerBase
    {
        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllTramitesQuery _qryAllTramites;
        private IGetTramiteQuery _qryTramite;   

        public AdministracionCircuitosController(ILogger<PermisosController> logger, IJwtTokenUtils jwtTokenUtils, IGetAllTramitesQuery qryAllTram, IGetTramiteQuery qryTramite)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _qryAllTramites = qryAllTram;   
            _qryTramite = qryTramite;   
        }

        /// <summary>
        /// obtiene el listado de tramites disponibles en la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpGet("tramites")]
        [Authorize]        
        public async Task<IActionResult> GetTramites()
        {
            try
            {
                List<TramiteModel> lista = await this._qryAllTramites.Execute();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// obtiene el listado de tramites disponibles en la aplicación (Paginacion)
        /// </summary>
        /// <returns></returns>
        [HttpGet("tramites/{page:int}/{items:int}")]
        [Authorize]
        public async Task<IActionResult> GetTramites(int page, int items)
        {
            try
            {
                List<TramiteModel> lista = await this._qryAllTramites.Execute(page,items);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un tramite preciso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("tramite/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetTramite(Guid id)
        {
            try
            {
                TramiteModel? tramite = await this._qryTramite.Execute(id);
                if (tramite == null)
                    return NotFound();
                return Ok(tramite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
