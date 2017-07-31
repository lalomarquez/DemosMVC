using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidades;
using DAL;
using RelojChecador.Models;

namespace RelojChecador.Controllers
{
    public class InicioController : Controller
    {
        private AccesoDatos dal = new AccesoDatos();
        private dbContexUsuario db = new dbContexUsuario();

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

        [HttpPost]
        public ActionResult AltaUsuarios(EntUsuario usuario)
        {
            //if (Session["Nombre"] != null)
            //    return View();
            //else
            //    return RedirectToAction("Login");            
            
            try
            {
                string existeCorreo = string.Empty;
                existeCorreo = dal.ValidarExisteUsuario(usuario);

                if (ModelState.IsValid)
                {
                    if (existeCorreo == "true")
                        ViewBag.existeCorreo = "Ya existe un usuario registrado con ese mismo correo electronico.";
                    else
                    {
                        ViewBag.Message = dal.AltaNuevoUsuario(usuario);
                        return RedirectToAction("ListaUsuarios");
                    }
                }
                //ModelState.Clear();
                //return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                //return View();
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(int id)
        {                        
            return View(dal.ObtenerTodosUsuarios().Find(u => u.IdUsuario == id));
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(int id, EntUsuario u)
        {
            string existeCorreo = string.Empty;
            existeCorreo = dal.ValidarExisteUsuario(u);

            try
            {
                if (existeCorreo == "true")
                    ViewBag.existeCorreo = "Ya existe un usuario registrado con ese mismo correo electronico.";
                else
                {
                    ViewBag.Message = dal.ActualizarUsuario(u);
                    return RedirectToAction("ListaUsuarios");
                }                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {                
                if (dal.EliminarUsuario(id))                
                    ViewBag.AlertMsg = "Usuario Eliminado!!";
                
                return RedirectToAction("ListaUsuarios");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ListaUsuarios");
            }
        }  

        [HttpGet]
        public ActionResult ListaUsuarios()
        {
            //if (Session["Nombre"] != null)
            //    return View();
            //else
            //    return RedirectToAction("Login");

            var listUsuarios =  dal.ListaUsuarios();

            return View(listUsuarios.ToList());
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon(); 
            return RedirectToAction("Login");
        }

        //[HttpPost]
        //public JsonResult AjaxMethod(EntUsuario usuario)
        //{
        //    string resultado = dal.ValidarExisteUsuario(usuario);

        //    if (resultado == "true")
        //        ViewBag.existeCorreo = "Ya existe un usuario registrado con ese correo!";

        //    return Json(resultado);
        //}
        
    }
}
