using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Queries.Tipos;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Queries.Unidades;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;


namespace lfvb.secure.api.Controllers.UnidadesOrganizativas
{
    /// <summary>
    /// Controlador para obtencion de datos de unidades organizativas
    /// </summary>
    [ApiController]
    [Route("api/modulos/unidad/organizativa")]
    public class UnidadesOrganizativasController : ControllerBase
    {

        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllTiposUnidadesOrganizativasQuery _qryGetAllTiposUnidades;
        private IGetArbolUnidadesOrganizativasQuery _qryGetArbolUnidades;

        public UnidadesOrganizativasController(ILogger<PermisosController> logger,
                                                             IJwtTokenUtils jwtTokenUtils,
                                                             IGetAllTiposUnidadesOrganizativasQuery qryGetAllTiposUnidades,
                                                             IGetArbolUnidadesOrganizativasQuery qryGetArbolUnidades)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;

            _qryGetAllTiposUnidades = qryGetAllTiposUnidades;
            _qryGetArbolUnidades = qryGetArbolUnidades;
        }

        /// <summary>
        /// Obtiene el listado de tipos de unidades organizativas
        /// </summary>
        /// <returns></returns>
        [HttpGet("tipos")]
        public async Task<IActionResult> Hola()
        {
            var tipos = await _qryGetAllTiposUnidades.execute();
            return Ok(tipos);
        }

        /// <summary>
        /// Obtiene el arbol de unidades organizativas (O los arboles si no se indica codPadre) 
        /// </summary>
        /// <param name="codPadre"></param>
        /// <param name="Tipo"></param>
        /// <param name="nivelMax"></param>
        /// <returns></returns>
        [HttpGet("arbol")]
        [Authorize]
        public async Task<IActionResult> GetArbolUnidadesOrganizativas([FromQuery] Guid? codPadre = null,
                                                                      [FromQuery] Guid? Tipo = null,
                                                                      [FromQuery] int nivelMax = 999)
        {
            List<UnidadOrganizativaModel> unidades = await _qryGetArbolUnidades.execute(codPadre, Tipo, nivelMax);
            return Ok(unidades);
        }
    }
            
}
