using CapaEntidades;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

            try
            {
                if (ModelState.IsValid)
                {
                    var v = crede.Where(existe => existe.Correo.Equals(c.Correo.Trim()) && existe.Contrasena.Equals(c.Contrasena.Trim())).FirstOrDefault();
                    if (v != null)
                    {
                        Session["Nombre"] = v.Nombre.ToString();
                        Session["IdUsuario"] = v.IdUsuario.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("", "Usuario o contraseña no válidos.");
                }
                else
                    ModelState.AddModelError("", "Usuario o contraseña no válidos.");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
            }             
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
            if (Session["Nombre"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult RegistrarEntrada(EntRegistrarEntrada entrada)
        {
            string verificarExisteUsuario = string.Empty;
            string verificarEntradaAnterior = string.Empty;
            
            try
            {
                if (ModelState.IsValid)
                {
                    verificarExisteUsuario = dal.VerificarExisteUsuario(entrada);

                    if (verificarExisteUsuario == "NOEXISTE")
                        ViewBag.NoExiste = "Usuario no registrado.";
                    else
                    {
                        entrada.TipoRegistro = "1";
                        verificarEntradaAnterior = dal.VerificarRegistroEntrada(entrada);

                        if (verificarEntradaAnterior == "ENTRADAREGISTRADA")                        
                            ViewBag.EntradaRegistrada = "El usuario ya ha registro su entrada.";
                        else
                        {
                            dal.RegistrarEntrada(entrada);
                            return RedirectToAction("ReporteAsistencia");
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
            }

            return View(entrada);
        }

        [HttpGet]
        public ActionResult RegistrarSalida()
        {
            if (Session["Nombre"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult RegistrarSalida(EntRegistrarEntrada entrada)
        {
            string verificarExisteUsuario = string.Empty;
            string verificarExisteEntrada = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    verificarExisteUsuario = dal.VerificarExisteUsuario(entrada);

                    if (verificarExisteUsuario == "NOEXISTE")
                        ViewBag.NoExiste = "Usuario no registrado.";
                    else
                    {
                        entrada.TipoRegistro = "1";
                        verificarExisteEntrada = dal.VerificarRegistroEntrada(entrada);

                        if (verificarExisteEntrada == "ENTRADAREGISTRADA")
                        {
                            entrada.TipoRegistro = "2";
                            dal.RegistrarEntrada(entrada);
                            return RedirectToAction("ReporteAsistencia");
                        }
                        else                        
                            ViewBag.SinEntradaRegistrada = "El usuario no tiene un registro de entrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
            }

            return View(entrada);
        }

        [HttpGet]
        public ActionResult ReporteAsistencia()
        {
            if (Session["Nombre"] != null)
            {
                var reporteAsistencia = dal.ObtenerRegistroAsistencia();
                return View(reporteAsistencia.ToList());
            }
            else
                return RedirectToAction("Login");            
        }

        [HttpGet]
        public ActionResult AltaUsuarios()
        {
            if (Session["Nombre"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult AltaUsuarios(EntUsuario usuario)
        {                    
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
            if (Session["Nombre"] != null)            
                return View(dal.ObtenerTodosUsuarios().Find(u => u.IdUsuario == id));            
            else
                return RedirectToAction("Login");            
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(int id, EntUsuario u)
        {
            string existeCorreo = string.Empty;
            existeCorreo = dal.ValidarExisteUsuario(u);

            try
            {
                if (existeCorreo == "true" && u.IdUsuario != id)
                    ViewBag.existeCorreo = "Ya existe un usuario registrado con ese mismo correo electronico.";
                else
                {
                    dal.ActualizarUsuario(u);
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
            if (Session["Nombre"] != null)
            {
                try
                {
                    if (dal.EliminarUsuario(id))
                        ViewBag.AlertMsg = "Usuario Eliminado!!";

                    return RedirectToAction("ListaUsuarios");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.ToString();
                    return RedirectToAction("ListaUsuarios");
                }
            }
            else
                return RedirectToAction("Login");
        }  

        [HttpGet]
        public ActionResult ListaUsuarios()
        {
            if (Session["Nombre"] != null)
            {
                var listUsuarios = dal.ListaUsuarios();
                return View(listUsuarios.ToList());
            }
            else
                return RedirectToAction("Login");                       
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            Session.Abandon(); 
            return RedirectToAction("Login");
        }
    }
}
