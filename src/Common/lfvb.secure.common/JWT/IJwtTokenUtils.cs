using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.common.JWT
{
    public interface IJwtTokenUtils
    {
        public string GetToken(string id, string secret, int expiresInMinutes = 5);
        public Guid? GetIdFromToken(HttpContext contexto);
    }
}
