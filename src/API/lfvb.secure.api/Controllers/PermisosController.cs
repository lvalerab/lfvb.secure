using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacionesUsuario;
using lfvb.secure.aplication.Database.Aplicaciones.Queries.PermisoElementoAplicacion;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Database.Usuario.Queries.ElementosUsuario;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers
{
    /// <summary>
    /// Controlador para manejar los permisos del usuario logeado
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermisosController : Controller
    {
        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IGetGruposUsuario _qryGruposUsuarios;
        private IGetAplicacionesUsuarioQuery _qryAplicacionesUsuario;
        private IGetAllElementosUsuario _qryAllElementosUsuario;
        private IPermisoElementoAplicacionQuery _qryPermisosElementoAplicacionQuery;

        public PermisosController(ILogger<PermisosController> logger,
                                  IJwtTokenUtils jwtTokenUtils,
                                  IGetGruposUsuario qryGruposUsuarios,
                                  IGetAplicacionesUsuarioQuery qryAplicacionesUsuario,
                                  IGetAllElementosUsuario qryAllElementosUsuario,
                                  IPermisoElementoAplicacionQuery qryPermisosElementoAplicacionQuery)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _qryGruposUsuarios = qryGruposUsuarios;
            _qryAplicacionesUsuario = qryAplicacionesUsuario;
            _qryAllElementosUsuario = qryAllElementosUsuario;
            _qryPermisosElementoAplicacionQuery = qryPermisosElementoAplicacionQuery;
        }

        /// <summary>
        /// Obtiene los grupos de permisos a los que pertenece el usuario logeado
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/grupos")]
        public async Task<IActionResult> GetGruposUsuario()
        {
            Guid id = Guid.Empty;
            try
            {
                id = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (id == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    List<GetGruposUsuarioModel> grupos = await this._qryGruposUsuarios.Execute(id);
                    return StatusCode(StatusCodes.Status200OK, grupos);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al obtener los grupos del usuario", new { id, ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene las aplicaciones en las que tiene permisos un susuario 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/aplicaciones")]
        public async Task<IActionResult> GetAplicacionesUsuario()
        {
            Guid id = Guid.Empty;
            try
            {
                id = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (id == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    List<GetAplicacionUsuarioModel> aplicaciones = await this._qryAplicacionesUsuario.Execute(id);
                    return StatusCode(StatusCodes.Status200OK, aplicaciones);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al obtener las aplicaciones del usuario", new { id, ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene todos los elementos donde el usuario puede interactuar
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/elementos")]
        public async Task<IActionResult> GetElementosDelUsuario()
        {
            Guid id = Guid.Empty;
            try
            {
                id = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (id == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    List<VMElementosModel> elementos = await this._qryAllElementosUsuario.Execute(id);
                    return StatusCode(StatusCodes.Status200OK, elementos);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al obtener los elementos del usuario", new { id, ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Nos indica si el usuario puede manejar el elemento indicado
        /// </summary>
        /// <param name="idElemento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/actua/elemento/{idElemento:Guid}")]
        public async Task<IActionResult> PuedeActuarSobreElElemento(Guid idElemento)
        {
            Guid id = Guid.Empty;
            try
            {
                id = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (id == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    bool Puede = await this._qryAllElementosUsuario.Execute(id, idElemento);
                    return StatusCode(StatusCodes.Status200OK, Puede);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al comprobar si hay relacion entre el usuario y el elemento", new { id, idElemento, ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene los permisos de un elemento de una aplicacion para el usuario logeado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idElemento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/permiso/aplicacion/{id:guid}/elemento/{idElemento:guid}")]
        [Authorize]
        public async Task<IActionResult> GetPermisoElementoAplicacion(Guid id, Guid idElemento)
        {
            Guid idUsuario = Guid.Empty;
            try
            {
                idUsuario = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (idUsuario == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    var permiso = await this._qryPermisosElementoAplicacionQuery.Execute(idUsuario, id, idElemento);
                    if (permiso.CodigoTipoPermiso.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, permiso);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al comprobar si hay relacion entre el usuario y el elemento", new { idUsuario, id, idElemento, ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene los permisos de un elemento de una aplicacion para el usuario logeado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idElemento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/permiso/aplicacion/{id:guid}/elemento/{idElemento:guid}/tipo/{codTipo}")]
        [Authorize]
        public async Task<IActionResult> GetPermisoElementoAplicacion(Guid id, Guid idElemento, string codTipo)
        {
            Guid idUsuario = Guid.Empty;
            try
            {
                idUsuario = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (idUsuario == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    var permiso = await this._qryPermisosElementoAplicacionQuery.Execute(idUsuario, id, idElemento, codTipo);
                    if (permiso.CodigoTipoPermiso.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, permiso);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al comprobar si hay relacion entre el usuario y el elemento", new { idUsuario, id, idElemento, ex = ex });
                return BadRequest();
            }

        }

        /// <summary>
        /// Obtiene los permisos de un elemento de una aplicacion para el usuario logeado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idElemento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/permiso/aplicacion/{cod}/elemento/{codElemento}")]
        [Authorize]
        public async Task<IActionResult> GetPermisoElementoAplicacion(string cod, string codElemento)
        {
            Guid idUsuario = Guid.Empty;
            try
            {
                idUsuario = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (idUsuario == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    var permiso = await this._qryPermisosElementoAplicacionQuery.Execute(idUsuario, cod, codElemento);
                    if(permiso.CodigoTipoPermiso.Count>0) { 
                        return StatusCode(StatusCodes.Status200OK, permiso);
                    } else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al comprobar si hay relacion entre el usuario y el elemento", new { idUsuario, cod, codElemento, ex = ex });
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene los permisos de un elemento de una aplicacion para el usuario logeado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idElemento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/permiso/aplicacion/{cod}/elemento/{codElemento}/tipo/{codTipo}")]
        [Authorize]
        public async Task<IActionResult> GetPermisoElementoAplicacion(string cod, string codElemento, string codTipo)
        {
            Guid idUsuario = Guid.Empty;
            try
            {
                idUsuario = this._jwtTokenUtils.GetIdFromToken(HttpContext) ?? Guid.Empty;
                if (idUsuario == Guid.Empty)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    var permiso = await this._qryPermisosElementoAplicacionQuery.Execute(idUsuario, cod, codElemento, codTipo);
                    if (permiso.CodigoTipoPermiso.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, permiso);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status401Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error al comprobar si hay relacion entre el usuario y el elemento", new { idUsuario, cod, codElemento, ex = ex });
                return BadRequest();
            }

        }

    }
}
