using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Aplicaciones.Queries.GetArbolElementosAplicacion
{
    public class GetArbolElementosAplicacion : IGetArbolElementosAplicacion
    {
        private IDataBaseService _db;
        private IMapper _mapper;
        public GetArbolElementosAplicacion(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<List<ElementoAplicacionModel>> Execute(Guid idAplicacion, Guid? idElementoPadre = null)
        {
            List<ElementoAplicacionModel> elementos = await (from ea in _db.ElementosAplicaciones.Include(p=>p.TipoElementoAplicacion)
                                                                                           .Include(p => p.Aplicacion)
                                                       where ea.IdAplicacion == idAplicacion &&
                                                                (idElementoPadre == null || ea.IdPadre == idElementoPadre)
                                                          select new ElementoAplicacionModel
                                                          {
                                                             Id=ea.Id,
                                                             Aplicacion=new AplicacionModel
                                                             {
                                                                 Id=ea.Aplicacion.Id,
                                                                 Codigo=ea.Aplicacion.Codigo,
                                                                 Nombre=ea.Aplicacion.Nombre 
                                                             },
                                                             TipoElemento=new TipoElementoAplicacionModel
                                                             {
                                                                Codigo=ea.TipoElementoAplicacion.Codigo,
                                                                 Nombre=ea.TipoElementoAplicacion.Nombre
                                                             },
                                                             Nombre=ea.Nombre,
                                                             Elementos = new List<ElementoAplicacionModel>()
                                                          }).ToListAsync<ElementoAplicacionModel>();
            foreach (var elemento in elementos)
            {
                elemento.Elementos = await Execute(idAplicacion, elemento.Id);
            }
            return elementos;
        }
    }
}
