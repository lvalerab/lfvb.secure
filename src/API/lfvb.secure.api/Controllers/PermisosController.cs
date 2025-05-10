using lfvb.secure.aplication.Database.Aplicaciones.Queries.GetAplicacionesUsuario;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
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
    public class PermisosController:Controller
    {
        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IGetGruposUsuario _qryGruposUsuarios;
        private IGetAplicacionesUsuarioQuery _qryAplicacionesUsuario;
        private IGetAllElementosUsuario _qryAllElementosUsuario;

        public PermisosController(ILogger<PermisosController> logger, IJwtTokenUtils jwtTokenUtils, IGetGruposUsuario qryGruposUsuarios, IGetAplicacionesUsuarioQuery qryAplicacionesUsuario, IGetAllElementosUsuario qryAllElementosUsuario)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _qryGruposUsuarios = qryGruposUsuarios;
            _qryAplicacionesUsuario=qryAplicacionesUsuario;
            _qryAllElementosUsuario=qryAllElementosUsuario;
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
    
    }
}
