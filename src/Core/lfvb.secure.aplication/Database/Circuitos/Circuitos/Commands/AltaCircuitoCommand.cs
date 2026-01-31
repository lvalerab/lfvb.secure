using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Circuito;
using lfvb.secure.domain.Entities.Circuitos.GrupoAdministradorCircuito;
using lfvb.secure.domain.Entities.Circuitos.TipoElementoCircuito;
using lfvb.secure.domain.Entities.Elemento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands
{
    public class AltaCircuitoCommand : IAltaCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public AltaCircuitoCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<AltaCircuitoModel> Execute(AltaCircuitoModel model)
        {

            ElementoEntity elem = new ElementoEntity
            {
                CodigoTipoElemento="circ"
            };

            await _db.Elementos.AddAsync(elem);


            CircuitoEntity entity = new CircuitoEntity
            {
                Id=elem.Id,
                IdTramite = model.Tramite.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,    
                Normativa = model.normativa,
                FechaAlta = DateTime.UtcNow,
                FechaModificacion = DateTime.UtcNow,
                Activo = true
            };
            await _db.Circuitos.AddAsync(entity);

            //Agregar los tipos de elementos asociados al circuito
            foreach (var tipo in model.Tipos)
            {
                var relacion = new TipoElementoCircuitoEntity
                {
                    IdCircuito = entity.Id,
                    CodigoTipoElemento = tipo.Codigo
                };
                await _db.TiposElementosCircuitos.AddAsync(relacion);
            }

            //Agregamos los grupos que pueden administrar dicho circuito
            foreach (var g in model.Grupos)
            {
                var relacion=new GrupoAdministradorCircuitoEntity                 {
                    IdCircuito=entity.Id,
                    IdGuap=g.Id??Guid.Empty
                };
                await _db.GruposAdministradoresCircuitos.AddAsync(relacion);
            }

            await _db.SaveAsync();

            model.Id = entity.Id;

            return model;

        }
    }
}
