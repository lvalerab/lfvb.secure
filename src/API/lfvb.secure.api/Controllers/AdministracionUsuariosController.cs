
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Credencial.Commands.CaducarCredencial;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos;
using lfvb.secure.aplication.Database.Grupos.Queries.GetGruposUsuario;
using lfvb.secure.aplication.Database.TipoCrendecial.Models;
using lfvb.secure.aplication.Database.TipoCrendecial.Queries.GetAllTiposCredenciales;
using lfvb.secure.aplication.Database.Usuario.Commands.CreateUsuario;
using lfvb.secure.aplication.Database.Usuario.Models;
using lfvb.secure.aplication.Database.Usuario.Queries.GetAllUsuarios;
using lfvb.secure.aplication.Database.Usuario.Queries.GetCredencialesUsuario;
using lfvb.secure.aplication.Database.Usuario.Queries.GetUsuario;
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
        private IGetUsuarioQuery _qruGetUsuario;
        private ICreateUsuarioCommand _cmdCreateUsuario;
        private ICaducarCredencialCommand _cmdCaducarCredencial;

        public AdministracionUsuariosController(ILogger<PermisosController> logger, 
                                                IJwtTokenUtils jwtTokenUtils,
                                                IGetAllUsuariosQuery qryGetUsuarios,
                                                IGetAllTiposCredencialesQuery qryAllTiposCredenciales,
                                                IGetGruposUsuario qryGruposUsuarios,
                                                IGetAllGruposQuery qryGrupos,
                                                IGetCredencialesUsuarioQuery qryCredencialesUsuario,
                                                IGetUsuarioQuery qruGetUsuario,
                                                ICreateUsuarioCommand cmdCreateUsuario,
                                                ICaducarCredencialCommand cmdCaducarCredencial)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryGetUsuarios = qryGetUsuarios;
            this._qryAllTiposCredenciales = qryAllTiposCredenciales;
            this._qryGruposUsuarios = qryGruposUsuarios;
            this._qryGrupos = qryGrupos;
            this._qryCredencialesUsuario = qryCredencialesUsuario;
            this._qruGetUsuario = qruGetUsuario;
            this._cmdCreateUsuario = cmdCreateUsuario;
            this._cmdCaducarCredencial = cmdCaducarCredencial;
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
            List<UsuarioModel> lista = await this._qryGetUsuarios.Execute(pagina, elementos);
            return Ok(lista);
        }

        /// <summary>
        /// Obtiene los datos de cualquier usuario del sistema, tiene que tener los permisos de consulta de usuarios/listados de usuarios
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [Authorize]
        [DbAuthorize("ADM_USR", "SW_ADM_USR_LST_USU", "LLSWEP")]
        public async Task<IActionResult> GetUsuario(Guid id)
        {
            UsuarioModel usuario = await this._qruGetUsuario.Execute(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
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

        /// <summary>
        /// Caduca las credenciales de un usuario de un tipo determinado, las credenciales caducadas no se pueden volver a utilizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codTIpo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario/{id:guid}/credencial/tipo/{codTIpo}/revocar")]
        [Authorize]
        [DbAuthorize("ADM_USR", "SW_ADM_USR_CRD_CADUCAR", "LLSWEP")]
        public async Task<IActionResult> CaducarCredencialUsuario(Guid id, string codTIpo)
        {
            try
            {
                int credenciales = await this._cmdCaducarCredencial.execute(id, codTIpo);                
                return Ok(credenciales);
            } catch (Exception err)
            {
                return BadRequest(err);
            }
        }

        /// <summary>
        /// Metodo para crear un usuario, debe tener el permiso se llamar a crear usuario del panel de administracion de usuarios
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("usuario")]
        [Authorize]
        [DbAuthorize("ADM_USR","SW_ADM_USR_ALT_USU","LLSWEP")]
        public async Task<IActionResult> AltaUsuario(CreateUsuarioModel datos)
        {
            try { 
                CreateUsuarioModel md = await this._cmdCreateUsuario.Execute(datos);
            return Ok(md);
            } catch (Exception err)
            {
                return BadRequest(err);
            }
        }

    }
}
