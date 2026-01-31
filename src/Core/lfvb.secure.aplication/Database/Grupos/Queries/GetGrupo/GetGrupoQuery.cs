using AutoMapper;
using lfvb.secure.aplication.Database.Aplicaciones.Models;
using lfvb.secure.aplication.Database.Grupos.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetGrupo
{
    public class GetGrupoQuery : IGetGrupoQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mapper;

        public GetGrupoQuery(IDataBaseService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<GrupoModel?> Execute(Guid grupoId)
        {
            GrupoModel? grupo = await _db.Grupos.Include(p => p.Aplicacion)
                .Where(g => g.Id == grupoId)
                .Select(g => new GrupoModel
                {
                    Id = g.Id,
                    Nombre = g.Nombre,
                    Aplicacion = new AplicacionModel
                    {
                        Id = g.Aplicacion.Id,
                        Codigo = g.Aplicacion.Codigo,
                        Nombre = g.Aplicacion.Nombre
                    }
                })
                .FirstOrDefaultAsync();
            return grupo;
        }
    }
}
