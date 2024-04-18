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

        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            string? secret = configuration.GetSection("jwt")?.GetValue<string>("secret");
            byte[] aSecret=Encoding.UTF8.GetBytes(secret??"");
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config=>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey=new SymmetricSecurityKey(aSecret),
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });
            return services;
        }
    }
}
