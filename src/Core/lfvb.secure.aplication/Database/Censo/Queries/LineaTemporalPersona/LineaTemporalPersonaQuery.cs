using lfvb.secure.aplication.Database.Censo.Models;
using lfvb.secure.aplication.Database.Censo.Queries.GetRelaciones;
using lfvb.secure.aplication.Database.Censo.Queries.GetSituaciones;
using lfvb.secure.aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Censo.Queries.LineaTemporalPersona
{
    public class LineaTemporalPersonaQuery: ILineaTemporalPersonaQuery
    {
        private readonly IDataBaseService _db;
        private IGetSituacionesPersonalesQuery _getSituacionesPersonalesQuery;
        private IGetRelacionesPersonaQuery _getRelacionesPersonaQuery;
        public LineaTemporalPersonaQuery(IDataBaseService db, IGetSituacionesPersonalesQuery getSituacionesPersonalesQuery, IGetRelacionesPersonaQuery getRelacionesPersonaQuery)
        {
            _db = db;
            _getSituacionesPersonalesQuery = getSituacionesPersonalesQuery;
            _getRelacionesPersonaQuery = getRelacionesPersonaQuery;
        }

        public async Task<List<EntradaTiempoPersonaModel>> execute(Guid id)
        {
            //Obtenemos el listado de situaciones de la persona
            List<SituacionPersonaModel> situaciones= await _getSituacionesPersonalesQuery.execute(id);
            //Obtenemos el listado de relaciones de la persona
            List<RelacionPersonaModel> relaciones = await _getRelacionesPersonaQuery.execute(id);   

            List<EntradaTiempoPersonaModel> linea = new List<EntradaTiempoPersonaModel>();

            //Convertimos las situaciones a entradas de tiempo
            foreach(SituacionPersonaModel situacion in situaciones)
            {
                linea.Add(new EntradaTiempoPersonaModel
                {
                    Fecha = situacion.FechaInicio ?? DateTime.MinValue,
                    Titulo = situacion.Tipo.Nombre,
                    Situacion = situacion
                });
            }
            
            //Convertimos las relaciones a entradas de tiempo
            foreach(RelacionPersonaModel relacion in relaciones)
            {
                string nombreRelacion = relacion.Persona2.Tipo.Codigo != "F" ? relacion.Persona2.Nombre : (relacion.Persona2.Apellido1 + " " + relacion.Persona2.Apellido2 + ", " + relacion.Persona2.Nombre);
                linea.Add(new EntradaTiempoPersonaModel
                {
                    Fecha = relacion.FechaInicioVigencia ?? DateTime.MinValue,
                    Titulo = $"Inicio de tipo de relación de {relacion.Tipo.Nombre} con {nombreRelacion}",
                    Relacion = relacion
                });
                if(relacion.FechaFinVigencia.HasValue)
                {
                    linea.Add(new EntradaTiempoPersonaModel
                    {
                        Fecha = relacion.FechaFinVigencia.Value,
                        Titulo = $"Inicio de tipo de relación de {relacion.Tipo.Nombre} con {nombreRelacion}",
                        Relacion = relacion
                    });
                }
            }

            return linea.OrderBy(e => e.Fecha).ToList();

        }
    }
}
