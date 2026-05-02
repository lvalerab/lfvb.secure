using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Direcciones.Queries;
using lfvb.secure.aplication.Database.Direcciones.Models;
using lfvb.secure.aplication.Database.Direcciones.Commands.Direccion;

namespace lfvb.secure.api.Controllers.Direcciones
{
    [ApiController]
    [Route("api/direcciones")]
    public class DireccionesController : ControllerBase
    {
        private ILogger<DireccionesController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private IGetAllTiposEntidadesTerritorialesQuery _qryAllTiposEntiTerri;
        private IBuscarEntidadesQuery _qryBuscarEntidades;
        private IGetAllTiposViasQuery _qryAllTiposVias;
        private IGetEntidadTerritorialQuery _qryGetEntidadTerritorial;
        private IBuscadorCallejeroQuery _qryBuscadorCallejero;

        private IGetDireccionesPersonaQuery _qryDireccionesPersona;
        private IGetDireccionQuery _qryDireccion;
        private IGetArbolEntidadTerritorialQuery _qryArbolEntidadTerritorial;

        private IAltaModificacionDireccionCommand _cmdAltaModificacionDireccion;

        public DireccionesController(ILogger<DireccionesController> logger,
                                    IJwtTokenUtils jwtTokenUtils,
                                    IGetAllTiposEntidadesTerritorialesQuery qryAllTiposEntiTerri,
                                    IBuscarEntidadesQuery qryBuscarEntidades,
                                    IGetAllTiposViasQuery qryAllTiposVias,
                                    IGetEntidadTerritorialQuery qryGetEntidadTerritorial,
                                    IBuscadorCallejeroQuery qryBuscadorCallejero,
                                    IGetDireccionesPersonaQuery qryDireccionesPersona,
                                    IGetDireccionQuery qryDireccion,
                                    IAltaModificacionDireccionCommand cmdAltaModificacionDireccion,
                                    IGetArbolEntidadTerritorialQuery qryArbolEntidadTerritorial)
        {
            this._logger = logger;
            this._jwtTokenUtils = jwtTokenUtils;
            this._qryAllTiposEntiTerri = qryAllTiposEntiTerri;
            this._qryBuscarEntidades = qryBuscarEntidades;
            _qryAllTiposVias = qryAllTiposVias;
            _qryGetEntidadTerritorial = qryGetEntidadTerritorial;
            _qryBuscadorCallejero = qryBuscadorCallejero;
            _qryDireccionesPersona = qryDireccionesPersona;
            _qryDireccion = qryDireccion;
            _cmdAltaModificacionDireccion = cmdAltaModificacionDireccion;
            _qryArbolEntidadTerritorial = qryArbolEntidadTerritorial;
        }

        /// <summary>
        /// Obtiene todos los tipos de entidades territoriales disponibles en el sistema    
        /// </summary>
        /// <permission cref="">Publico</permission>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("entidad/territorial/tipos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<TipoEntidadTerritorialModel> tipos = await _qryAllTiposEntiTerri.execute();
                return Ok(tipos);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de entidades territoriales");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el arbol de la entidad territorial indicada
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("entidad/territorial/{id:guid}/arbol")]
        public async Task<IActionResult> GetArbolEntidadTerritorial(Guid id)
        {
            try
            {
                EntidadTerritorialModel entidad=await this._qryArbolEntidadTerritorial.execute(id);
                return Ok(entidad);
            } catch (Exception ex) {
                _logger.LogError(ex, "Error al obtener el arbol de la entidad territorial indicada");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los tipos de vías disponibles en el sistema. Este método devuelve una lista de tipos de vías, cada uno con su código y nombre. Los tipos de vías pueden incluir categorías como calles, avenidas, carreteras, etc., que se utilizan para clasificar las diferentes vías en el sistema de direcciones. 
        /// </summary>
        /// <permission cref="">Publico</permission>"
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("vias/tipos")]
        public async Task<IActionResult> GetTiposVias()
        {
            try
            {
                List<TipoViaModel> tipos = await _qryAllTiposVias.execute();
                return Ok(tipos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de vías");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para buscar entidades territoriales según los criterios de búsqueda proporcionados en el filtro. El filtro puede incluir tipos de entidades, padres y nombre. El resultado es una lista de entidades territoriales que coinciden con los criterios de búsqueda.  
        /// </summary>
        /// <param name="filtro"></param>
        /// <permission cref="">Publico</permission>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("entidad/territorial/buscar")]
        public async Task<IActionResult> BuscarEntidades([FromBody] FiltroBusquedaEntidadTerritorialModel filtro)
        {
            try
            {
                List<EntidadTerritorialModel> resultado = await _qryBuscarEntidades.execute(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar entidades territoriales");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de una entidad territorial específica según su ID. El resultado es un objeto que contiene la información detallada de la entidad territorial, incluyendo su nombre, tipo, padre (si tiene) y otros datos relevantes. Este método es útil para obtener información específica de una entidad territorial en particular.    
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("entidad/territorial/{id}")]
        public async Task<IActionResult> GetEntidadTerritorial(Guid id)
        {
            try
            {
                EntidadTerritorialModel resultado = await _qryGetEntidadTerritorial.execute(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la entidad territorial con id {id}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Buscador de vias que permite buscar vías según los criterios de búsqueda proporcionados en el filtro. El filtro puede incluir tipos de vías, nombre, calles superiores e inferiores, y entidades territoriales. El resultado es una lista de vías que coinciden con los criterios de búsqueda. Este método es útil para encontrar vías específicas en el sistema de direcciones.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("vias/buscador")]
        public async Task<IActionResult> BuscadorCallejero([FromBody] FiltroBusquedaCallejeroModel filtro)
        {
            try
            {
                List<CallejeroModel> resultado = await _qryBuscadorCallejero.execute(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al buscar en el callejero con el filtro {filtro}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene las direcciones de una persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("persona/{id:guid}")]
        public async Task<IActionResult> GetDireccionesPersonas(Guid id)
        {
            try
            {
                List<DireccionModel> direcciones = await _qryDireccionesPersona.execute(id);
                return Ok(direcciones);
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Error obtener las direcciones de la persona {id}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de una direccion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetDireccion(Guid id)
        {
            try
            {
                DireccionModel dir = await _qryDireccion.execute(id);
                return Ok(dir);
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la direccion {id}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Data de alta o modifica una direccion, la asignación de esta direccion, se hara con los metodos de asignacion de elementos a personas, facturas, etc...
        /// que se encuentran en los controladores de cada elemento. Este método se encargará únicamente de dar de alta o modificar la dirección en sí, 
        /// sin asignarla a ningún elemento específico. De esta manera, se mantiene una separación clara entre la gestión de direcciones y la asignación de estas a otros elementos del sistema.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> AltaModificacionDireccion([FromBody] DireccionModel model)
        {
            try
            {
                DireccionModel resultado = await _cmdAltaModificacionDireccion.execute(model);
                return Ok(resultado);
            } catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al dar de alta/modificar la dirección");
                return BadRequest(ex.Message);
            }
        }
    }
}
