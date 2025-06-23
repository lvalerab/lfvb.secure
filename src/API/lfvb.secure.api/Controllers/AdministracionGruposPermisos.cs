using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.Grupos.Queries.GetUsuariosGrupo;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGrupo;
using lfvb.secure.aplication.Database.Grupos.Commands.AltaGrupoUsuariosPermisos;
using lfvb.secure.aplication.Database.Grupos.Commands.ActualizaGrupoUsuariosPerisos;

namespace lfvb.secure.api.Controllers
{
    /// <summary>
    /// Controlador para la adminsitracion de los grupos de permisos del sistema
    /// </summary>
    [ApiController]
    [Route("api/administracion/permisos/grupos")]
    public class AdministracionGruposPermisos : ControllerBase
    {

        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IGetAllGruposQuery _qryListaGrupos;
        private IGetGrupoQuery _qryGrupo;
        private IGetUsuariosGrupoQuery _qryGetUsuariosGrupos;
        private IAltaGrupoUsuariosPermisosCommand _cmdAltaGrupoUsuariosPermisos;
        private IActualizaGrupoUsuariosPermisosCommand _cmdActualizaGrupoUsuariosPermisos;

        public AdministracionGruposPermisos(ILogger<PermisosController> logger,
                                            IJwtTokenUtils jwtTokenUtils,
                                            IGetAllGruposQuery qryListaGrupos,
                                            IGetGrupoQuery qryGrupo,
                                            IGetUsuariosGrupoQuery qryUsuariosGrupo,
                                            IAltaGrupoUsuariosPermisosCommand cmdAltaGrupoUsuariosPermisos,
                                            IActualizaGrupoUsuariosPermisosCommand cmdActualizaGrupoUsuariosPermisos)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            _qryListaGrupos = qryListaGrupos;
            this._qryGetUsuariosGrupos = qryUsuariosGrupo;
            this._qryGrupo = qryGrupo;
            this._cmdAltaGrupoUsuariosPermisos = cmdAltaGrupoUsuariosPermisos;
            this._cmdActualizaGrupoUsuariosPermisos = cmdActualizaGrupoUsuariosPermisos;
        }

        /// <summary>
        /// Obtiene el listado de grupos de permisos del sistema    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Lista()
        {
            try
            {
                List<GrupoModel> grupos = await _qryListaGrupos.Execute();
                return Ok(grupos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de grupos de permisos");
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene el detalle de un grupo de permisos
        /// </summary>
        /// <param name="id">Identificador del grupo</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<IActionResult> DetalleGrupo(Guid Id)
        {
            try
            {
                GrupoModel? grupo = await _qryGrupo.Execute(Id);
                if (grupo == null)
                {
                    return NotFound();
                }
                return Ok(grupo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el grupo de permisos {grupoId}", Id);
                return BadRequest();
            }
        }

        /// <summary>
        /// Obtiene los usuarios que pertenecen a un grupo de permisos
        /// </summary>
        /// <param name="Id">Identificador del grupo</param>
        /// <returns></returns>
        [HttpGet("{Id}/usuarios")]
        [Authorize]
        public async Task<IActionResult> ListaUsuariosGrupo(Guid Id)
        {
            try
            {
                List<UsuarioModel> usuarios = await _qryGetUsuariosGrupos.Execute(Id);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de usuarios del grupo {grupoId}", Id);
                return BadRequest();
            }
        }

        /// <summary>
        /// Da de alta un grupo de usuarios y permisos en el sistema
        /// </summary>
        /// <param name="grupo"></param>
        /// <returns></returns>
        [HttpPost("grupo")]
        [Authorize]
        public async Task<IActionResult> AltaGrupoUsuariosPermisos([FromBody] GrupoModel grupo)
        {
            try
            {
                if (grupo == null)
                {
                    return BadRequest("El grupo no puede ser nulo");
                } else if (grupo.Id != null && grupo.Id!=Guid.Empty)
                {
                    return BadRequest("El grupo no puede tener identificador");
                }
                grupo.Id = null;
                GrupoModel resultado = await _cmdAltaGrupoUsuariosPermisos.Execute(grupo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta el grupo de usuarios y permisos");
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Actualiza los datos de un grupo de usuarios y permisos en el sistema    
        /// </summary>
        /// <param name="grupo"></param>
        /// <returns></returns>
        [HttpPut("grupo")]
        [Authorize]
        public async Task<IActionResult> ActualizaGrupoUsuariosPermisos([FromBody] GrupoModel grupo)
        {
            try
            {
                if (grupo == null || grupo.Id == null || grupo.Id==Guid.Empty)
                {
                    return BadRequest("El grupo no puede ser nulo y debe tener un identificador");
                }
                GrupoModel resultado = await _cmdActualizaGrupoUsuariosPermisos.Execute(grupo);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el grupo de usuarios y permisos");
                return BadRequest(ex.Message);
            }

        }
    }
}
