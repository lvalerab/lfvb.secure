using AutoMapper;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento;
using lfvb.secure.aplication.Interfaces;
using lfvb.secure.domain.Entities.Propiedad;
using lfvb.secure.domain.Entities.PropiedadElemento;
using lfvb.secure.domain.Entities.ValorPropiedadElemento;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Commands.NuevaPropiedadElemento
{
    public class NuevaActualizaPropiedadElementoCommand : INuevaActualizaPropiedadElementoCommand
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public NuevaActualizaPropiedadElementoCommand(IDataBaseService db, IMapper mp)
        {
            this._db = db;
            this._mapper = mp;
        }

        public async Task<PropiedadElementoModel> Execute(PropiedadElementoModel propiedad)
        {
            if (propiedad != null)
            {
                PropiedadElementoEntity entity = new PropiedadElementoEntity
                {
                    IdElemento = propiedad.IdElemento,
                    Activo = (propiedad.Activo ?? false) ? "S" : "N",
                    FechaValor = DateTime.Now,
                    CodPropiedad = propiedad.Propiedad.Codigo
                };

                if (propiedad.Propiedad.TipoPropiedad.Historico)
                {
                    //Si es historico, desactivamos el resto de propiedades y siempre damos de alta una nueva propiedad //aunque tenga el ID
                    List<PropiedadElementoEntity> propiedades = await this._db.PropiedadesElementos.Where(x => x.IdElemento == propiedad.IdElemento && x.CodPropiedad.Equals(propiedad.Propiedad.Codigo)).ToListAsync<PropiedadElementoEntity>();
                    foreach (var prop in propiedades)
                    {
                        prop.Activo = "N";
                        this._db.PropiedadesElementos.Update(prop);
                    }
                } else { 
                    if (propiedad.Id == null)
                    {
                        await this._db.PropiedadesElementos.AddAsync(entity);
                    }
                    else
                    {
                        entity.Id = propiedad.Id ?? 0;
                        PropiedadElementoEntity anterior = await this._db.PropiedadesElementos.Where(x => x.Id == entity.Id).FirstOrDefaultAsync<PropiedadElementoEntity>();
                        if (anterior != null)
                        {
                            entity.Elemento = anterior.Elemento;
                            entity.Valores = anterior.Valores;
                            entity.Propiedad = anterior.Propiedad;
                            entity.IdElemento = anterior.IdElemento;
                            entity.Activo = anterior.Activo;
                            entity.FechaValor = anterior.FechaValor;
                            entity.CodPropiedad = anterior.CodPropiedad;
                        }
                        this._db.PropiedadesElementos.Update(entity);
                    }
                }
                propiedad.Id = entity.Id;
                if (propiedad.Valores != null && propiedad.Valores.Count > 0)
                {
                    foreach (var valor in propiedad.Valores)
                    {
                        if(valor.Id==null || (valor.IdPropiedadElemento!=entity.Id))
                        {
                            ValorPropiedadElementoEntity nuevov=new ValorPropiedadElementoEntity
                            {
                                IdPropiedadElemento=entity.Id,
                                Numerico=valor.Numero,
                                NumericoMaximo=valor.NumeroMaximo,
                                Fecha=valor.Fecha,
                                FechaMaximo=valor.FechaMaxima,
                                Texto=valor.Texto,
                                Booleano=valor.Bool??false?"S": "N"
                            };
                            await this._db.ValoresPropiedadesElementos.AddAsync(nuevov);
                            valor.Id = nuevov.Id;
                        } else
                        {
                            ValorPropiedadElementoEntity anterior = await this._db.ValoresPropiedadesElementos.Where(x => x.Id == valor.Id).FirstOrDefaultAsync<ValorPropiedadElementoEntity>();
                            if(anterior != null)
                            {
                                anterior.Numerico = valor.Numero;
                                anterior.NumericoMaximo = valor.NumeroMaximo;
                                anterior.Fecha = valor.Fecha;
                                anterior.FechaMaximo = valor.FechaMaxima;
                                anterior.Texto = valor.Texto;
                                anterior.Booleano = valor.Bool ?? false ? "S" : "N";
                                this._db.ValoresPropiedadesElementos.Update(anterior);
                            }
                        }
                    }
                }
            }
            return propiedad;
        }
    }
}
