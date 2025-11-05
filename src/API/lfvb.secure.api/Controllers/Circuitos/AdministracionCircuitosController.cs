using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Commands;
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
    public class AdministracionCircuitosController : ControllerBase
    {
        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllTramitesQuery _qryAllTramites;
        private IGetTramiteQuery _qryTramite;
        private IAltaTramiteCommand _cmdAltaTramite;
        private IModificarTramiteCommand _cmdModificarTramite;

        private IGetCircuitosQuery _cmdGetCircuitosQuery;

        public AdministracionCircuitosController(ILogger<PermisosController> logger, 
                                                 IJwtTokenUtils jwtTokenUtils, 
                                                 IGetAllTramitesQuery qryAllTram, 
                                                 IGetTramiteQuery qryTramite, 
                                                 IAltaTramiteCommand cmdAltaTramite, 
                                                 IModificarTramiteCommand cmdModificarTramite,
                                                 IGetCircuitosQuery cmdGetCircuitosQuery)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _qryAllTramites = qryAllTram;
            _qryTramite = qryTramite;
            _cmdAltaTramite = cmdAltaTramite;   
            _cmdModificarTramite = cmdModificarTramite;
            _cmdGetCircuitosQuery = cmdGetCircuitosQuery;
        }

        #region "Relativos a los tramites de la aplicacion"

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
                List<TramiteModel> lista = await this._qryAllTramites.Execute(page, items);
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

        /// <summary>
        /// Da de alta un tramite nuevo, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_NORM_ALTA, LLSWEP]
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("tramite")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_NORM_ALTA", "LLSWEP")]
        public async Task<IActionResult> AltaTramite(AltaTramiteModel modelo)
        {
            try
            {
                if(modelo.Nombre!="" && modelo.Descripcion!="" && modelo.Normativa!="")
                {
                    TramiteModel nuevoTramite = await _cmdAltaTramite.execute(modelo);
                    return Ok(nuevoTramite);
                }
                else
                {
                    _logger.LogCritical("El modelo de alta de tramite no es correcto.");
                    return BadRequest("El modelo de alta de tramite no es correcto.");
                }   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///  Modifica un tramite existente, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_NORM_MOD, LLSWEP]
        /// </summary>
        [HttpPut("tramite")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_NORM_MOD", "LLSWEP")]
        public async Task<IActionResult> ModificaTramite(TramiteModel modelo)
        {
            try
            {
                if (modelo.Id!=Guid.Empty && modelo.Nombre != "" && modelo.Descripcion != "" && modelo.Normativa != "")
                {
                    TramiteModel modificado= await _cmdModificarTramite.execute(modelo);
                    if(modificado==null)
                    {
                        _logger.LogCritical("No se ha encontrado el tramite a modificar.");
                        return NotFound("No se ha encontrado el tramite a modificar.");
                    } else
                    {
                        return Ok(modificado);
                    }   
                }
                else
                {
                    _logger.LogCritical("El modelo de alta de tramite no es correcto.");
                    return BadRequest("El modelo de alta de tramite no es correcto.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region "Relativos a obtener los datos de los circuitos"

        /// <summary>
        /// Obtiene el listado de circuitos con filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpPost("circuitos/listado")]
        [Authorize]
        public async Task<IActionResult> ListaCircuitos(FiltroCircuitoModel filtro)
        {
            try
            {
                List<CircuitoModel> lista =await _cmdGetCircuitosQuery.execute(filtro);
                return Ok(lista);
            } catch (Exception err)
            {
                _logger.LogError(err, err.Message);
                return BadRequest(err.Message);
            }
        }

        #endregion
    }
}
