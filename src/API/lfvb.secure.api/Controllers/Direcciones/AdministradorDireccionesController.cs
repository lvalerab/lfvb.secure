using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Database.Direcciones.Commands.TipoVia;
using lfvb.secure.aplication.Database.Direcciones.Commands.TipoEntidadTerritorial;
using lfvb.secure.aplication.Database.Direcciones.Commands.TipoCodigoEntidadTerritorial;
using lfvb.secure.aplication.Database.Direcciones.Commands.EntidadTerritorial;
using lfvb.secure.aplication.Database.Direcciones.Commands.Callejero;

namespace lfvb.secure.api.Controllers.Direcciones
{
    /// <summary>
    /// End point para la gestión de direcciones.
    /// </summary>
    [ApiController]
    [Route("api/administracion/direcciones")]
    public class AdministradorDireccionesController : ControllerBase
    {
        private ILogger<AdministradorDireccionesController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IAltaTipoViaCommand _altaTipoViaCommand;
        private IAltaTipoEntidadTerritorialCommand _altaTipoEntidadTerritorialCommand;
        private IAltaTipoCodigoEntidadTerritorialCommand _altaTipoCodigoEntidadTerritorialCommand;

        private IAltaEntidadTerritorialCommand _altaEntidadTerritorialCommand;
        private IModificarEntidadTerritorialCommand _modificarEntidadTerritorialCommand;
        private IEliminarEntidadTerritorialCommand _eliminarEntidadTerritorialCommand;

        private IAltaViaCommand _altaViaCommand;
        private IModificarViaCommand _modificarViaCommand;
        private IEliminarViaCommand _eliminarViaCommand;



        public AdministradorDireccionesController(ILogger<AdministradorDireccionesController> logger,
                                                IJwtTokenUtils jwtTokenUtils,
                                                IAltaTipoViaCommand altaTipoViaCommand,
                                                IAltaTipoEntidadTerritorialCommand altaTipoEntidadTerritorialCommand,
                                                IAltaTipoCodigoEntidadTerritorialCommand altaTipoCodigoEntidadTerritorialCommand,
                                                IAltaEntidadTerritorialCommand altaEntidadTerritorialCommand,
                                                IModificarEntidadTerritorialCommand modificarEntidadTerritorialCommand,
                                                IEliminarEntidadTerritorialCommand eliminarEntidadTerritorialCommand,
                                                IAltaViaCommand altaViaCommand,
                                                IModificarViaCommand modificarViaCommand,
                                                IEliminarViaCommand eliminarViaCommand
                                                )
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            _altaTipoViaCommand= altaTipoViaCommand;
            _altaTipoEntidadTerritorialCommand = altaTipoEntidadTerritorialCommand;
            _altaTipoCodigoEntidadTerritorialCommand = altaTipoCodigoEntidadTerritorialCommand;
            _altaEntidadTerritorialCommand = altaEntidadTerritorialCommand;
            _modificarEntidadTerritorialCommand = modificarEntidadTerritorialCommand;
            _eliminarEntidadTerritorialCommand = eliminarEntidadTerritorialCommand;
            _altaViaCommand = altaViaCommand;
            _modificarViaCommand = modificarViaCommand;
            _eliminarViaCommand = eliminarViaCommand;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok("OK");
        }

        /// <summary>
        /// Servicio para dar de alta/modificar los tipos de via
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        [HttpPost]
        [Route("tipo/via")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ADM_CALL_AM_TIPO_VIA:LLSWEP"])]
        public async Task<IActionResult> AltaModificacionTipoVia([FromBody] TipoViaModel model)
        {
            try
            {
                TipoViaModel resultado=await this._altaTipoViaCommand.execute(model);
                return Ok(resultado);   
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error en el alta de tipo de via", ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Servicio para dar de alta/modificar los tipos de entidad
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        [HttpPost]
        [Route("tipo/entidad")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ADM_CALL_AM_TIPO_ENTI:LLSWEP"])]
        public async Task<IActionResult> AltaModificacionTipoEntidad([FromBody] TipoEntidadTerritorialModel model)
        {
            try
            {
                TipoEntidadTerritorialModel resultado = await this._altaTipoEntidadTerritorialCommand.execute(model);
                return Ok(resultado);  
            }
            catch (Exception ex)
            {
                _logger.LogError("Al dar de alta el tipo de entidad", ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Servicio para dar de alta/modificar los tipos de codigos de entidad
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        [HttpPost]
        [Route("tipo/codigo/entidad")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ADM_CALL_AM_TICO_ENTI:LLSWEP"])]
        public async Task<IActionResult> AltaModificacionTipoCodigoTipoEntidad([FromBody] TipoCodigoEntidadTerritorialModel model)
        {
            try
            {
                TipoCodigoEntidadTerritorialModel resultado = await this._altaTipoCodigoEntidadTerritorialCommand.execute(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Al dar de alta el tipo de codigo de tipo de entidad",ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Servicio web para dar de alta una entidad territorial
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("entidad/territorial")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ENT_TER_ALTA:LLSWEP"])]
        public async Task<IActionResult> AltaEntidadTerritorial([FromBody] EntidadTerritorialModel model)
        {
            try
            {
                EntidadTerritorialModel result = await _altaEntidadTerritorialCommand.execute(model);
                return Ok(result);
            }  catch (Exception err)
            {
                this._logger.LogError("Error en el alta de entidad territorial", err);
                return BadRequest(err);
            }
        }

        /// <summary>
        /// Servicio web para modificar una entidad territorial
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("entidad/territorial")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ENT_TER_MODIFICA:LLSWEP"])]
        public async Task<IActionResult> ModificarEntidadTerritorial([FromBody] EntidadTerritorialModel model)
        {
            try
            {
                EntidadTerritorialModel result = await _modificarEntidadTerritorialCommand.execute(model);
                return Ok(result);
            }
            catch (Exception err)
            {
                this._logger.LogError("Error en modificación de entidad territorial", err);
                return BadRequest(err);
            }
        }

        /// <summary>
        /// Servicio web para eliminar una entidad territorial
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("entidad/territorial/{id:guid}")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ENT_TER_ELIMINAR:LLSWEP"])]
        public async Task<IActionResult> EliminarEntidadTerritorial(Guid id)
        {
            try
            {
                bool puede = await _eliminarEntidadTerritorialCommand.execute(id);
                return Ok(puede);
            } catch (Exception err)
            {
                this._logger.LogError("Error en la eliminación de entidad territorial", err);
                return BadRequest(err);
            }
        }

        /// <summary>
        /// Servicio web para dar de alta una via
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("via")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ALTA_VIA:LLSWEP"])]
        public async Task<IActionResult> AltaVia([FromBody] CallejeroModel model)
        {
            try
            {
                CallejeroModel resultado = await this._altaViaCommand.execute(model);
                return Ok(resultado);
            } catch (Exception err)
            {
                this._logger.LogError("Error en el alta de la via", err);
                return BadRequest(err);
            }
        }

        /// <summary>
        /// Servicio web para modificar una via
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("via")]
        [DbAuthorize(["ADM_CALLEJERO:SW_MODIFICA_VIA:LLSWEP"])]
        public async Task<IActionResult> ModfiicaVia([FromBody] CallejeroModel model)
        {
            try
            {
                CallejeroModel resultado = await this._modificarViaCommand.execute(model);
                return Ok(resultado);
            }
            catch (Exception err)
            {
                this._logger.LogError("Error al modificar de la via", err);
                return BadRequest(err);
            }
        }

        /// <summary>
        /// Servicio web para eliminar una via
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("via/{id:guid}")]
        [DbAuthorize(["ADM_CALLEJERO:SW_ELIMINA_VIA:LLSWEP"])]
        public async Task<IActionResult> EliminarVia(Guid id)
        {
            try
            {
                bool eliminado = await _eliminarViaCommand.execute(id);
                return Ok(eliminado);
            } catch (Exception err)
            {
                this._logger.LogError("Error al eliminar la via", err);
                return BadRequest(err);
            }
        }

    }
}
