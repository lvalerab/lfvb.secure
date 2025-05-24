
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using lfvb.secure.aplication.Database.TipoCrendecial.Queries.GetAllTiposCredenciales;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios;
using lfvb.secure.aplication.Database.Usuario.Queries.GetCredencialesUsuario;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers
{
    /// <summary>
    /// Controlador para la administración de usuarios
    /// </summary>
    [Route("api/administracion/usuarios")]
    [ApiController]
    public class AdministracionUsuariosController:ControllerBase
    {
        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IGetAllUsuariosQuery _qryGetUsuarios;
        private IGetAllTiposCredencialesQuery _qryAllTiposCredenciales;
        private IGetGruposUsuario _qryGruposUsuarios;
        private IGetAllGruposQuery _qryGrupos;
        private IGetCredencialesUsuarioQuery _qryCredencialesUsuario;   

        public AdministracionUsuariosController(ILogger<PermisosController> logger, 
                                                IJwtTokenUtils jwtTokenUtils,
                                                IGetAllUsuariosQuery qryGetUsuarios,
                                                IGetAllTiposCredencialesQuery qryAllTiposCredenciales,
                                                IGetGruposUsuario qryGruposUsuarios,
                                                IGetAllGruposQuery qryGrupos,
                                                IGetCredencialesUsuarioQuery qryCredencialesUsuario)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryGetUsuarios = qryGetUsuarios;
            this._qryAllTiposCredenciales = qryAllTiposCredenciales;
            this._qryGruposUsuarios = qryGruposUsuarios;
            this._qryGrupos = qryGrupos;
            this._qryCredencialesUsuario = qryCredencialesUsuario;
        }

        /// <summary>
        /// Obtiene la lista de usuarios del sistema
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="elementos"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lista/{pagina:int}/{elementos:int}")]
        [Authorize]
        [DbAuthorize("ADM_USR", "SW_ADM_USR_LST_USU", "LLSWEP")]
        public async Task<IActionResult> GetListaUsuarios(int pagina, int elementos)
        {
            List<GetAllUsuariosModel> lista = await this._qryGetUsuarios.Execute(pagina, elementos);
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene la lista de tipos de credenciales que soporta el sistema y estan vigentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("credenciales/tipos")]
        public async Task<IActionResult> GetAllTiposCredenciales()
        {
            List<TipoCredencialModel> lista = await this._qryAllTiposCredenciales.Execute();
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene el listado de grupos de usuarios disponibles en el sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("grupos")]
        public async Task<IActionResult> GetGrupos()
        {
            List<GrupoModel> lista = await this._qryGrupos.Execute();
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene el listado de grupos a los que pertenece un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/{id:guid}/lista/grupos")]
        [Authorize]
        [DbAuthorize("ADM_USR", "SW_ADM_USR_GRP_USU", "LLSWEP")]
        public async Task<IActionResult> GetGruposUsuario(Guid id)
        {
            List<GetGruposUsuarioModel> lista = await this._qryGruposUsuarios.Execute(id);
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene el listado de credenciales asignadas a un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/{id:guid}/lista/credenciales")]
        [Authorize]
        [DbAuthorize("ADM_USR", "SW_ADM_USR_CRD_USU", "LLSWEP")]
        public async Task<IActionResult> GetCredencialesUsuario(Guid id)
        {
            List<CredencialUsuarioModel> lista = await this._qryCredencialesUsuario.Execute(id);
            return Ok(lista);
        }

    }
}
