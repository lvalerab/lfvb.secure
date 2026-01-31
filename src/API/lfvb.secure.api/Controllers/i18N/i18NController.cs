using lfvb.secure.aplication.Database.i18N.Idiomas.Queries;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers.i18N
{
    /// <summary>
    /// Controlador de idiomas y textos
    /// </summary>
    [ApiController]
    [Route("api/i18n")]
    public class i18NController:ControllerBase
    {
        private ILogger<i18NController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        
        private IGetAllIdiomasQuery _qryGetAllIdiomas;
        private IGetIdiomaQuery _qryGetIdioma;

        public i18NController(ILogger<i18NController> logger,
                              IJwtTokenUtils jwtTokenUtils,
                              IGetAllIdiomasQuery qryGetAllIdiomas,
                              IGetIdiomaQuery qryGetIdioma
                              )
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryGetAllIdiomas = qryGetAllIdiomas;
            this._qryGetIdioma = qryGetIdioma;
        }

        /// <summary>
        /// Obtiene todos los idiomas del sistema (solo los simples)    
        /// </summary>
        /// <remarks>Api publica</remarks>
        /// <returns></returns>
        [HttpGet("idiomas")]
        public async Task<IActionResult> GetAllIdiomas()
        {
            try
            {
                var idiomas = await _qryGetAllIdiomas.execute(false);
                return Ok(idiomas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo los idiomas");
                return StatusCode(500, "Error obteniendo los idiomas");
            }
        }

        /// <summary>
        /// Obtiene los datos de un idioma en concreto
        /// </summary>
        /// <remarks>Api publica</remarks>
        /// <param name="codigoIdioma"></param>
        /// <returns></returns>
        [HttpGet("idiomas/{codigoIdioma}")]
        public async Task<IActionResult> GetIdioma(string codigoIdioma)
        {
            try
            {
                var idioma = await _qryGetIdioma.execute(codigoIdioma);
                return Ok(idioma);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo el idioma");
                return StatusCode(500, "Error obteniendo el idioma");
            }
        }

    }
}
