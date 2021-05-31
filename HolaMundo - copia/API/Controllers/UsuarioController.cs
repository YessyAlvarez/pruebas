using API.Models;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoginAsync(UsuarioLogin usuarioLogin)
        {
            if (usuarioLogin == null)
                return BadRequest("Usuario y Contraseña requeridos.");


            //UsuarioInfo userInfo = await AutenticarUsuarioAsync(usuarioLogin.Cedula, usuarioLogin.Password);
            Usuario user = Sistema.Instancia.Login(new Usuario { Cedula = usuarioLogin.Cedula, Password = usuarioLogin.Password });
            if (user != null)
            {
                var tok = GeneradorToken.GenerarTokenJWT(user);
                return Ok(new { token = tok, usuario = user });
            }
            else
            {     
                return Unauthorized();
            }
        }

        // COMPROBAMOS SI EL USUARIO EXISTE EN LA BASE DE DATOS 
        private async Task<UsuarioInfo> AutenticarUsuarioAsync(string usuario, string password)
        {
            // AQUÍ LA LÓGICA DE AUTENTICACIÓN //

            // Supondremos que el usuario existe en la Base de Datos.
            // Retornamos un objeto del tipo UsuarioInfo, con toda
            // la información del usuario necesaria para el Token.
            return new UsuarioInfo()
            {
                // Id del Usuario en el Sistema de Información (BD)
                Id = new Guid("B5D233F0-6EC2-4950-8CD7-F44D16EC878F"),
                Nombre = "Nombre",
                Apellidos = "Apellidos Usuario",
                Email = "email.usuario@dominio.com",
                Rol = "Administrador"
            };

            // Supondremos que el usuario NO existe en la Base de Datos.
            // Retornamos NULL.
            //return null;
        }























/*

        // GET: api/Usuario
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Usuario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Usuario
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
*/
    }
}
