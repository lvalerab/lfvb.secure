using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Commads.Tipos;

namespace lfvb.secure.api.Controllers.UnidadesOrganizativas
{
    /// <summary>
    /// Controlador para la gestion de unidades organizativas.
    /// </summary>
    [ApiController]
    [Route("api/modulos/unidad/organizativa/administracion")]
    public class AdministracionUnidadesOrganizativasController : ControllerBase
    {
        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IAltaTipoUnidadOrganizativaCommand _altaTipoUnidadOrganizativaCommand;
        private IModificarTipoUnidadOrganizativaCommand _modificacionTipoUnidadOrganizativaCommand;

        public AdministracionUnidadesOrganizativasController(ILogger<PermisosController> logger,
                                                             IJwtTokenUtils jwtTokenUtils,
                                                             IAltaTipoUnidadOrganizativaCommand altaTipoUnidadOrganizativaCommand,
                                                             IModificarTipoUnidadOrganizativaCommand modificacionTipoUnidadOrganizativaCommand)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _altaTipoUnidadOrganizativaCommand = altaTipoUnidadOrganizativaCommand;
            _modificacionTipoUnidadOrganizativaCommand = modificacionTipoUnidadOrganizativaCommand;
        }

        /// <summary>
        /// Metodo para dar de alta un nuevo tipo de unidad organizativa.  Permiso ("MOD_ADM_UNOR", "SW_ALTA_TIPO_UNOR", "LLSWEB") 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpPost("tipo/unidad")]
        [DbAuthorize("MOD_ADM_UNOR", "SW_ALTA_TIPO_UNOR", "LLSWEB")]
        public async Task<IActionResult> AltaTipoUnidadOrganizativa([FromBody] TipoUnidadOrganizativaModel tipo)
        {
            try
            {
                TipoUnidadOrganizativaModel resultado = await _altaTipoUnidadOrganizativaCommand.execute(tipo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al dar de alta el tipo de unidad organizativa: {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Modifica un tipo de unidad organizativa existente. Permiso ("MOD_ADM_UNOR", "SW_MODI_TIPO_UNOR", "LLSWEB")  
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpPut("tipo/unidad")]
        [DbAuthorize("MOD_ADM_UNOR", "SW_MODI_TIPO_UNOR", "LLSWEB")]
        public async Task<IActionResult> ModificacionTipoUnidadOrganizativa([FromBody] TipoUnidadOrganizativaModel tipo)
        {
            try
            {
                TipoUnidadOrganizativaModel resultado = await _modificacionTipoUnidadOrganizativaCommand.execute(tipo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al modificar el tipo de unidad organizativa: {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para dar de alta una nueva unidad organizativa. Permiso ("MOD_ADM_UNOR", "SW_ALTA_UNOR", "LLSWEB")
        /// </summary>
        /// <param name="unidad"></param>
        /// <returns></returns>
        [HttpPost("unidad")]
        [DbAuthorize("MOD_ADM_UNOR", "SW_ALTA_UNOR", "LLSWEB")]
        public IActionResult AltaUnidadOrganizativa([FromBody] UnidadOrganizativaModel unidad)
        {
            return Ok();
        }

        /// <summary>
        /// Modificacion de una unidad organizativa existente. Permiso ("MOD_ADM_UNOR", "SW_MODI_UNOR", "LLSWEB")
        /// </summary>
        /// <param name="unidad"></param>
        /// <returns></returns>
        [HttpPut("unidad")]
        [DbAuthorize("MOD_ADM_UNOR", "SW_MODI_UNOR", "LLSWEB")]
        public IActionResult ModificacionUnidadOrganizativa([FromBody] UnidadOrganizativaModel unidad)
        {
            return Ok();
        }

        /// <summary>
        /// Asocia un elemento a una unidad organizativa
        /// </summary>
        /// <remarks>permiso: ("MOD_ADM_UNOR","SW_ASOC_ELEM_UNOR", "LLSWEB")</remarks>
        /// <param name="IdElemento">The unique identifier of the element to associate.</param>
        /// <param name="CodUnor">The unique identifier of the organizational unit with which to associate the element.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the association operation.</returns>
        [HttpGet("unidad/{CodUnor:guid}/elemento/{IdElemento:guid}")]
        [DbAuthorize("MOD_ADM_UNOR","SW_ASOC_ELEM_UNOR", "LLSWEB")]
        public IActionResult RelacionarElementoConUnor(Guid IdElemento, Guid CodUnor)
        {
            return Ok();
        }
    }
}
