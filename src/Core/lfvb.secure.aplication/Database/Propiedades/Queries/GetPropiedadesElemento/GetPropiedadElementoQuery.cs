using AutoMapper;
using lfvb.secure.aplication.Database.Propiedades.Queries.GetAllPropiedades;
using lfvb.secure.aplication.Database.TipoPropiedad.Queries;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Propiedades.Queries.GetPropiedadesElemento
{
    public class GetPropiedadElementoQuery : IGetPropiedadesElementoQuery
    {
        private readonly IMapper _mapper;
        private readonly IDataBaseService _db;
        public GetPropiedadElementoQuery(IDataBaseService db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }


        public async Task<List<PropiedadElementoModel>> Execute(Guid idElemento, string? codigos=null)
        {
            List<string> prmcodigos = new List<string>();
            if(codigos!=null)
            {
                prmcodigos = codigos.Split(',').ToList();
            }
            List<PropiedadElementoModel> propiedades=await (from p in _db.PropiedadesElementos
                                                            where p.IdElemento.Equals(idElemento)
                                                            && p.Activo.Equals("S")
                                                            && (prmcodigos.Count==0 || (prmcodigos.Count>0 && prmcodigos.Contains(p.Propiedad.Codigo)))
                                                            select new PropiedadElementoModel
                                                            {
                                                                Id = p.Id,
                                                                IdElemento = p.IdElemento,
                                                                Propiedad = new PropiedadModel
                                                                {
                                                                    Codigo = p.Propiedad.Codigo,
                                                                    Nombre = p.Propiedad.Nombre,
                                                                    TipoPropiedad = new TipoPropiedadModel
                                                                    {
                                                                        Codigo = p.Propiedad.TipoPropiedad.Codigo,
                                                                        Nombre = p.Propiedad.TipoPropiedad.Nombre,
                                                                        Historico = p.Propiedad.TipoPropiedad.Historico.Equals("S"),
                                                                        Intervalo = p.Propiedad.TipoPropiedad.Intervalo.Equals("S"),
                                                                        ListaValores = p.Propiedad.TipoPropiedad.ListaValores.Equals("S"),
                                                                        Multiple = p.Propiedad.TipoPropiedad.Multiple.Equals("S"),
                                                                        Tipo = p.Propiedad.TipoPropiedad.Tipo
                                                                    }
                                                                },
                                                                FechaValor = p.FechaValor,
                                                                Activo = p.Activo.Equals("S")
                                                            }).ToListAsync<PropiedadElementoModel>();

            foreach (PropiedadElementoModel prop in propiedades)
            {
                prop.Valores = await (from v in _db.ValoresPropiedadesElementos
                                      where v.IdPropiedadElemento == prop.Id
                                      select new ValorPropiedadModel
                                      {
                                          Id = v.Id,
                                          IdPropiedadElemento = v.IdPropiedadElemento,
                                          Texto = v.Texto,
                                          Numero = v.Numerico,
                                          Fecha = v.Fecha,
                                          Bool = v.Booleano.Equals("S"),
                                          NumeroMaximo = v.NumericoMaximo,
                                          FechaMaxima = v.FechaMaximo
                                      }).ToListAsync<ValorPropiedadModel>();
            }

            return propiedades;
        }

        public async Task<List<PropiedadElementoModel>> Execute(List<Guid?> idElementos)
        {
            List<PropiedadElementoModel> propiedades = await(from p in _db.PropiedadesElementos
                                                             where idElementos.Contains(p.IdElemento)
                                                             && p.Activo.Equals("S")
                                                             select new PropiedadElementoModel
                                                             {
                                                                 Id = p.Id,
                                                                 IdElemento = p.IdElemento,
                                                                 Propiedad = new PropiedadModel
                                                                 {
                                                                     Codigo = p.Propiedad.Codigo,
                                                                     Nombre = p.Propiedad.Nombre,
                                                                     TipoPropiedad = new TipoPropiedadModel
                                                                     {
                                                                         Codigo = p.Propiedad.TipoPropiedad.Codigo,
                                                                         Nombre = p.Propiedad.TipoPropiedad.Nombre,
                                                                         Historico = p.Propiedad.TipoPropiedad.Historico.Equals("S"),
                                                                         Intervalo = p.Propiedad.TipoPropiedad.Intervalo.Equals("S"),
                                                                         ListaValores = p.Propiedad.TipoPropiedad.ListaValores.Equals("S"),
                                                                         Multiple = p.Propiedad.TipoPropiedad.Multiple.Equals("S"),
                                                                         Tipo = p.Propiedad.TipoPropiedad.Tipo
                                                                     }
                                                                 },
                                                                 FechaValor = p.FechaValor,
                                                                 Activo = p.Activo.Equals("S")
                                                            }).ToListAsync<PropiedadElementoModel>();
            foreach(PropiedadElementoModel prop in propiedades)
            {
                 prop.Valores = await (from v in _db.ValoresPropiedadesElementos
                                    where v.IdPropiedadElemento == prop.Id
                                    select new ValorPropiedadModel
                                    {
                                        Id = v.Id,
                                        IdPropiedadElemento= v.IdPropiedadElemento,
                                        Texto = v.Texto,
                                        Numero=v.Numerico,
                                        Fecha=v.Fecha,
                                        Bool=v.Booleano.Equals("S"),
                                        NumeroMaximo=v.NumericoMaximo,
                                        FechaMaxima=v.FechaMaximo
                                      }).ToListAsync<ValorPropiedadModel>();
            }

            return propiedades;
        }

        public async Task<List<PropiedadElementoModel>> Execute(List<Guid?> idElementos, List<string> CodigoPropiedad)
        {
            List<PropiedadElementoModel> propiedades = await(from p in _db.PropiedadesElementos
                                                             where idElementos.Contains(p.IdElemento)
                                                                && CodigoPropiedad.Contains(p.Propiedad.Codigo)
                                                                && p.Activo.Equals("S")
                                                             select new PropiedadElementoModel
                                                             {
                                                                 Id = p.Id,
                                                                 IdElemento = p.IdElemento,
                                                                 Propiedad = new PropiedadModel
                                                                 {
                                                                     Codigo = p.Propiedad.Codigo,
                                                                     Nombre = p.Propiedad.Nombre,
                                                                     TipoPropiedad = new TipoPropiedadModel
                                                                     {
                                                                         Codigo = p.Propiedad.TipoPropiedad.Codigo,
                                                                         Nombre = p.Propiedad.TipoPropiedad.Nombre,
                                                                         Historico = p.Propiedad.TipoPropiedad.Historico.Equals("S"),
                                                                         Intervalo = p.Propiedad.TipoPropiedad.Intervalo.Equals("S"),
                                                                         ListaValores = p.Propiedad.TipoPropiedad.ListaValores.Equals("S"),
                                                                         Multiple = p.Propiedad.TipoPropiedad.Multiple.Equals("S"),
                                                                         Tipo = p.Propiedad.TipoPropiedad.Tipo
                                                                     }
                                                                 },
                                                                 FechaValor = p.FechaValor,
                                                                 Activo = p.Activo.Equals("S")
                                                             }).ToListAsync<PropiedadElementoModel>();

            foreach (PropiedadElementoModel prop in propiedades)
            {
                prop.Valores = await (from v in _db.ValoresPropiedadesElementos
                                      where v.IdPropiedadElemento == prop.Id
                                      select new ValorPropiedadModel
                                      {
                                          Id = v.Id,
                                          IdPropiedadElemento = v.IdPropiedadElemento,
                                          Texto = v.Texto,
                                          Numero = v.Numerico,
                                          Fecha = v.Fecha,
                                          Bool = v.Booleano.Equals("S"),
                                          NumeroMaximo = v.NumericoMaximo,
                                          FechaMaxima = v.FechaMaximo
                                      }).ToListAsync<ValorPropiedadModel>();
            }

            return propiedades;
        }
    }
}
