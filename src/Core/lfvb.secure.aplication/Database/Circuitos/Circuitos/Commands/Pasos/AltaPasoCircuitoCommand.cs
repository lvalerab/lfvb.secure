using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Paso;
using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.EstadoEsperadoPaso;


namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands.Pasos
{
    public class AltaPasoCircuitoCommand : IAltaPasoCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public AltaPasoCircuitoCommand(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<PasoModel> execute(PasoModel pasoModel)
        {
            if(pasoModel == null)
                throw new ArgumentNullException("El modelo no puede ser nulo");

            if(pasoModel.Circuito == null || pasoModel.Circuito.Id == null)
                throw new ArgumentNullException("El circuito no puede ser nulo");

            ElementoEntity entity = new ElementoEntity
            {
                Id = Guid.NewGuid(),
                CodigoTipoElemento = "paso"
            };

            await _db.Elementos.AddAsync(entity);   

            PasoEntity pasoEntity = new PasoEntity
            {
                Id = entity.Id,
                IdCircuito = pasoModel.Circuito.Id??Guid.Empty,
                CodEstado = pasoModel.Estado.Codigo ,
                CodEstadoSiguiente = pasoModel.EstadoNuevo!=null? pasoModel.EstadoNuevo.Codigo:null ,
                IdCircuitoSiguiente = pasoModel.CircuitoSiguiente!=null? pasoModel.CircuitoSiguiente.Id:null,
                Nombre=pasoModel.Nombre
            };

            await _db.Pasos.AddAsync(pasoEntity);

            //Si tiene estados esperados los agregamos
            if (pasoModel.EstadosEsperados != null && pasoModel.EstadosEsperados.Count > 0)
            {
                foreach (var estadoEsperado in pasoModel.EstadosEsperados)
                {
                    EstadoEsperadoPasoEntity estadoEsperadoEntity = new EstadoEsperadoPasoEntity
                    {
                        IdPaso = pasoEntity.Id,
                        CodTipoElemento = estadoEsperado.TipoElemento.Codigo,
                        CodEstado = estadoEsperado.Estado.Codigo,
                        TipoEstadoEsperado = estadoEsperado.TipoEstadoEsperado
                    };
                    await _db.EstadosEsperadosPasos.AddAsync(estadoEsperadoEntity);
                }
            }

            await _db.SaveAsync();

            pasoModel.Id = entity.Id;

            return pasoModel;
        }
    }
}
