
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaActualizacionElementoAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaPermisoElementoAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAllAplicaciones;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetArbolElementosAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetGruposAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisosElementosAplicacionPorGrupoYAplicacion;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.TiposPermisosElementoPorTipoQuery;
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
        private IAltaAplicacionCommand _altaAplicacionCommand;
        private IAltaActualizacionElementoAplicacionCommand _altaActualizacionElementoAplicacionCommand;
        private IPermisosElementosAplicacionPorGrupoYAplicacionQuery _qryPermisosElementosAplicacion;
        private ITiposPermisosElementoPorTipoQuery _qryTiposPermisosElementoPorTipoQuery;
        private IAltaPermisoElementoAplicacionCommand _altaPermisoElementoAplicacionCommand;

        public AdministracionAplicacionesController(ILogger<PermisosController> logger,
                                                    IJwtTokenUtils jwtTokenUtils,
                                                    IGetAllAplicacionesQuery qryListaAplicaciones,
                                                    IGetAplicacionQuery qryAplicacion,
                                                    IGetArbolElementosAplicacion qryArbolElementosAplicacion,
                                                    IGetGruposAplicacionQuery qryGruposAplicacion,
                                                    IGetAllTiposElementosAplicacionQuery qryTiposElementosAplicacion,
                                                    IAltaAplicacionCommand altaAplicacionCommand,
                                                    IAltaActualizacionElementoAplicacionCommand altaActualizacionElementoAplicacionCommand,
                                                    IPermisosElementosAplicacionPorGrupoYAplicacionQuery qryPermisosElementosAplicacion,
                                                    ITiposPermisosElementoPorTipoQuery qryTiposPermisosElementoPorTipoQuery,
                                                    IAltaPermisoElementoAplicacionCommand altaPermisoElementoAplicacionCommand)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryListaAplicaciones = qryListaAplicaciones;
            this._qryAplicacion = qryAplicacion;
            this._getArbolElementosAplicacion = qryArbolElementosAplicacion;
            this._qryGruposAplicacion = qryGruposAplicacion;
            this._qryTiposElementosAplicacion = qryTiposElementosAplicacion;
            this._altaAplicacionCommand = altaAplicacionCommand;
            this._altaActualizacionElementoAplicacionCommand = altaActualizacionElementoAplicacionCommand;
            this._qryPermisosElementosAplicacion = qryPermisosElementosAplicacion;
            this._qryTiposPermisosElementoPorTipoQuery = qryTiposPermisosElementoPorTipoQuery;
            this._altaPermisoElementoAplicacionCommand = altaPermisoElementoAplicacionCommand;
        }

        /// <summary>
        /// Obtiene la lista de todas las aplicaciones disponibles en el sistema (Permiso: SW_ADM_APL_LST_APL)
        /// </summary>
        /// <returns></returns>
        [HttpGet("lista")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_APL_LST_APL", "LLSWEP")]
        public async Task<IActionResult> Lista()
        {
            List<AplicacionModel> aplicaciones = await _qryListaAplicaciones.Execute();

            return Ok(aplicaciones);
        }

        /// <summary>
        /// Obtiene los datos de una aplicacion en particular (Permiso: SW_ADM_APL_FICH_APL)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_APL_FICH_APL", "LLSWEP")]
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
        /// Da de a una nueva aplicacion en el sistema  
        /// </summary>
        /// <param name="aplicacion"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_APL_COM_ALTA","LLSWEP")]
        public async Task<IActionResult> AltaAplicacion([FromBody] AltaAplicacionModel aplicacion)
        {
            if (aplicacion == null)
            {
                return BadRequest("Los datos de la aplicacion no pueden ser nulos");
            }
            try
            {
                AplicacionModel nuevaAplicacion = await _altaAplicacionCommand.Execute(aplicacion);
                return Ok(nuevaAplicacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta la aplicacion");
                return BadRequest("Error interno del servidor al dar de alta la aplicacion");
            }
        }

        /// <summary>
        /// Obtiene el arbol de elementos de una aplicacion (Permiso SW_ADM_EAPL_LST_ELEM)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("elementos/{id}")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_EAPL_LST_ELEM","LLSWEP")]
        public async Task<IActionResult> GetElementosAplicacion(Guid id)
        {
            var elementos = await _qryAplicacion.Execute(id);
            return Ok(elementos);
        }

        /// <summary>
        /// Da de alta un nuevo elemento de la aplicacion (Permiso SW_ADM_EAPL_ALT_ELEM)
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        [HttpPost("elementos/alta")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_EAPL_ALT_ELEM","LLSWEP")]
        public async Task<IActionResult> AltaElementoAplicacion([FromBody] ElementoAplicacionModel elemento)
        {
            if (elemento == null)
            {
                return BadRequest("Los datos del elemento no pueden ser nulos");
            }
            else if (elemento.Id != null)
            {
                return BadRequest("Este metodo solo sirve para añadir nuevos elementos");
            }
            try
            {
                ElementoAplicacionModel nuevoElemento = await _altaActualizacionElementoAplicacionCommand.Execute(elemento);
                return Ok(nuevoElemento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta el elemento de aplicacion");
                return BadRequest("Error interno del servidor al dar de alta el elemento de aplicacion");
            }
        }

        /// <summary>
        /// Da de alta un nuevo elemento de la aplicacion (Permiso SW_ADM_EAPL_ALT_ELEM)
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        [HttpPut("elementos/actualizar")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_EAPL_ALT_ELEM","LLSWEP")]
        public async Task<IActionResult> ActualizaElementoAplicacion([FromBody] ElementoAplicacionModel elemento)
        {
            if (elemento == null)
            {
                return BadRequest("Los datos del elemento no pueden ser nulos");
            }
            else if (elemento.Id == null)
            {
                return BadRequest("Este metodo solo sirve para actualizar un elemento dado");
            }
            try
            {
                ElementoAplicacionModel nuevoElemento = await _altaActualizacionElementoAplicacionCommand.Execute(elemento);
                return Ok(nuevoElemento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta el elemento de aplicacion");
                return BadRequest("Error interno del servidor al dar de alta el elemento de aplicacion");
            }
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
        [DbAuthorize("ADM_APLI", "SW_ADM_APL_LST_GRP","LLSWEP")]
        public async Task<IActionResult> GetGruposAplicacion(Guid id)
        {
            var grupos = await _qryGruposAplicacion.Execute(id);
            return Ok(grupos);
        }

        /// <summary>
        /// Obtiene el listado de permisos de los elementos de un grupo y una aplicacion
        /// </summary>
        /// <param name="idAplicacion"></param>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        [HttpGet("elementos/permiso/{idAplicacion}/{idGrupo}")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_ELP_PERM_LST","LLSWEP")]
        public async Task<IActionResult> GetPermisosElementosAplicacionPorGrupoYAplicacion(Guid idAplicacion, Guid idGrupo)
        {
            var permisos = await _qryPermisosElementosAplicacion.Execute(idAplicacion, idGrupo);
            return Ok(permisos);
        }

        /// <summary>
        /// Obtiene los tipos de permisos en base al tipo de elemento de la aplicacion
        /// </summary>
        /// <param name="codigoTipo"></param>
        /// <returns></returns>
        [HttpGet("elementos/tipo/{codigoTipo}/permisos")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_ELP_PERM_LST","LLSWEP")]
        public async Task<IActionResult> GetTiposPermisosElementoPorTipo(string codigoTipo)
        {
            var tiposPermisos = await _qryTiposPermisosElementoPorTipoQuery.Execute(codigoTipo);
            return Ok(tiposPermisos);
        }

        /// <summary>
        /// Da de alta un nuevo permiso para un elemento de aplicacion (Permiso SW_ADM_ELP_PERM_ALTA)   
        /// </summary>
        /// <param name="permiso"></param>
        /// <returns></returns>
        [HttpPost("elementos/permiso/alta")]
        [Authorize]
        [DbAuthorize("ADM_APLI", "SW_ADM_ELP_PERM_ALTA","LLSWEP")]
        public ActionResult AltaPermisoElementoAplicacion([FromBody] AltaPermisoElementoAplicacionModel permiso)
        {
            if (permiso == null)
            {
                return BadRequest("Los datos del permiso no pueden ser nulos");
            }
            try
            {
                AltaPermisoElementoAplicacionModel nuevoPermiso = _altaPermisoElementoAplicacionCommand.Execute(permiso).Result;
                return Ok(nuevoPermiso);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta el permiso de elemento de aplicacion");
                return BadRequest("Error interno del servidor al dar de alta el permiso de elemento de aplicacion");
            }
        }
    }
}
