using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUrlStringReader _urlStringReader;

        public UsuarioController(IUrlStringReader urlStringReader)
        {
            _urlStringReader = urlStringReader;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            
            if (Session["User"] != null)
            {
                IEnumerable<Usuario> listaUsuarios = Sistema.Instancia.FindAllUsers();
                if (listaUsuarios != null)
                    return View(listaUsuarios);
                else
                    return View(new List<Dominio.Usuario>());
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            if (user.Cedula != null && user.Password != null) {

                var resp = urlStringReader.login(new Uri("https://google.com"));
                /*           try
                           {
                               Usuario aux = Sistema.Instancia.Login(user);
                               if (aux != null)
                               {
                                   Session["User"] = user;
                                   return RedirectToAction("Index");
                               }


                           }
                           catch
                           {
                               return View(user);
                           }
                */
            }
            return View();
        }


        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario user)
        {
            if (user.Cedula != null && user.Password != null)
            {
                try
                {
                    Usuario aux = Sistema.Instancia.create(user);
                    if (aux != null)
                    {
                        Session["User"] = user;
                        return RedirectToAction("Index");
                    }

                }
                catch
                {
                    return View(user);
                }
            }
            return View();
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
