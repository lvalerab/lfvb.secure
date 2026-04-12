using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hydra.comunicaciones.Http
{
    public interface IHttpRestClient
    {
        public void SetBearerToken(bool force = false, string? urlToken = null);

        public Task<T> Get<T>(string url, bool noValidate = false);

        public Task<T> Post<T,P>(string url, P payload, bool noValidate = false);

        public Task<T> Put<T,P>(string url, P payload, bool noValidate = false); 
        
        public Task<T> Delete<T>(string url, bool noValidate = false);
    }
}
