using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas;
using lfvb.secure.aplication.Database.Censo.Queries.GetPersona;
using lfvb.secure.aplication.Database.Censo.Models;


namespace lfvb.secure.api.Controllers.Censo
{
    [ApiController]
    [Route("api/censo")]
    public class CensoPersonaController : ControllerBase
    {

        private ILogger<PermisosController> _logger;
        private IJwtTokenUtils _jwtTokenUtils;

        private readonly IBuscadorPersonaQuery _buscadorPersonaQuery;
        private readonly IGetPersonaQuery _getPersonaQuery;

        public CensoPersonaController(ILogger<PermisosController> logger, IJwtTokenUtils jwtTokenUtils, IBuscadorPersonaQuery buscadorPersonaQuery, IGetPersonaQuery getPersonaQuery)
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _buscadorPersonaQuery = buscadorPersonaQuery;
            _getPersonaQuery = getPersonaQuery;
        }

        /// <summary>
        /// Obtiene los datos basicos de una persona a partir de su ID. Este endpoint es útil para mostrar información general de la persona sin necesidad de cargar todos los detalles relacionados, como identificaciones, situaciones o relaciones. Se puede utilizar para obtener una vista rápida de la persona en listados o para validar la existencia de una persona antes de realizar operaciones más complejas.
        /// </summary>
        /// <param name="id">El ID de la persona.</param>
        /// <returns>Los datos básicos de la persona.</returns>
        [HttpGet]
        [Authorize]
        [Route("persona/{id:guid}")]
        public async Task<IActionResult> GetPersona(Guid id)
        {
            try
            {
                PersonaModel resultado = await _getPersonaQuery.execute(id);
                if (resultado == null)
                {
                    return NotFound();
                }
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la persona con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Buscador de las personas del censo. Permite buscar personas utilizando diferentes criterios, como nombre, apellidos o identificaciones. Este endpoint es útil para encontrar personas específicas dentro del censo, especialmente cuando se tienen muchos registros y se necesita filtrar la información para obtener resultados relevantes. El filtro de búsqueda puede incluir uno o varios criterios para refinar la búsqueda y obtener resultados más precisos.
        /// </summary>
        /// <param name="filtro">El filtro de búsqueda que contiene los criterios para buscar personas.</param>
        /// <returns>Una lista de personas que coinciden con los criterios de búsqueda. </returns>
        [HttpPost]
        [Authorize]
        [Route("persona/buscar")]
        public async Task<IActionResult> BuscarPersonas([FromBody] FiltroBusquedaPersonasModel filtro)
        {
            try
            {
                List<PersonaModel> resultado = await _buscadorPersonaQuery.execute(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar personas con el filtro {@Filtro}", filtro);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
