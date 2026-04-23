using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace hydra.comunicaciones.Http
{
    public class HttpRestClient:IHttpRestClient
    {
        private string? _tokenUser = null; //Es lo unico que guardamos del estado del cliente, el token de usuario,
                                         //para poder identificarlo en cada petición y asociarlo a un usuario concreto.
                                         //No guardamos nada más, ni siquiera la IP, para respetar la privacidad del usuario.
        private string? _bearerToken = null; //El token de autenticación, que se usará para autenticar las peticiones al servidor de comandos.


        /// <summary>
        /// Para la inyeccion de dependencias
        /// </summary>
        private IConfiguration _config;
        private ILogger<HttpRestClient> _logger;

        private HttpClient _client=new HttpClient();


        public HttpRestClient(IConfiguration config, ILogger<HttpRestClient> logger)
        {
            _config = config;
            _logger = logger;
            this._tokenUser = this._config.GetValue<string>("auth:token");
        }

        public virtual async void SetBearerToken(bool force=false, string? urlToken=null)
        {   
            if(string.IsNullOrEmpty(this._bearerToken) || force)
            {
                //Hacemos la logica para obtener el token de autenticación, por ejemplo, podríamos hacer una petición al servidor de comandos para obtener un token de acceso.
                string url= urlToken??this._config.GetValue<string>("Conexiones:sw:url") + this._config.GetValue<string>("Conexiones:sw:rutas:user:token");
                this._bearerToken = await this.Post<string,string>(url, this._tokenUser.ToString(),true);
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this._bearerToken);
        }

        public void SetDefaultHeaders()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }   

        /// <summary>
        /// Método genérico para hacer peticiones GET al servidor de comandos, con manejo de autenticación y errores.   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<R> Get<R>(string url,bool noValidate=false)
        {
            try
            {
                _logger.LogInformation("Realizando petición POST a {url}", url);
                if (!noValidate)
                    this.SetBearerToken();
                this.SetDefaultHeaders();
                var result = _client.GetAsync(url);
                if (result.Result.IsSuccessStatusCode)
                {
                    return result.Result.Content.ReadFromJsonAsync<R>();
                }
                else
                {
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized && !noValidate)
                    {
                        //Si el token ha expirado o es inválido, lo volvemos a obtener y reintentamos la petición una sola vez.
                        this.SetBearerToken(true);
                        result = _client.GetAsync(url);
                        if (result.Result.IsSuccessStatusCode)
                        {
                            return result.Result.Content.ReadFromJsonAsync<R>();
                        }
                        else
                        {
                            _logger.LogError("Error en la petición GET a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                            return Task.FromResult(default(R));
                        }
                    }
                    else
                    {
                        _logger.LogError("Error en la petición GET a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                        return Task.FromResult(default(R));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar la petición GET a {url}", url);
                return Task.FromResult(default(R));
            }
        }

        /// <summary>
        /// Metodo generico para hacer peticiones POST al servidor de comandos, con manejo de autenticación y errores.  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public Task<T> Post<T,P>(string url, P payload, bool noValidate = false)
        {
            try {                 
                _logger.LogInformation("Realizando petición POST a {url} con payload: {@payload}", url, payload);
                if (!noValidate)
                    this.SetBearerToken();
                this.SetDefaultHeaders();
                var result=_client.PostAsJsonAsync(url, payload);
                if(result.Result.IsSuccessStatusCode)
                {
                    return result.Result.Content.ReadFromJsonAsync<T>();
                } else
                {
                    if(result.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized && !noValidate)
                    {
                        //Si el token ha expirado o es inválido, lo volvemos a obtener y reintentamos la petición una sola vez.
                        this.SetBearerToken(true);
                        result=_client.PostAsJsonAsync(url, payload);
                        if(result.Result.IsSuccessStatusCode)
                        {
                            return result.Result.Content.ReadFromJsonAsync<T>();
                        } else
                        {
                            _logger.LogError("Error en la petición POST a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                            return Task.FromResult(default(T));
                        }
                    } else
                    {
                        _logger.LogError("Error en la petición POST a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                        return Task.FromResult(default(T));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errorl al realizar la petición POST a {url}", url);
                return Task.FromResult(default(T)); 
            }
        }


        /// <summary>
        /// Metodo generico para hacer peticiones PUT al servidor de comandos, con manejo de autenticación y errores.   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public Task<T> Put<T, P>(string url, P payload, bool noValidate = false)
        {
            try
            {
                _logger.LogInformation("Realizando petición PUT a {url} con payload: {@payload}", url, payload);
                if (!noValidate)
                    this.SetBearerToken();
                this.SetDefaultHeaders();
                var result = _client.PutAsJsonAsync(url, payload);
                if (result.Result.IsSuccessStatusCode)
                {
                    return result.Result.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized && !noValidate)
                    {
                        //Si el token ha expirado o es inválido, lo volvemos a obtener y reintentamos la petición una sola vez.
                        this.SetBearerToken(true);
                        result = _client.PutAsJsonAsync(url, payload);
                        if (result.Result.IsSuccessStatusCode)
                        {
                            return result.Result.Content.ReadFromJsonAsync<T>();
                        }
                        else
                        {
                            _logger.LogError("Error en la petición PUT a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                            return Task.FromResult(default(T));
                        }
                    }
                    else
                    {
                        _logger.LogError("Error en la petición PUT a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                        return Task.FromResult(default(T));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar la petición PUT a {url}", url);
                return Task.FromResult(default(T));
            }
        }

        /// <summary>
        /// Metodo generico para hacer peticiones DELETE al servidor de comandos, con manejo de autenticación y errores.    
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<T> Delete<T>(string url, bool noValidate = false)
        {
            try
            {
                _logger.LogInformation("Realizando petición DELETE a {url}", url);

                if (!noValidate)
                    this.SetBearerToken();
                this.SetDefaultHeaders();
                var result = _client.DeleteAsync(url);
                if (result.Result.IsSuccessStatusCode)
                {
                    return result.Result.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    if (result.Result.StatusCode == System.Net.HttpStatusCode.Unauthorized && !noValidate)
                    {
                        //Si el token ha expirado o es inválido, lo volvemos a obtener y reintentamos la petición una sola vez.
                        this.SetBearerToken(true);
                        result = _client.DeleteAsync(url);
                        if (result.Result.IsSuccessStatusCode)
                        {
                            return result.Result.Content.ReadFromJsonAsync<T>();
                        }
                        else
                        {
                            _logger.LogError("Error en la petición DELETE a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                            return Task.FromResult(default(T));
                        }
                    }
                    else
                    {
                        _logger.LogError("Error en la petición DELETE a {url}: {statusCode} - {reasonPhrase}", url, result.Result.StatusCode, result.Result.ReasonPhrase);
                        return Task.FromResult(default(T));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar la petición DELETE a {url}", url);
                return Task.FromResult(default(T));
            }
        }
    }
}
