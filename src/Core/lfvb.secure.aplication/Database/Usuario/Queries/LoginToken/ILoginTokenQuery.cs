using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.aplication.Database.Usuario.Queries.LoginToken
{
    public interface ILoginTokenQuery
    {
        public Task<LoginTokenModel> Execute(LoginTokenModel parameters);
    }
}
