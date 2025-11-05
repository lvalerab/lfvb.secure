using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries
{
    public class GetCircuitosQuery : IGetCircuitosQuery
    {
        private readonly IDataBaseService _db;  
        private readonly IMapper _mapper;   

        public GetCircuitosQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }   


        public async Task<List<CircuitoModel>> execute(FiltroCircuitoModel filtro)
        {
            List<CircuitoModel> circuitos = await (from cr in _db.Circuitos.Include(c=>c.Tramite)
                                                   where cr.Nombre.Contains(filtro.Nombre ?? "")
                                                     && cr.Activo == (filtro.Activo ?? true)
                                                     && ((filtro.IdTramite ?? Guid.Empty) == Guid.Empty || ((filtro.IdTramite ?? Guid.Empty) != Guid.Empty && cr.IdTramite == (filtro.IdTramite ?? Guid.Empty)))
                                                   select new CircuitoModel
                                                   {
                                                       Id=cr.Id,
                                                       Activo=cr.Activo,
                                                       Nombre=cr.Nombre,
                                                       Descripcion=cr.Descripcion,
                                                       Normativa=cr.Normativa,
                                                       Tramite=new Tramites.Models.TramiteModel
                                                       {
                                                           Id=cr.Tramite.Id,
                                                           Nombre=cr.Tramite.Nombre,
                                                           Descripcion=cr.Tramite.Descripcion??""
                                                       },
                                                       FechaAlta=cr.FechaAlta,
                                                       FechaBaja=cr.FechaBaja,
                                                       FechaModificacion=cr.FechaModificacion
                                                   }).ToListAsync<CircuitoModel>();

            return circuitos;
        }
    }
}
