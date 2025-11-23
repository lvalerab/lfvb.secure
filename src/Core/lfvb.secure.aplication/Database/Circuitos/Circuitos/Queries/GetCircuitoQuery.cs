using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Database.Circuitos.Tramites.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.Circuitos.Queries
{
    public class GetCircuitoQuery: IGetCircuitoQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetCircuitoQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<CircuitoModel?> execute(Guid idCircuito)
        {
            CircuitoModel? circuito= await (from c in _db.Circuitos.Include(c=>c.Tramite)
                                           where c.Id == idCircuito
                                           select new CircuitoModel
                                           {
                                                Id = c.Id,
                                                Nombre = c.Nombre,
                                                Tramite = new TramiteModel
                                                {
                                                   Id=c.Tramite.Id,
                                                   Nombre=c.Tramite.Nombre,
                                                   Descripcion=c.Tramite.Descripcion,
                                                   Normativa=c.Tramite.Normativa
                                                },
                                                Descripcion = c.Descripcion,
                                                Normativa = c.Normativa,
                                                Activo = c.Activo,
                                                FechaAlta = c.FechaAlta,
                                                FechaModificacion = c.FechaModificacion,
                                                FechaBaja = c.FechaBaja
                                           }).FirstOrDefaultAsync();

            return circuito;
        }
    }
}
