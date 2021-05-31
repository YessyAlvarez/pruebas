using API.Models;
using Dominio;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace API
{
    internal static class GeneradorToken
    {
        public static string GenerarTokenJWT(Usuario usuarioInfo)
        {
            // RECUPERAMOS LAS VARIABLES DE CONFIGURACIÓN
            var _ClaveSecreta = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var _Issuer = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var _Audience = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            if (!Int32.TryParse(ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"], out int _Expires))
                _Expires = 24;


            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_ClaveSecreta));
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.Cedula),
                new Claim("nombre", usuarioInfo.Nombre != null ? usuarioInfo.Nombre : ""),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email!=null ? usuarioInfo.Email : ""),
                new Claim(ClaimTypes.Role, usuarioInfo.Rol != null ? usuarioInfo.Rol : "")
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: _Issuer,
                    audience: _Audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddMinutes(_Expires)
 //                   expires: DateTime.UtcNow.AddHours(_Expires)
                );
           

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }




  /*      public static string GenerateTokenJwt(string username)
        {
            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = new JwtSecurityToken();
            try
            {
                jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                    audience: audienceToken,
                    issuer: issuerToken,
                    subject: claimsIdentity,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                    signingCredentials: signingCredentials);
            }
            catch(Exception ex)
            {
                if (ex != null)
                {
                    int asd = 4;
                }
            }

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }

*/



    }
}