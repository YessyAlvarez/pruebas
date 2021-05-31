using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class PersonaaController : ApiController
    {
        // GET: api/Personaa
        public IEnumerable<Persona> Get()
        {
            Persona personita = new Persona();
            IEnumerable<Persona> listaPersona = personita.FindAll();
            return (IEnumerable<Persona>)Ok(listaPersona);
        }

        // GET: api/Personaa/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Personaa
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Personaa/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Personaa/5
        public void Delete(int id)
        {
        }
    }
}
