using AutoMapper;
using lfvb.secure.aplication.Database.UnidadesOrganizativas.Models;
using lfvb.secure.aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.UnidadesOrganizativas.Queries.Unidades
{
    public class GetArbolUnidadesOrganizativasQuery : IGetArbolUnidadesOrganizativasQuery
    {
        private readonly IDataBaseService _db;
        private readonly IMapper _mp;

        public GetArbolUnidadesOrganizativasQuery(IDataBaseService db, IMapper mp)
        {
            _db = db;
            _mp = mp;
        }

        public async Task<List<UnidadOrganizativaModel>> execute(Guid? codPadre = null, Guid? Tipo = null, int nivelMax = 999, int nivelActual = 1)
        {
            List<UnidadOrganizativaModel> unidades = await (from uo in _db.UnidadesOrganizativas.Include(uo => uo.TipoUnidadOrganizativa)
                                                            where (codPadre == null ||
                                                                   (codPadre != null && uo.CodUnorPadre == codPadre))
                                                         && (Tipo == null || uo.CodTuno == Tipo)
                                                            select new UnidadOrganizativaModel
                                                            {
                                                                Codigo = uo.Codigo,
                                                                Nombre = uo.Nombre,
                                                                TipoUnidadOrganizativa = new TipoUnidadOrganizativaModel
                                                                {
                                                                    Codigo = uo.TipoUnidadOrganizativa.Codigo,
                                                                    Nombre = uo.TipoUnidadOrganizativa.Nombre,
                                                                    Descripcion = uo.TipoUnidadOrganizativa.Descripcion
                                                                },
                                                                Unidades = new List<UnidadOrganizativaModel>()
                                                            }).ToListAsync();
            if (nivelActual < nivelMax)
            {
                foreach (UnidadOrganizativaModel uo in unidades)
                {
                    uo.Unidades = await execute(uo.Codigo, Tipo, nivelMax, nivelActual + 1);
                }
            }
            else
            {
                foreach (UnidadOrganizativaModel uo in unidades)
                {
                    uo.Unidades = new List<UnidadOrganizativaModel>();
                }
            }

            return unidades;
        }
    }
}
