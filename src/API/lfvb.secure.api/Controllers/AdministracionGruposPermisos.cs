using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos;
using lfvb.secure.aplication.Database.Grupos.Models;

namespace lfvb.secure.api.Controllers
{
    /// <summary>
    /// Controlador para la adminsitracion de los grupos de permisos del sistema
    /// </summary>
    [ApiController]
    [Route("api/administracion/permisos/grupos")]
    public class AdministracionGruposPermisos:ControllerBase
    {

        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IGetAllGruposQuery _qryListaGrupos;

        public AdministracionGruposPermisos(ILogger<PermisosController> logger, 
                                            IJwtTokenUtils jwtTokenUtils,
                                            IGetAllGruposQuery qryListaGrupos)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            _qryListaGrupos = qryListaGrupos;
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

    }
}
