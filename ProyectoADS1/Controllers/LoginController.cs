using Microsoft.AspNetCore.Mvc;
using ProyectoADS1.Data;
using ProyectoADS1.Models;
using System.Linq;

namespace ProyectoADS1.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBADS1Context _context;

        public LoginController(DBADS1Context context)
        {
            _context = context;
        }

        // GET: /Login/
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login/
        [HttpPost]
        public IActionResult Index(string correo, string password)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Por favor ingrese correo y contraseña.";
                return View();
            }

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == correo && u.Password == password && u.Activo == true);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos, o usuario inactivo.";
                return View();
            }

            // Puedes guardar el usuario en sesión
            HttpContext.Session.SetString("UsuarioNombre", usuario.NombreUsuario);
            HttpContext.Session.SetString("UsuarioRol", usuario.Rol);
            HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);

            // Redirige a una vista principal (por ejemplo, Inspeccion/Index)
            return RedirectToAction("Index", "Inspeccion");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
