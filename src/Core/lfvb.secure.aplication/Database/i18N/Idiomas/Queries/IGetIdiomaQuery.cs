using lfvb.secure.aplication.Database.i18N.Idiomas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.i18N.Idiomas.Queries
{
    public interface IGetIdiomaQuery
    {
        public Task<IdiomaModel?> execute(string codigo); 
    }
}
