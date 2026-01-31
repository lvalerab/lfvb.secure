using AutoMapper;
using lfvb.secure.aplication.Database.Circuitos.Acciones.Models;
using lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Models;
using lfvb.secure.aplication.Database.Circuitos.Circuitos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Circuitos.AccionesPasos.Queries
{
    public class GetAccionesPasoQuery : IGetAccionesPasoQuery
    {
        private readonly IDataBaseService _db;  
        private readonly IMapper _mp;

        public GetAccionesPasoQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<List<AccionPasoModel>> execute(Guid idPaso, string tipo = "")
        {
            List<AccionPasoModel> accionesPaso = await (from ap in _db.PasosAcciones
                                                                .Include(ap=> ap.Paso)
                                                                .Include(ap=> ap.Accion)
                                                                .Include(ap=> ap.CircuitoError) 
                                                  where ap.IdPaso == idPaso
                                                    && (tipo == "" || ap.TipoEjecucion == tipo)
                                                        select new AccionPasoModel
                                                 {
                                                     Id = ap.Id,
                                                     Paso = new PasoModel
                                                     {
                                                         Id = ap.Paso.Id,
                                                         Nombre = ap.Paso.Nombre
                                                     },
                                                     TipoEjecucion = ap.TipoEjecucion,
                                                     Orden = ap.Orden,
                                                     Accion = new AccionModel
                                                     {
                                                         Id = ap.Accion.Id,
                                                         Nombre = ap.Accion.Nombre
                                                     },
                                                     CircuitoError = ap.CircuitoError != null ? new CircuitoModel
                                                     {
                                                         Id = ap.CircuitoError.Id,
                                                         Nombre = ap.CircuitoError.Nombre
                                                     } : null
                                                 }).ToListAsync();
            return accionesPaso;
        }
    }
}
