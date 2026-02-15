using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.i18N.Composiciones.Commands;
using lfvb.secure.aplication.Database.i18N.Composiciones.Models;
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
    public class i18NAdministracionController : ControllerBase
    {
        private ILogger<i18NAdministracionController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllIdiomasQuery _qryGetIdiomas;
        private IAltaIdiomaCommand _cmdAltaIdioma;
        private IModificarIdiomaCommand _cmdModificarIdioma;

        private IAltaColeccionTextoCommand _cmdAltaColeccionTexto;
        private IModificaColeccionTexto _cmdModificaColeccionTexto;
        private IEliminarColeccionTextoCommand _cmdEliminarColeccionTexto;

        private IAltaCampoColeccionTextoCommand _cmdAltaCampoColeccionTexto;
        private IModificarCampoColeccionTextoCommand _cmdModificarCampoColeccionTexto;
        private IEliminarCamposColeccionTextoCommand _cmdEliminarCamposColeccionTexto;

        private IAltaOpcionCampoColeccionTextoCommand _cmdAltaOpcionCampoColeccionTexto;
        private IModificaOpcionCampoColeccionTextoCommand _cmdModificaOpcionCampoColeccionTexto;
        private IEliminarOpcionCamposColeccionTextosCommand _cmdEliminarOpcionCamposColeccionTextos;


        public i18NAdministracionController(ILogger<i18NAdministracionController> logger,
                              IJwtTokenUtils jwtTokenUtils,
                              IGetAllIdiomasQuery qryGetIdiomas,
                              IAltaIdiomaCommand cmdAltaIdioma,
                              IModificarIdiomaCommand cmdModificarIdioma,
                              IModificaColeccionTexto cmdModificaColeccionTexto,
                              IEliminarColeccionTextoCommand eliminarColeccionTexto,
                              IAltaCampoColeccionTextoCommand cmdAltaCampoColeccionTexto,
                              IModificarCampoColeccionTextoCommand cmdModificarCampoColeccionTexto,
                              IEliminarCamposColeccionTextoCommand cmdEliminarCamposColeccionTexto,
                              IAltaOpcionCampoColeccionTextoCommand cmdAltaOpcionCampoColeccionTexto,
                              IModificaOpcionCampoColeccionTextoCommand cmdModificaOpcionCampoColeccionTexto,
                              IEliminarOpcionCamposColeccionTextosCommand cmdEliminarOpcionCamposColeccionTextos)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            _qryGetIdiomas = qryGetIdiomas;
            _cmdAltaIdioma = cmdAltaIdioma;
            _cmdModificarIdioma = cmdModificarIdioma;
            _cmdModificaColeccionTexto = cmdModificaColeccionTexto;
            _cmdEliminarColeccionTexto = eliminarColeccionTexto;
            _cmdAltaCampoColeccionTexto = cmdAltaCampoColeccionTexto;
            _cmdModificarCampoColeccionTexto = cmdModificarCampoColeccionTexto;
            _cmdEliminarCamposColeccionTexto = cmdEliminarCamposColeccionTexto;
            _cmdAltaOpcionCampoColeccionTexto = cmdAltaOpcionCampoColeccionTexto;
            _cmdModificaOpcionCampoColeccionTexto = cmdModificaOpcionCampoColeccionTexto;
            _cmdEliminarOpcionCamposColeccionTextos = cmdEliminarOpcionCamposColeccionTextos;
        }


        /// <summary>
        /// Obtiene los idiomas simples y compuestos dados de alta en la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("idiomas/todos")]
        [DbAuthorize("ADM_IDIOMAS", "SW_LST_ALL_IDIOMA", "LLSWEP")]
        public async Task<IActionResult> GetTodosIdiomas()
        {
            try
            {
                List<IdiomaModel> idiomas = await _qryGetIdiomas.execute(true);
                return Ok(idiomas);
            }
            catch (Exception err)
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
            }
            catch (Exception err)
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

        /// <summary>
        /// Método para dar de alta una colección de texto en concreto  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("coleccion")]
        public async Task<IActionResult> AltaColeccionTexto(ColeccionTextoModel model)
        {
            try
            {
                var retorno = await _cmdAltaColeccionTexto.execute(model);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido dar de alta la colección de texto", err);
                return BadRequest("No se ha podido dar de alta la colección de texto");
            }

        }

        /// <summary>
        /// Modifica una colección de texto en concreto
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("coleccion")]
        public async Task<IActionResult> ModificaColeccionTexto(ColeccionTextoModel model)
        {
            try
            {
                var retorno = await _cmdModificaColeccionTexto.execute(model);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido modificar la colección de texto", err);
                return BadRequest("No se ha podido modificar la colección de texto");
            }
        }

        /// <summary>
        /// Elimina una colección de texto en concreto. Si el parámetro "textos" es true, se eliminarán también los textos asociados a la colección. Si es false, se mantendrán los textos pero se desvincularán de la colección eliminada. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="textos"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("coleccion/{id:guid}/textos/{textos:bool}")]
        public async Task<IActionResult> EliminarColeccionTexto(Guid id, bool textos)
        {
            try
            {
                await _cmdEliminarColeccionTexto.execute(id, textos, true);
                return Ok();
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido eliminar la colección de texto", err);
                return BadRequest("No se ha podido eliminar la colección de texto");
            }
        }

        /// <summary>
        /// Crea un campo para una colección de texto dada. El campo se asocia a la colección indicada en el modelo. Si la colección indicada no existe, se lanzará un error. El campo creado se podrá utilizar posteriormente para asociar textos a la colección a través de dicho campo.  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("coleccion/campo")]
        public async Task<IActionResult> AltaCampoColeccionTexto(CampoColeccionTextoModel model)
        {
            try
            {
                var retorno = await _cmdAltaCampoColeccionTexto.execute(model);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido dar de alta el campo de la colección de texto", err);
                return BadRequest("No se ha podido dar de alta el campo de la colección de texto");
            }
        }

        /// <summary>
        /// Modifica un campo para una colección de texto dada. El campo se asocia a la colección indicada en el modelo. Si la colección indicada no existe, se lanzará un error. El campo modificado se podrá utilizar posteriormente para asociar textos a la colección a través de dicho campo.  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("coleccion/campo")]
        public async Task<IActionResult> ModificarCampoColeccionTexto(CampoColeccionTextoModel model)
        {
            try
            {
                var retorno = await _cmdModificarCampoColeccionTexto.execute(model);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido modificar el campo de la colección de texto", err);
                return BadRequest("No se ha podido modificar el campo de la colección de texto");
            }
        }

        /// <summary>
        /// Elimina un campo para una colección de texto dada. Si el parámetro "textos" es true, se eliminarán también los textos asociados a dicho campo en la colección. Si es false, se mantendrán los textos pero se desvincularán del campo eliminado. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="textos"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("coleccion/campo/{id:guid}/textos/{textos:bool}")]
        public async Task<IActionResult> EliminarCampoColeccionTexto(Guid id, bool textos)
        {
            try
            {
                await _cmdEliminarCamposColeccionTexto.execute(id, textos, true);
                return Ok();
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido eliminar el campo de la colección de texto", err);
                return BadRequest("No se ha podido eliminar el campo de la colección de texto");
            }
        }

        /// <summary>
        /// Crea una opción para un campo de una colección de texto dada. La opción se asocia al campo y a la colección indicados en el modelo. Si el campo o la colección indicados no existen, se lanzará un error. La opción creada se podrá utilizar posteriormente para asociar textos a la colección a través de dicho campo y opción.    
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("coleccion/campo/opcion")]
        public async Task<IActionResult> AltaOpcionCampoColeccionTexto(OpcionCampoColeccionTextoModel model)
        {
            try
            {
                var retorno = await _cmdAltaOpcionCampoColeccionTexto.execute(model);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido dar de alta la opción del campo de la colección de texto", err);
                return BadRequest("No se ha podido dar de alta la opción del campo de la colección de texto");
            }
        }

        /// <summary>
        /// Modifica una opción para un campo de una colección de texto dada. La opción se asocia al campo y a la colección indicados en el modelo. Si el campo o la colección indicados no existen, se lanzará un error. La opción modificada se podrá utilizar posteriormente para asociar textos a la colección a través de dicho campo y opción.    
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("coleccion/campo/opcion")]
        public async Task<IActionResult> ModificarOpcionCampoColeccionTexto(OpcionCampoColeccionTextoModel model)
        {
            try
            {
                var retorno = await _cmdModificaOpcionCampoColeccionTexto.execute(model);
                return Ok(retorno);
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido modificar la opción del campo de la colección de texto", err);
                return BadRequest("No se ha podido modificar la opción del campo de la colección de texto");
            }
        }

        /// <summary>
        /// Elimina una opción para un campo de una colección de texto dada. Si el parámetro "textos" es true, se eliminarán también los textos asociados a dicha opción en la colección. Si es false, se mantendrán los textos pero se desvincularán de la opción eliminada.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="textos"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("coleccion/campo/opcion/{id:guid}/textos/{textos:bool}")]
        public async Task<IActionResult> EliminarOpcionCampoColeccionTexto(Guid id, bool textos)
        {
            try
            {
                await _cmdEliminarOpcionCamposColeccionTextos.execute(id, textos, true);
                return Ok();
            }
            catch (Exception err)
            {
                this._logger.LogError("No se ha podido eliminar la opción del campo de la colección de texto", err);
                return BadRequest("No se ha podido eliminar la opción del campo de la colección de texto");
            }
        }
    }
}
