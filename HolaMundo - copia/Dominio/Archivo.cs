using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dominio
{
    public class Archivo
    {



        public Archivo()
        {
            this.Id = Guid.NewGuid();
            this.Creado = DateTime.Now;
        }

        // PROPIEDADES PÚBLICAS
        
        public Guid Id { get; set; }

       
        public DateTime Creado { get; set; }

       
        public string Nombre { get; set; }

       
        public string Extension { get; set; }

        
        public int Descargas { get; set; }

        // PROPIEDADES PRIVADAS
        public string PathRelativo
        {
            get
            {
                return ConfigurationManager.AppSettings["PathArchivos"] +
                                            this.Id.ToString() + "." +
                                            this.Extension;
            }
        }

        public string PathCompleto
        {
            get
            {
                var _PathAplicacion = HttpContext.Current.Request.PhysicalApplicationPath;
                return Path.Combine(_PathAplicacion, this.PathRelativo);
            }
        }

        // MÉTODOS PÚBLICOS
        public void SubirArchivo(byte[] archivo)
        {
            File.WriteAllBytes(this.PathCompleto, archivo);
        }

        public byte[] DescargarArchivo()
        {
            return File.ReadAllBytes(this.PathCompleto);
        }

        public void EliminarArchivo()
        {
            File.Delete(this.PathCompleto);
        }




    }
}
