using lfvb.secure.api.Atributos.Secure;
using lfvb.secure.aplication.Database.Censo.Commands.AgregarIdentificacion;
using lfvb.secure.aplication.Database.Censo.Commands.AltaPersona;
using lfvb.secure.aplication.Database.Censo.Commands.AltaRelacionPersona;
using lfvb.secure.aplication.Database.Censo.Commands.AltaSituacionPersona;
using lfvb.secure.aplication.Database.Censo.Commands.ModificaPersona;
using lfvb.secure.aplication.Database.Censo.Commands.ModificarSituacionPersona;
using lfvb.secure.aplication.Database.Censo.Commands.RelacionarElementoPersona;
using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Database.Censo.Queries.BuscadorPersonas;
using lfvb.secure.aplication.Database.Censo.Queries.GetIdentificadores;
using lfvb.secure.aplication.Database.Censo.Queries.GetPersona;
using lfvb.secure.aplication.Database.Censo.Queries.GetRelaciones;
using lfvb.secure.aplication.Database.Censo.Queries.GetSituaciones;
using lfvb.secure.aplication.Database.Censo.Queries.Maestros;
using lfvb.secure.common.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;


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

        private readonly IGetAllTiposIdentificadoresPersonaQuery _qryMaestrosTiposIdentificadores;
        private readonly IGetAllTipoSituacionPersonaModel _qryMaestrosTiposSituaciones;
        private readonly IGetAllTiposPersonaQuery _qryTiposPersonas;
        private readonly IGetTipoRelacionPersonaQuery _qryTipoRelacionPersonas;
        private readonly IGetAllTipoSexoPersonaQuery _qryTipoSexo;

        private readonly IAltaPersonaCommand _cmdAltaPersona;
        private readonly IModificarPersonaCommand _cmdModificarPersona;


        private readonly IAltaSituacionPersonaCommand _cmdAltaSituacionPersona;
        private readonly IModificarSituacionPersonaCommand _cmdModificarSituacionPersona;

        private readonly IAltaModificacionRelacionPersonaCommand _cmdAltaModificacionRelacionPersona;

        private readonly IAltaModificacionIdentificacionPersonaCommand _cmdAltaModificacionIdentificacionPersona;

        private readonly IRelacionarElementoPersonaCommand _cmdRelacionarElementoPersona;

        private readonly IGetIdentificadoresPersonaQuery _qryGetIdentificadores;
        private readonly IGetRelacionesPersonaQuery _qryGetRelaciones;
        private readonly IGetSituacionesPersonalesQuery _qryGetSituaciones;



        public CensoPersonaController(ILogger<PermisosController> logger,
                                      IJwtTokenUtils jwtTokenUtils,
                                      IBuscadorPersonaQuery buscadorPersonaQuery,
                                      IGetPersonaQuery getPersonaQuery,
                                      IGetAllTiposIdentificadoresPersonaQuery qryMaestrosTiposIdentificadores,
                                      IGetAllTipoSituacionPersonaModel qryMaestrosTiposSituaciones,
                                      IGetAllTiposPersonaQuery qryTiposPersonas,
                                      IGetTipoRelacionPersonaQuery qryTipoRelacionPersonas,
                                      IGetAllTipoSexoPersonaQuery qryTipoSexo,
                                        IAltaPersonaCommand cmdAltaPersona,
                                        IModificarPersonaCommand cmdModificarPersona,
                                        IAltaSituacionPersonaCommand cmdAltaSituacionPersona,
                                        IModificarSituacionPersonaCommand cmdModificarSituacionPersona,
                                        IAltaModificacionRelacionPersonaCommand cmdAltaModificacionRelacionPersona,
                                        IAltaModificacionIdentificacionPersonaCommand cmdAltaModificacionIdentificacionPersona,
                                        IRelacionarElementoPersonaCommand cmdRelacionarElementoPersona,
                                        IGetIdentificadoresPersonaQuery qryGetIdentificadores,
                                        IGetRelacionesPersonaQuery qryGetRelaciones,
                                        IGetSituacionesPersonalesQuery qryGetSituaciones
            )
        {
            _logger = logger;
            _jwtTokenUtils = jwtTokenUtils;
            _buscadorPersonaQuery = buscadorPersonaQuery;
            _getPersonaQuery = getPersonaQuery;
            _qryMaestrosTiposIdentificadores = qryMaestrosTiposIdentificadores;
            _qryMaestrosTiposSituaciones = qryMaestrosTiposSituaciones;
            _qryTiposPersonas = qryTiposPersonas;
            _qryTipoRelacionPersonas = qryTipoRelacionPersonas;
            _qryTipoSexo = qryTipoSexo;


            _cmdAltaPersona = cmdAltaPersona;
            _cmdModificarPersona = cmdModificarPersona;
            _cmdAltaSituacionPersona = cmdAltaSituacionPersona;
            _cmdModificarSituacionPersona = cmdModificarSituacionPersona;
            _cmdAltaModificacionRelacionPersona = cmdAltaModificacionRelacionPersona;
            _cmdAltaModificacionIdentificacionPersona = cmdAltaModificacionIdentificacionPersona;
            _cmdRelacionarElementoPersona = cmdRelacionarElementoPersona;

            _qryGetIdentificadores = qryGetIdentificadores;
            _qryGetRelaciones = qryGetRelaciones;
            _qryGetSituaciones = qryGetSituaciones;
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
        /// Obtiene todas las identificaciones asociadas a una persona específica utilizando su ID. Este endpoint es útil para mostrar o gestionar las diferentes formas de identificación que una persona puede tener, como DNI, pasaporte, número de seguridad social, entre otros. Al obtener esta información, los usuarios pueden verificar o actualizar las identificaciones de la persona en el censo, asegurando que los datos estén completos y actualizados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("persona/{id:guid}/identificaciones")]
        public async Task<IActionResult> GetIdentificacionesPersona(Guid id)
        {
            try
            {
                List<IdentificacionPersonaModel> resultado = await _qryGetIdentificadores.execute(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las identificaciones de la persona con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Da de alta o modifica una identificación de persona utilizando la información proporcionada en el cuerpo de la solicitud. Este endpoint permite agregar una nueva identificación a una persona existente o modificar una identificación existente, dependiendo de si se proporciona un ID de identificación en el modelo. Es importante destacar que este endpoint solo se encarga de las identificaciones asociadas a la persona; para modificar los datos básicos de la persona o sus situaciones y relaciones, se deben utilizar los endpoints específicos para cada uno de esos aspectos. Al utilizar este endpoint, los usuarios pueden mantener la información de las identificaciones de las personas en el censo actualizada y precisa.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("persona/identificacion")]
        public async Task<IActionResult> AgregarIdentificacionPersona([FromBody] IdentificacionPersonaModel model)
        {
            try
            {
                IdentificacionPersonaModel resultado = await _cmdAltaModificacionIdentificacionPersona.execute(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar/modificar la identificacion de la persona con modelo {@Model}",  model);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene todas las situaciones de la persona asociadas a una persona específica utilizando su ID. Este endpoint es útil para mostrar o gestionar las diferentes situaciones que una persona puede tener, como activo, inactivo, fallecido, entre otros. Al obtener esta información, los usuarios pueden verificar o actualizar las situaciones de la persona en el censo, asegurando que los datos estén completos y actualizados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("persona/{id:guid}/situaciones")]
        public async Task<IActionResult> GetSituacionesPersona(Guid id)
        {
            try
            {
                List<SituacionPersonaModel> resultado = await _qryGetSituaciones.execute(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las situaciones de la persona con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Da de alta una nueva situacion de la persona
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("persona/situacion")]
        public async Task<IActionResult> AgregarSituacionPersona([FromBody] SituacionPersonaModel model)
        {
            try
            {
                SituacionPersonaModel resultado = await _cmdAltaSituacionPersona.execute(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la situacion de la persona con modelo {@Model}", model);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Modifica una situacion de la persona existente utilizando la información proporcionada en el cuerpo de la solicitud. Este endpoint permite actualizar una situación de persona existente, dependiendo del ID de situación que se proporcione en el modelo. Es importante destacar que este endpoint solo se encarga de las situaciones asociadas a la persona; para modificar los datos básicos de la persona o sus identificaciones y relaciones, se deben utilizar los endpoints específicos para cada uno de esos aspectos. Al utilizar este endpoint, los usuarios pueden mantener la información de las situaciones de las personas en el censo actualizada y precisa. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("persona/situacion")]
        public async Task<IActionResult> ModificarSituacionPersona([FromBody] SituacionPersonaModel model)
        {
            try
            {
                SituacionPersonaModel resultado = await _cmdModificarSituacionPersona.execute(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la situacion de la persona con modelo {@Model}", model);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene las relaciones entre personas de una persona dada utilizando su ID. Este endpoint es útil para mostrar o gestionar las diferentes relaciones que una persona puede tener con otras personas, como familiar, laboral, social, entre otros. Al obtener esta información, los usuarios pueden verificar o actualizar las relaciones de la persona en el censo, asegurando que los datos estén completos y actualizados. Además, esta información puede ser valiosa para entender el contexto social o laboral de la persona dentro del censo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("persona/{id:guid}/relaciones")]
        public async Task<IActionResult> GetRelacionesPersona(Guid id)
        {
            try
            {
                List<RelacionPersonaModel> resultado = await _qryGetRelaciones.execute(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las relaciones de la persona con id {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Agrega o modifica una relación de persona utilizando la información proporcionada en el cuerpo de la solicitud. Este endpoint permite agregar una nueva relación entre personas o modificar una relación existente, dependiendo de si se proporciona un ID de relación en el modelo. Es importante destacar que este endpoint solo se encarga de las relaciones asociadas a la persona; para modificar los datos básicos de la persona o sus identificaciones y situaciones, se deben utilizar los endpoints específicos para cada uno de esos aspectos. Al utilizar este endpoint, los usuarios pueden mantener la información de las relaciones de las personas en el censo actualizada y precisa.    
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("persona/relacion")]
        public async Task<IActionResult> AgregarRelacionPersona([FromBody] RelacionPersonaModel model)
        {
            try
            {
                RelacionPersonaModel resultado = await _cmdAltaModificacionRelacionPersona.execute(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar/modificar la relacion de la persona con modelo {@Model}", model);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Creates a new person record using the data provided in the request body.    
        /// </summary>
        /// <remarks>Requires authentication. Returns HTTP 201 (Created) with the created person data on
        /// success. Returns HTTP 500 (Internal Server Error) if an unexpected error occurs.</remarks>
        /// <param name="model">The person information to create. Must not be null. All required fields must be populated according to the
        /// API contract.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created person data if successful; otherwise, an error
        /// response.</returns>
        [HttpPost]
        [Authorize]
        [Route("persona")]
        public async Task<IActionResult> AltaPersona([FromBody] PersonaModel model)
        {
            try
            {
                PersonaModel resultado = await _cmdAltaPersona.execute(model,true);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al dar de alta la persona {@Model}", model);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Modifica los datos de una persona existente utilizando la información proporcionada en el cuerpo de la solicitud. Este endpoint permite actualizar los datos básicos de una persona, como nombre, apellidos, fecha de nacimiento, tipo de persona, sexo, entre otros. Es importante destacar que este endpoint no modifica las identificaciones, situaciones o relaciones asociadas a la persona; para eso se deben utilizar los endpoints específicos para cada uno de esos aspectos. Al utilizar este endpoint, los usuarios pueden mantener la información de las personas en el censo actualizada y precisa.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]   
        [Authorize]
        [Route("persona")]
        public async Task<IActionResult> ModificarPersona([FromBody] PersonaModel model)
        {
            try
            {
                PersonaModel resultado = await _cmdModificarPersona.execute(model);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al modificar la persona {@Model}", model);
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

        /// <summary>
        /// Obtiene los diferentes tipos de identificadores de persona que existen en el sistema. Este endpoint es útil para conocer las opciones disponibles al momento de registrar o actualizar la información de una persona, ya que cada tipo de identificador puede tener un formato o significado específico. Por ejemplo, los tipos de identificadores pueden incluir DNI, pasaporte, número de seguridad social, entre otros. Con esta información, los usuarios pueden seleccionar el tipo de identificador adecuado al ingresar los datos de una persona en el censo.    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("maestros/tipos/identificadores")]
        public async Task<IActionResult> GetMaestrosTiposIdentificadores()
        {
            try
            {
                List<TipoIdentificacionPersonaModel> resultado = await _qryMaestrosTiposIdentificadores.execute();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de identificadores de persona");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        /// <summary>
        /// Obtiene los diferentes tipos de situaciones de persona que existen en el sistema. Este endpoint es útil para conocer las opciones disponibles al momento de registrar o actualizar la información de una persona, ya que cada tipo de situación puede tener un significado específico. Por ejemplo, los tipos de situaciones pueden incluir activo, inactivo, fallecido, entre otros. Con esta información, los usuarios pueden seleccionar el tipo de situación adecuado al ingresar los datos de una persona en el censo. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("maestros/tipos/situaciones")]
        public async Task<IActionResult> GetMaestrosTiposSituaciones()
        {
            try
            {
                List<TipoSituacionPersonaModel> resultado = await _qryMaestrosTiposSituaciones.execute();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de situaciones de persona");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene los diferentes tipos de personas que existen en el sistema. Este endpoint es útil para conocer las opciones disponibles al momento de registrar o actualizar la información de una persona, ya que cada tipo de persona puede tener un significado específico. Por ejemplo, los tipos de personas pueden incluir empleado, cliente, proveedor, entre otros. Con esta información, los usuarios pueden seleccionar el tipo de persona adecuado al ingresar los datos de una persona en el censo.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("maestros/tipos/personas")]
        public async Task<IActionResult> GetMaestrosTiposPersonas()
        {
            try
            {
                List<TipoPersonaModel> resultado = await _qryTiposPersonas.execute();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de personas");
                return StatusCode(500, "Error interno del servidor");
            }

        }

        /// <summary>
        /// Obtiene los diferentes tipos de relaciones de persona que existen en el sistema. Este endpoint es útil para conocer las opciones disponibles al momento de registrar o actualizar la información de una persona, ya que cada tipo de relación puede tener un significado específico. Por ejemplo, los tipos de relaciones pueden incluir familiar, laboral, social, entre otros. Con esta información, los usuarios pueden seleccionar el tipo de relación adecuado al ingresar los datos de una persona en el censo.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("maestros/tipos/relaciones")]
        public async Task<IActionResult> GetMaestrosTiposRelaciones()
        {
            try
            {
                List<TipoRelacionPersonaModel> resultado = await _qryTipoRelacionPersonas.execute();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de relaciones de persona");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Obtiene los diferentes tipos de sexo de persona que existen en el sistema. Este endpoint es útil para conocer las opciones disponibles al momento de registrar o actualizar la información de una persona, ya que cada tipo de sexo puede tener un significado específico. Por ejemplo, los tipos de sexo pueden incluir masculino, femenino, no binario, entre otros. Con esta información, los usuarios pueden seleccionar el tipo de sexo adecuado al ingresar los datos de una persona en el censo.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("maestros/tipos/sexo")]
        public async Task<IActionResult> GetMaestrosTiposSexo()
        {
            try
            {
                List<TipoSexoPersonaModel> resultado = await _qryTipoSexo.execute();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de sexo de persona");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
