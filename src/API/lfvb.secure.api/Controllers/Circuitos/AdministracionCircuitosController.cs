using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.api.ParametrosModel;
using lfvb.secure.aplication.Database.Circuitos.Acciones.Models;
using lfvb.secure.aplication.Database.Circuitos.Acciones.Queries;
using lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Models;
using lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Queries;
using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Models;
using lfvb.secure.aplication.Database.Circuitos.BandejaTramites.Queries;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries.Pasos;
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
        private IGetCircuitoQuery _cmdGetCircuitoQuery;
        private IAltaCircuitoCommand _cmdAltaCircuitoCommand;
        private IModificacionCircuitoCommand _cmdModificarCircuitoCommand; 

        private IGetPasosCircuitoQuery _qryPasosCircuito;
        private IGetPasoCircuitoQuery _qryPasoCircuito;

        private IAltaPasoCircuitoCommand _cmdAltaPasoCircuitoCommand;
        private IModificarPasoCircuitoCommand _cmdModificarPasoCircuitoCommand;
        private IEliminarPasoCircuitoCommand _cmdEliminarPasoCircuitoCommand;

        private IAltaPasoSiguienteCircuitoCommand _cmdAltaPasoSiguienteCommando;
        private IEliminarPasosSiguienteCircuitoCommand _cmdEliminarPasosSiguientesCommando;

        private IGetAllAccionesQuery _qryAllAcciones; 
        
        private IGetAccionesPasoQuery _qryAccionesPaso;


        private IListaBandejasSistemaQuery _qryListaBandejas;

        /// <summary>
        /// Controlador para aministrar los circuitos de tramitacion, necesita permisos en ADM_GEST_CIRCUITOS
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jwtTokenUtils"></param>
        /// <param name="qryAllTram"></param>
        /// <param name="qryTramite"></param>
        /// <param name="cmdAltaTramite"></param>
        /// <param name="cmdModificarTramite"></param>
        /// <param name="cmdGetCircuitosQuery"></param>
        /// <param name="cmdAltaCircuitoCommand"></param>
        /// <param name="qryPasosCircuito"></param>
        /// <param name="qryPasoCircuito"></param>
        /// <param name="cmdAltaPasoCircuitoCommand"></param>
        /// <param name="cmdModificarPasoCircuitoCommand"></param>
        /// <param name="cmdEliminarPasoCircuitoCommand"></param>
        public AdministracionCircuitosController(ILogger<PermisosController> logger,
                                                 IJwtTokenUtils jwtTokenUtils,
                                                 IGetAllTramitesQuery qryAllTram,
                                                 IGetTramiteQuery qryTramite,
                                                 IAltaTramiteCommand cmdAltaTramite,
                                                 IModificarTramiteCommand cmdModificarTramite,
                                                 IGetCircuitosQuery cmdGetCircuitosQuery,
                                                 IGetCircuitoQuery cmdGetCircuitoQuery,
                                                 IAltaCircuitoCommand cmdAltaCircuitoCommand,
                                                 IGetPasosCircuitoQuery qryPasosCircuito,
                                                 IGetPasoCircuitoQuery qryPasoCircuito,
                                                 IAltaPasoCircuitoCommand cmdAltaPasoCircuitoCommand,
                                                 IModificacionCircuitoCommand  cmdModificarCircuitoCommand,
                                                 IModificarPasoCircuitoCommand cmdModificarPasoCircuitoCommand,  
                                                 IEliminarPasoCircuitoCommand cmdEliminarPasoCircuitoCommand,
                                                 IAltaPasoSiguienteCircuitoCommand cmdAltaPasoSiguienteCommando,
                                                 IEliminarPasosSiguienteCircuitoCommand cmdEliminarPasosSiguientesCommando,
                                                 IGetAllAccionesQuery qryAllAcciones,
                                                 IGetAccionesPasoQuery qryAccionesPaso,
                                                 IListaBandejasSistemaQuery qryListaBandejas
            )
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _qryAllTramites = qryAllTram;
            _qryTramite = qryTramite;
            _cmdAltaTramite = cmdAltaTramite;
            _cmdModificarTramite = cmdModificarTramite;
            _cmdGetCircuitosQuery = cmdGetCircuitosQuery;
            _cmdGetCircuitoQuery = cmdGetCircuitoQuery; 
            _cmdAltaCircuitoCommand = cmdAltaCircuitoCommand;
            _cmdModificarCircuitoCommand = cmdModificarCircuitoCommand;
            _qryPasoCircuito = qryPasoCircuito;
            _qryPasosCircuito= qryPasosCircuito;
            _qryPasoCircuito = qryPasoCircuito;
            _cmdAltaPasoCircuitoCommand = cmdAltaPasoCircuitoCommand;
            _cmdModificarPasoCircuitoCommand = cmdModificarPasoCircuitoCommand;
            _cmdEliminarPasoCircuitoCommand = cmdEliminarPasoCircuitoCommand;
            _cmdAltaPasoSiguienteCommando = cmdAltaPasoSiguienteCommando;
            _cmdEliminarPasosSiguientesCommando = cmdEliminarPasosSiguientesCommando;
            _qryAllAcciones = qryAllAcciones;
            _qryAccionesPaso = qryAccionesPaso;
            _qryListaBandejas = qryListaBandejas;
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
                if (modelo.Nombre != "" && modelo.Descripcion != "" && modelo.Normativa != "")
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
                if (modelo.Id != Guid.Empty && modelo.Nombre != "" && modelo.Descripcion != "" && modelo.Normativa != "")
                {
                    TramiteModel modificado = await _cmdModificarTramite.execute(modelo);
                    if (modificado == null)
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
        /// Obtiene el listado de circuitos asociados a un tramite
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("tramite/{id:guid}/circuitos/listado")]
        [Authorize]
        public async Task<IActionResult> ListaCircuitosTramite(Guid id)
        {
            try
            {
                FiltroCircuitoModel filtro = new FiltroCircuitoModel()
                {
                    IdTramite = id
                };
                List<CircuitoModel> lista = await _cmdGetCircuitosQuery.execute(filtro);
                return Ok(lista);
            }
            catch (Exception err)
            {
                _logger.LogError(err, err.Message);
                return BadRequest(err.Message);
            }
        }

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
                List<CircuitoModel> lista = await _cmdGetCircuitosQuery.execute(filtro);
                return Ok(lista);
            } catch (Exception err)
            {
                _logger.LogError(err, err.Message);
                return BadRequest(err.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de un circuito dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("circuito/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetCircuito(Guid id)
        {
            try
            {
                CircuitoModel? circuito = await _cmdGetCircuitoQuery.execute(id);
                if (circuito == null)
                    return NotFound();
                return Ok(circuito);
            }
            catch (Exception err)
            {
                _logger.LogError(err, err.Message);
                return BadRequest(err.Message);
            }
        }

        /// <summary>
        /// Método para dar de alta un circuito nuevo, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_CIRC_ALTA, LLSWEP]
        /// </summary>
        [HttpPost("circuitos/alta")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_CIRC_ALTA", "LLSWEP")]
        public async Task<IActionResult> AltaCircuito(AltaCircuitoModel modelo)
        {
            try
            {
                return Ok(await _cmdAltaCircuitoCommand.Execute(modelo));
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metedo para modificar un circuito existente, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_CIRC_MOD, LLSWEP] 
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPut("circuitos/modificacion")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_CIRC_MOD", "LLSWEP")]
        public async Task<IActionResult> ModificacionCircuito(CircuitoModel modelo)
        {
            try
            {
                CircuitoModel? modificado = await _cmdModificarCircuitoCommand.execute(modelo);
                if (modificado == null)
                    return NotFound();
                return Ok(modificado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los pasos de un circuito determinado
        /// </summary>
        /// <param name="circuitoId"></param>
        /// <returns></returns>
        [HttpGet("circuitos/{circuitoId:guid}/pasos")]
        [Authorize]
        public async Task<IActionResult> GetPasosCircuito(Guid circuitoId)
        {
            try
            {
                List<PasoModel> pasos = await _qryPasosCircuito.execute(circuitoId);
                return Ok(pasos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de un paso en concreto
        ///</summary>
        [HttpGet("circuitos/paso/{pasoId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetPasoCircuito(Guid pasoId)
        {
            try
            {
                PasoModel? paso = await _qryPasoCircuito.execute(pasoId);
                if (paso == null)
                    return NotFound();
                return Ok(paso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Para dar de alta un nuevo paso en un circuito, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_PSCR_ALTA, LLSWEP]  
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("circuitos/paso/alta")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_PSCR_ALTA", "LLSWEP")]
        public async Task<IActionResult> AltaPasoCircuito(PasoModel modelo)
        {
            try
            {
                PasoModel nuevoPaso = await _cmdAltaPasoCircuitoCommand.execute(modelo);
                return Ok(nuevoPaso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Para modificar un paso existente en un circuito, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_PSCR_MOD, LLSWEP] 
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPut("circuitos/paso/modificacion")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_PSCR_MOD", "LLSWEP")]
        public async Task<IActionResult> ModificarPasoCircuito(PasoModel modelo)
        {
            try
            {
                PasoModel modificado = await _cmdModificarPasoCircuitoCommand.execute(modelo);
                if (modificado == null)
                {
                    _logger.LogCritical("No se ha encontrado el paso a modificar.");
                    return NotFound("No se ha encontrado el paso a modificar.");
                }
                else
                {
                    return Ok(modificado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un paso de un circuito, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_PSCR_ELIM, LLSWEP] 
        /// </summary>
        /// <param name="pasoId"></param>
        /// <param name="interconectar"></param>
        /// <returns></returns>
        [HttpDelete("circuitos/paso/{pasoId:guid}")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_PSCR_ELIM", "LLSWEP")]
        public async Task<IActionResult> EliminarPasoCircuito(Guid pasoId, [FromQuery] bool interconectar = false)
        {
            try
            {
                bool resultado = await _cmdEliminarPasoCircuitoCommand.execute(pasoId, interconectar);
                if (!resultado)
                {
                    _logger.LogCritical("No se ha podido eliminar el paso, puede que tenga elementos asociados.");
                    return BadRequest("No se ha podido eliminar el paso, puede que tenga elementos asociados.");
                }
                else
                {
                    return Ok(resultado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Da de alta los pasos siguientes a un paso concreto, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_PSCR_RELA, LLSWEP]
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("circuitos/paso/siguientes/alta")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_PSCR_RELA", "LLSWEP")]
        public async Task<IActionResult> AltaPasoSiguienteCircuito(ParametrosRelacionPasoCircuito modelo)
        {
            try
            {
                List<Guid> resultado = await _cmdAltaPasoSiguienteCommando.execute(modelo.Id, modelo.Ids);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina los pasos siguientes a un paso concreto, requiere permisos en [ADM_GEST_CIRCUITOS, SW_MOD_ADM_PSCR_RELA, LLSWEP]    
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("circuitos/paso/siguientes/elimina")]
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_MOD_ADM_PSCR_RELA", "LLSWEP")]
        public async Task<IActionResult> EliminaPasosSiguientesCircuito(ParametrosRelacionPasoCircuito modelo)
        {
            try
            {
                List<Guid> resultado = await _cmdEliminarPasosSiguientesCommando.execute(modelo.Id, modelo.Ids);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region "Relativos a las acciones disponibles en los pasos de los circuitos"
        /// <summary>
        /// Obtiene el listado de acciones que se pueden ejecutar en un paso dado
        /// </summary>
        /// <returns></returns>
        [HttpPost("acciones/listado")]
        [Authorize]
        public async Task<IActionResult> GetAllAcciones()
        {
            try
            {
                List<AccionModel> lista = await this._qryAllAcciones.execute();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de acciones asociadas a un paso en concreto, se necesita el permiso de ADM_GEST_CIRCUITOS, SW_LST_PASO_ACCIONES, LLSWEP  
        /// </summary>
        /// <param name="pasoId"></param>
        /// <returns></returns>
        [HttpGet("circuitos/paso/{pasoId:guid}/acciones/{tipo}")]  
        [Authorize]
        [DbAuthorize("ADM_GEST_CIRCUITOS", "SW_LST_PASO_ACCIONES", "LLSWEP")]
        public async Task<IActionResult> GetAccionesPaso(Guid pasoId, string tipo="")
        {
            try
            {
                List<AccionPasoModel> lista = await this._qryAccionesPaso.execute(pasoId);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region "Relativos a las bandejas de tramites del sistema"
        /// <summary>
        /// Obtiene el listado de bandejas de tramites del sistema  
        /// </summary>
        /// <returns></returns>
        [HttpGet("bandejas")]
        [Authorize]
        public async Task<IActionResult> GetBandejasSistema()
        {
            try
            {
                List<BandejaTramiteModel> lista = await this._qryListaBandejas.execute();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
