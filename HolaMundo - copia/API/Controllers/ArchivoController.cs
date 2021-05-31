using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
//    [Authorize]

    [Authorize(Roles = "ADMIN")]
    public class ArchivoController : ApiController
    {
        [AllowAnonymous]
        // GET: api/Archivo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Authorize]
        // GET: api/Archivo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Archivo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Archivo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Archivo/5
        public void Delete(int id)
        {
        }
    }
}
