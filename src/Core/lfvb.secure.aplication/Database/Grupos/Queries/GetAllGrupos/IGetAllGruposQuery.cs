using lfvb.secure.aplication.Database.Grupos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Grupos.Queries.GetAllGrupos
{
    public interface IGetAllGruposQuery
    {
        public Task<List<GrupoModel>> Execute();    
    }
}
