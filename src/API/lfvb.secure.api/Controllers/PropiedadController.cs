using lfvb.secure.api.ParametrosModel;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lfvb.secure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadController:ControllerBase
    {
        private ILogger<PropiedadController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;
        private IConfiguration _configuration;
        private IGetAllTiposPropiedadesQuery _getAllTipoPropiedadesQuery;
        private IGetAllPropiedadesQuery _getAllPropiedadesQuery;  
        private IGetPropiedadesElementoQuery _getPropiedadesElementoQuery;
        private readonly string secret;
        private readonly int expires;
        public PropiedadController(ILogger<PropiedadController> logger, IJwtTokenUtils jwtUtils, IConfiguration config, IGetAllTiposPropiedadesQuery GetAllTipoPropiedadesQuery, IGetAllPropiedadesQuery getAllPropiedades, IGetPropiedadesElementoQuery getPropiedadesElementoQuery)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtUtils;
            this._configuration = config;
            this._getAllTipoPropiedadesQuery = GetAllTipoPropiedadesQuery;
            this._getAllPropiedadesQuery = getAllPropiedades;
            this._getPropiedadesElementoQuery = getPropiedadesElementoQuery;
        }

        /// <summary>
        /// Obtiene el listado de propiedades que hay en el sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tipos")]        
        public async Task<IActionResult> GetAllTiposPropiedades()
        {
            List<TipoPropiedadModel> tipos = await this._getAllTipoPropiedadesQuery.Execute();
            return Ok(tipos);
        }

        /// <summary>
        /// Obtiene el listado de propiedades que hay en el sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> GetAllPropiedades(string? codPadrePropiedad = null,string? codTipoElemento=null)
        {
            List<PropiedadModel> propiedades = await this._getAllPropiedadesQuery.Execute(codPadrePropiedad,codTipoElemento);
            return Ok(propiedades);
        }

        /// <summary>
        /// Obtiene el listado de propieddades de un elemento dado
        /// </summary>
        /// <param name="idElemento"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("elemento/{idElemento:guid}")]
        [Authorize]
        public async Task<IActionResult> GetPropiedadesElemento(Guid idElemento)
        {
            List<PropiedadElementoModel> propiedades = await this._getPropiedadesElementoQuery.Execute(idElemento);
            return Ok(propiedades);
        }

        /// <summary>
        /// Obtiene las propiedades de un elemento (seperado por comas) pasadas por parametro
        /// </summary>
        /// <param name="idElemento"></param>
        /// <param name="codPropiedades"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("elemento/{idElemento:guid}/{codPropiedades}")]
        [Authorize]
        public async Task<IActionResult> GetPropiedadesElemento(Guid idElemento, string codPropiedades)
        {
            List<PropiedadElementoModel> propiedades = await this._getPropiedadesElementoQuery.Execute(idElemento,codPropiedades);
            return Ok(propiedades);
        }
        /// <summary>
        /// Para consultar el listado de propidades de un listado de elementos
        /// </summary>
        /// <param name="idElementos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("consulta/masiva/elementos")]
        [Authorize]
        public async Task<IActionResult> GetPropiedadesElementoMasiva([FromBody] ParameterElementosPropiedadesModel parametros)
        {
            List<PropiedadElementoModel> propiedades = null;
            if (parametros.CodigoPropiedad== null || parametros.CodigoPropiedad.Count == 0)
            {
                propiedades = await this._getPropiedadesElementoQuery.Execute(parametros.IdElementos);
            }  else
            {
                propiedades = await this._getPropiedadesElementoQuery.Execute(parametros.IdElementos,parametros.CodigoPropiedad);
            }   
            return Ok(propiedades);
        }

        
    }
}
