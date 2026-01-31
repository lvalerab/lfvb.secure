using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Circuitos.Circuito;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Commands
{
    public class ModificarCircuitoCommand: IModificacionCircuitoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public ModificarCircuitoCommand(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<CircuitoModel> execute(CircuitoModel model)
        {
            //Lo primero, es buscar el circuito a modificar
            CircuitoEntity? entidad=await (from c in _db.Circuitos
                                                        .Include(p=>p.RelacionTiposElementos)
                                                        .Include(g=>g.GruposAdministradores)
                                           where c.Id == model.Id
                                     select c).FirstOrDefaultAsync();
            if(entidad==null)
            {
                throw new Exception("No se ha encontrado el circuito a modificar");
            } else
            {
                //Ponemos los datos del circuito que se modificar automaticamente
                entidad.FechaModificacion = DateTime.Now;
                entidad.Activo = true;

                //Modificamos los datos del circuito con los del modelo
                entidad.Nombre = model.Nombre;
                entidad.Descripcion = model.Descripcion;
                entidad.Normativa = model.Normativa;

                //Guardamos los cambios
                _db.Circuitos.Update(entidad);

                //Ahora, buscamos los tipos de elementos asociados al circuito y los eliminamos
                foreach(var tipo in entidad.RelacionTiposElementos.ToList())
                {
                    _db.TiposElementosCircuitos.Remove(tipo);
                }   

                foreach(var tipo in model.Tipos)
                {
                    var nuevoTipo = new domain.Entities.Circuitos.TipoElementoCircuito.TipoElementoCircuitoEntity()
                    {
                        CodigoTipoElemento = tipo.Codigo,
                        IdCircuito = entidad.Id
                    };
                    await _db.TiposElementosCircuitos.AddAsync(nuevoTipo);
                }

                //Hacemos lo mismo con los grupos
                foreach(var grupo in entidad.GruposAdministradores.ToList())
                {
                    _db.GruposAdministradoresCircuitos.Remove(grupo);
                }

                foreach(var grupo in model.Grupos)
                {
                    var nuevoGrupo = new domain.Entities.Circuitos.GrupoAdministradorCircuito.GrupoAdministradorCircuitoEntity()
                    {
                        IdGuap = grupo.Id??Guid.Empty,
                        IdCircuito = entidad.Id
                    };
                    await _db.GruposAdministradoresCircuitos.AddAsync(nuevoGrupo);
                }

                await _db.SaveAsync();

                return model;
            }
        }
    }
}
