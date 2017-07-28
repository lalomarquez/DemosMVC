using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using DAL;

namespace RelojChecador.Controllers
{
    public class InicioController : Controller
    {
        private AccesoDatos dal = new AccesoDatos();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EntCredenciales c)
        {            
            var crede = new List<EntCredenciales>();
            crede = dal.ObtenerCredenciales();

            if (ModelState.IsValid)
            {
                var v = crede.Where(existe => existe.Correo.Equals(c.Correo.Trim()) && existe.Contrasena.Equals(c.Contrasena.Trim())).FirstOrDefault();
                if (v != null)
                {
                    Session["Nombre"] = v.Nombre.ToString();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Usuario o contraseña no válidos.");
            }
            else
                ModelState.AddModelError("", "Usuario o contraseña no válidos.");                

            return View();
        }

        // GET: Inicio
        [Route("Inicio")]
        public ActionResult Index()
        {
            if (Session["Nombre"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult RegistrarEntrada()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrarSalida()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReporteAsistencia()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AltaUsuarios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaUsuarios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon(); 
            return RedirectToAction("Login");
        }

        // GET: Inicio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inicio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inicio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inicio/Edit/5
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

        // GET: Inicio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inicio/Delete/5
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
