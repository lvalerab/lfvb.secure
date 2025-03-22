using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace lfvb.secure.api
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// Añade las dependecias para usar los token JWT
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            string? secret = configuration.GetSection("jwt")?.GetValue<string>("secret")??"$$$_DEVELOP_SERVICE_$$$";
            byte[] aSecret=Encoding.UTF8.GetBytes(secret??"");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config=>
            {
                config.IncludeErrorDetails = true;
                config.RequireHttpsMetadata = true;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,                    
                    ValidateIssuer=false, //Validar Issuer (Variable de entorno)
                    ValidateAudience= false, //Validar Audiencia (Variable de entorno)
                    ValidateLifetime=false, //Valida el tiempo de vida
                    ValidIssuer = configuration.GetSection("jwt").GetValue<string>("Issuer"),
                    ValidAudience = configuration.GetSection("jwt").GetValue<string>("Audience"),                    
                    IssuerSigningKey = new SymmetricSecurityKey(aSecret)
                };
            });
            return services;
        }
    }
}
