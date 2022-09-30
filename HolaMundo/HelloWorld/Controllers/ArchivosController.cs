using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class ArchivosController : Controller
    {
        //AppDbContext _dbContext;

        [HttpGet]
        public ActionResult Index()
        {
            List<Archivo> archivos = new List<Archivo>();
            
                // Recuperamos la Lista de los archivos subidos.
                archivos = Sistema.Instancia.findAllFiles();
            
            // Retornamos la Vista Index, con los archivos subidos.
            return View(archivos);
        }

        [HttpPost]
        public ActionResult SubirArchivo(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // Extraemos el contenido en Bytes del archivo subido.
                var contenido = new byte[file.ContentLength];
                file.InputStream.Read(contenido, 0, file.ContentLength);

                // Separamos el Nombre del archivo de la Extensión.
                int indiceDelUltimoPunto = file.FileName.LastIndexOf('.');
                string nombre = file.FileName.Substring(0, indiceDelUltimoPunto);
                string extension = file.FileName.Substring(indiceDelUltimoPunto + 1,
                                    file.FileName.Length - indiceDelUltimoPunto - 1);

                // Instanciamos la clase Archivo y asignammos los valores.
                Archivo archivo = new Archivo()
                {
                    Nombre = nombre,
                    Extension = extension,
                    Descargas = 0
                };

                try
                {
                    // Subimos el archivo al Servidor.
                    archivo.SubirArchivo(contenido);
                    // Guardamos en la base de datos la instancia del archivo
                    /*       using (_dbContext = new AppDbContext())
                           {
                               _dbContext.Archivos.Add(_archivo);
                               _dbContext.SaveChanges();
                           }
                    */
                    Sistema.Instancia.createArchivo(archivo);
                }
                catch (Exception ex)
                {
                    // Aquí el código para manejar la Excepción.
                }
            }

            // Redirigimos a la Acción 'Index' para mostrar
            // Los archivos subidos al Servidor.
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DescargarArchivo(Guid id)
        {
            Archivo archivo;
            FileContentResult fileContent;

            
            archivo = Sistema.Instancia.findFileById(id);
            

            if (archivo == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    // Descargamos el archivo del Servidor.
                    fileContent = new FileContentResult(archivo.DescargarArchivo(),
                                                         "application/octet-stream");
                    fileContent.FileDownloadName = archivo.Nombre + "." + archivo.Extension;

                    // Actualizamos el nº de descargas en la base de datos.
                    archivo.Descargas++;
                    Sistema.Instancia.updateFile(archivo);

                    return fileContent;
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
        }

               [HttpGet]
               public ActionResult EliminarArchivo(Guid id)
               {
                    Archivo archivo;


                    archivo = Sistema.Instancia.findFileById(id);
                   

                   if (archivo != null)
                   {
                       
                        //   _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
                           
                           if (Sistema.Instancia.deleteFile(archivo))
                           {
                               // Eliminamos el archivo del Servidor.
                               archivo.EliminarArchivo();
                           }
                       
                       // Redirigimos a la Acción 'Index' para mostrar
                       // Los archivos subidos al Servidor.

                           //Codigo nuevo
                       return RedirectToAction("Index");
                   }
                   else
                   {
                       return HttpNotFound();
                   }
               }
       
    }
}
