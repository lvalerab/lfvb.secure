using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Elemento;
using lfvb.secure.domain.Entities.ElementoAplicacion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Commands.AltaActualizacionElementoAplicacion
{
    public class AltaActualizacionElementoAplicacionCommand : IAltaActualizacionElementoAplicacionCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public AltaActualizacionElementoAplicacionCommand(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }

        public async Task<ElementoAplicacionModel> Execute(ElementoAplicacionModel elemento)
        {
            ElementoAplicacionEntity entidad = null;
            if (elemento.Id==null)
            {
                //Es nuevo
                entidad= new ElementoAplicacionEntity
                {
                    Id= Guid.NewGuid(),
                    Codigo = elemento.Codigo,
                    Nombre = elemento.Nombre,
                    IdPadre = (elemento.Padre != null ? elemento.Padre.Id : null),
                    IdAplicacion= elemento.Aplicacion.Id,
                    CodigoTipoElemento = elemento.TipoElemento.Codigo
                };

                //Comprobamos que no exista ya un elemento con el mismo codigo para la misma aplicacion
                int count = await _db.ElementosAplicaciones.CountAsync(x => x.Codigo == entidad.Codigo && x.IdAplicacion == entidad.IdAplicacion);
                if(count > 0)
                {
                    throw new Exception("[DATAFIELD] Ya existe un elemento de aplicacion con el mismo codigo para la misma aplicacion");
                } else
                {
                    _db.ElementosAplicaciones.Add(entidad);
                }
                //Guardamos el elemento en la tabla de elementos
                ElementoEntity elem = new ElementoEntity
                {
                    Id = entidad.Id,
                    CodigoTipoElemento = "elpl"
                };
                await _db.Elementos.AddAsync(elem);
            } else
            {
                //Es una actualizacion
                entidad = await _db.ElementosAplicaciones.FirstOrDefaultAsync(x => x.Id == elemento.Id);
                if (entidad == null)
                {
                    throw new Exception("[DATA] No se ha encontrado el elemento de aplicacion indicado");
                }
                //Actualizamos los datos
                entidad.Nombre = elemento.Nombre;
                entidad.CodigoTipoElemento = elemento.TipoElemento.Codigo;
                entidad.IdPadre=(elemento.Padre!=null?elemento.Padre.Id:null);
                _db.ElementosAplicaciones.Update(entidad);                        
            }
            await _db.SaveAsync();
            elemento.Id = entidad.Id;
            return elemento;
        }
    }
}
