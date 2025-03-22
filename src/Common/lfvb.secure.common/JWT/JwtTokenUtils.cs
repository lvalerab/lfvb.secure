using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lfvb.secure.common.JWT
{
    public class JwtTokenUtils:IJwtTokenUtils
    {
        /// <summary>
        /// Crea un token con los datos de validacion
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <param name="subject"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="secret"></param>
        /// <param name="expiresInMinutes"></param>
        /// <returns></returns>
        public string GetToken(string id, string usuario, string subject, string issuer, string audience,  string secret, int expiresInMinutes=60)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64), //El Iat debe ser un entero
                new Claim("ID",id.ToString()),
                new Claim("USUARIO",usuario.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer,
                    audience,
                    claims.ToArray(),
                    expires: DateTime.Now.AddMinutes((double)expiresInMinutes),
                    signingCredentials: singIn
                );

            string textoToken = new JwtSecurityTokenHandler().WriteToken(token);

            return textoToken;
        }

        public Guid? GetIdFromToken(HttpContext contexto, bool rf = false)
        {
            var identity=contexto.User.Identity as ClaimsIdentity;

            return this.validarToken(identity);
        }

        public Guid? validarToken(ClaimsIdentity? identidad)
        {
            try
            {
                if(identidad==null || identidad.Claims.Count()==0)
                {
                    return null;
                } else
                {
                    string id = identidad.Claims.FirstOrDefault(x => x.Type == "ID").Value;
                    return Guid.Parse(id);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
    }
}
