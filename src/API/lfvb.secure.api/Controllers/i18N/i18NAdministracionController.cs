using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.i18N.Idiomas.Commands;
using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using lfvb.secure.aplication.Database.i18N.Idiomas.Queries;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using System.Runtime.CompilerServices;


namespace lfvb.secure.api.Controllers.i18N
{
    /// <summary>
    /// Api de aministracion de i18N (idiomas, textos, composiciones, etc)  
    ///<code>fsadfasd</code>
    /// </summary>
    [ApiController]
    [Route("api/i18n/administracion")]
    public class i18NAdministracionController:ControllerBase
    {
        private ILogger<i18NAdministracionController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllIdiomasQuery _qryGetIdiomas;
        private IAltaIdiomaCommand _cmdAltaIdioma;
        private IModificarIdiomaCommand _cmdModificarIdioma;
        
        
        public i18NAdministracionController(ILogger<i18NAdministracionController> logger,
                              IJwtTokenUtils jwtTokenUtils,
                              IGetAllIdiomasQuery qryGetIdiomas,
                              IAltaIdiomaCommand cmdAltaIdioma,
                              IModificarIdiomaCommand cmdModificarIdioma
                              )
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            _qryGetIdiomas = qryGetIdiomas;
            _cmdAltaIdioma = cmdAltaIdioma;
            _cmdModificarIdioma = cmdModificarIdioma;
        }


        /// <summary>
        /// Obtiene los idiomas simples y compuestos dados de alta en la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("idiomas/todos")]
        [DbAuthorize("ADM_IDIOMAS","SW_LST_ALL_IDIOMA", "LLSWEP")]
        public async Task<IActionResult> GetTodosIdiomas()
        {
            try
            {
                List<IdiomaModel> idiomas =await _qryGetIdiomas.execute(true);
                return Ok(idiomas);
            } catch (Exception err)
            {
                this._logger.LogError("No se ha podido obtener el listado de todos los idiomas", err);
                return BadRequest("No se ha podido obtener todos los idiomas");
            }
        }

        /// <summary>
        /// Método para dar de alta un idioma en concreto
        /// </summary>
        /// <param name="idio"></param>
        /// <remarks>Permiso ADM_IDIOMAS, SW_ALTA_IDIOMA, LLSWEP</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("idiomas/alta")]
        [DbAuthorize("ADM_IDIOMAS", "SW_ALTA_IDIOMA", "LLSWEP")]
        public async Task<IActionResult> AltaIdioma(IdiomaModel idio)
        {
            try
            {
                IdiomaModel retorno = await _cmdAltaIdioma.execute(idio);
                return Ok(retorno);
            } catch (Exception err)
            {
                this._logger.LogError("No se ha podido dar de alta el idioma", err);
                return BadRequest("No se ha podido dar de alta el idioma");
            }
        }


        /// <summary>
        /// Método para modificar idioma en concreto
        /// </summary>
        /// <param name="idio"></param>
        /// <remarks>Permiso ADM_IDIOMAS, SW_ALTA_IDIOMA, LLSWEP</remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("idiomas/modificacion")]
        [DbAuthorize("ADM_IDIOMAS", "SW_MODIFICAR_IDIOMA", "LLSWEP")]
        public async Task<IActionResult> ModificaIdioma(IdiomaModel idio)
        {
            try
            {
                IdiomaModel retorno = await _cmdModificarIdioma.execute(idio);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido modificar el idioma", err);
                return BadRequest("No se ha podido modificar el idioma");
            }
        }
    }
}
