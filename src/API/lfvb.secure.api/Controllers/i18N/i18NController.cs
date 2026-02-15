using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
using lfvb.secure.aplication.Database.i18N.Composiciones.Querys;
using lfvb.secure.aplication.Database.i18N.Idiomas.Queries;
using lfvb.secure.aplication.Database.i18N.Textos.Models;
using lfvb.secure.aplication.Database.i18N.Textos.Queries;
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
    public class i18NController : ControllerBase
    {
        private ILogger<i18NController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllIdiomasQuery _qryGetAllIdiomas;
        private IGetIdiomaQuery _qryGetIdioma;
        private IGetIdIdiomaQuery _qryIdIdioma;

        private IGetAllColeccionesTexto _qryColeccionesTexto;
        private IGetColeccionTextoQuery _qryColeccionTexto;

        private IGetAllCamposColeccionTextoQuery _qryAllCamposColeccion;
        private IGetCampoColeccionTextoQuery _qryCampoColeccion;

        private IGetAllOpcionesCamposColeccionQuery _qryAllOpcionesCamposColeccion;
        private IGetOpcionCampoColeccionTextoQuery _qryOpcionCampoColeccion;

        private IGetAllTextos _qryGetAllTextos;
        private IBuscadorTextosQuery _qryBuscadorTextos;
        private IGetTextoQuery _qryGetTexto;

        public i18NController(ILogger<i18NController> logger,
                              IJwtTokenUtils jwtTokenUtils,
                              IGetAllIdiomasQuery qryGetAllIdiomas,
                              IGetIdiomaQuery qryGetIdioma,
                              IGetIdIdiomaQuery qryIdIdioma,
                              IGetAllColeccionesTexto qryColeccionesTexto,
                              IGetColeccionTextoQuery qryColeccionTexto,
                              IGetAllCamposColeccionTextoQuery qryAllCamposColeccion,
                              IGetCampoColeccionTextoQuery qryCampoColeccion,
                              IGetAllOpcionesCamposColeccionQuery qryAllOpcionesCamposColeccion,
                              IGetOpcionCampoColeccionTextoQuery qryOpcionCampoColeccion,
                              IGetAllTextos qryGetAllTextos,
                              IBuscadorTextosQuery qryBuscadorTextos,
                              IGetTextoQuery qryGetTexto
                              )
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryGetAllIdiomas = qryGetAllIdiomas;
            this._qryGetIdioma = qryGetIdioma;
            this._qryIdIdioma = qryIdIdioma;
            this._qryColeccionesTexto = qryColeccionesTexto;
            this._qryColeccionTexto = qryColeccionTexto;
            this._qryColeccionTexto = qryColeccionTexto;
            this._qryAllCamposColeccion = qryAllCamposColeccion;
            this._qryCampoColeccion = qryCampoColeccion;
            this._qryAllOpcionesCamposColeccion = qryAllOpcionesCamposColeccion;
            this._qryOpcionCampoColeccion = qryOpcionCampoColeccion;
            this._qryGetAllTextos = qryGetAllTextos;
            this._qryBuscadorTextos = qryBuscadorTextos;
            this._qryGetTexto = qryGetTexto;
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

        /// <summary>
        /// Obtiene el identificador de elemento del idioma seleccionado
        /// </summary>
        /// <param name="codigoIdioma"></param>
        /// <returns></returns>
        [HttpGet("idiomas/{codigoIdioma}/id")]
        public async Task<IActionResult> GetIdIdioma(string codigoIdioma)
        {
            try
            {
                Guid? ident = await this._qryIdIdioma.execute(codigoIdioma);
                return Ok(ident);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo el id del idioma");
                return StatusCode(500, "Error obteniendo el id del idioma");
            }
        }

        /// <summary>
        /// Obtiene todas las colecciones de textos del sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet("coleccion/lista")]
        [Authorize]
        public async Task<IActionResult> GetColeccionLista()
        {
            try
            {
                var lista = await _qryColeccionesTexto.execute();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo la coleccion de idiomas");
                return StatusCode(500, "Error obteniendo la coleccion de idiomas");
            }
        }

        /// <summary>
        /// Obtiene los datos simples de una coleccion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("coleccion/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetColeccionTexto(Guid id)
        {
            try
            {
                ColeccionTextoModel? coleccion = await this._qryColeccionTexto.execute(id);
                if (coleccion != null)
                {
                    return Ok(coleccion);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo la coleccion de idiomas");
                return StatusCode(500, "Error obteniendo la coleccion de idiomas");
            }
        }

        /// <summary>
        /// Obtiene los campos configurados para una coleccion de textos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("coleccion/{id:guid}/campos")]
        [Authorize]
        public async Task<IActionResult> GetColeccionTextoCampos(Guid id)
        {
            try
            {
                List<CampoColeccionTextoModel> campos = await this._qryAllCamposColeccion.execute(id);
                return Ok(campos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo los campos de la coleccion de idiomas");
                return StatusCode(500, "Error obteniendo los campos de la coleccion de idiomas");
            }
        }

        /// <summary>
        /// Obtiene los datos de un campo concreto de una coleccion de textos   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idCampo"></param>
        /// <returns></returns>
        [HttpGet("coleccion/{id:guid}/campo/{idCampo:guid}")]
        [Authorize]
        public async Task<IActionResult> GetColeccionTextoCampo(Guid id, Guid idCampo)
        {
            try
            {
                CampoColeccionTextoModel? campo = await this._qryCampoColeccion.execute(idCampo);
                if (campo != null)
                {
                    return Ok(campo);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo el campo de la coleccion de idiomas");
                return StatusCode(500, "Error obteniendo el campo de la coleccion de idiomas");
            }

        }

        /// <summary>
        /// Obtiene las opciones de un campo concreto de una coleccion de textos    
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idCampo"></param>
        /// <returns></returns>
        [HttpGet("coleccion/{id:guid}/campo/{idCampo:guid}/opciones")]
        [Authorize]
        public async Task<IActionResult> GetColeccionTextoCampoOpciones(Guid id, Guid idCampo)
        {
            try
            {
                List<OpcionCampoColeccionTextoModel> opciones = await this._qryAllOpcionesCamposColeccion.execute(idCampo);
                return Ok(opciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo las opciones del campo de la coleccion de idiomas");
                return StatusCode(500, "Error obteniendo las opciones del campo de la coleccion de idiomas");
            }

        }

        /// <summary>
        /// Obtiene los datos de una opcion concreta de un campo concreto de una coleccion de textos    
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idCampo"></param>
        /// <param name="idOpcion"></param>
        /// <returns></returns>
        [HttpGet("coleccion/{id:guid}/campo/{idCampo:guid}/opcion/{idOpcion:guid}")]
        [Authorize]
        public async Task<IActionResult> GetColeccionTextoCampoOpcion(Guid id, Guid idCampo, Guid idOpcion)
        {
            try
            {
                OpcionCampoColeccionTextoModel? opcion = await this._qryOpcionCampoColeccion.execute(idOpcion);
                if (opcion != null)
                {
                    return Ok(opcion);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo la opcion del campo de la coleccion de idiomas");
                return StatusCode(500, "Error obteniendo la opcion del campo de la coleccion de idiomas");
            }
        }

        /// <summary>
        /// Obtiene el listado completo de textos del sistema (sin paginar ni filtrar)  
        /// </summary>
        /// <returns></returns>
        [HttpGet("textos")]
        [Authorize]
        public async Task<IActionResult> GetAllTextos()
        {
            try
            {
                var textos = await this._qryGetAllTextos.execute();
                return Ok(textos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo los textos");
                return StatusCode(500, "Error obteniendo los textos");
            }
        }

        /// <summary>
        /// Busca los guid de los textos que coincidan con el texto de búsqueda indicado, el idioma y el tipo de coincidencia (exacta o parcial)
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpPost("textos/busqueda")]
        [Authorize]
        public async Task<IActionResult> BusquedaTextos([FromBody] BusquedaTextosModel busqueda)
        {
            try
            {
                var textos = await this._qryBuscadorTextos.execute(busqueda.Busqueda, busqueda.MatchExacto, busqueda.Idiomas);
                return Ok(textos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error buscando los textos");
                return StatusCode(500, "Error buscando los textos");
            }
        }

        /// <summary>
        /// Busca los elementos de texto que coincidan con el texto de búsqueda indicado, el idioma y el tipo de coincidencia (exacta o parcial)
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpPost("textos/busqueda/textos")]
        [Authorize]
        public async Task<IActionResult> BusquedaTextosElementos([FromBody] BusquedaTextosModel busqueda)
        {
            try
            {
                var textos = await this._qryBuscadorTextos.executeModel(busqueda.Busqueda, busqueda.MatchExacto, busqueda.Idiomas);
                return Ok(textos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error buscando los textos");
                return StatusCode(500, "Error buscando los textos");
            }
        }

        /// <summary>
        /// Obtine los datos de un texto concreto a partir de su identificador, incluyendo sus variables y sus textos en los diferentes idiomas configurados    
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("textos/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetTexto(Guid id)
        {
            try
            {
                var texto = await this._qryGetTexto.execute(id);
                if (texto != null)
                {
                    return Ok(texto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo el texto");
                return StatusCode(500, "Error obteniendo el texto");
            }
        }
    }
}
