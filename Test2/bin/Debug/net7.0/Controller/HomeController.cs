using Microsoft.AspNetCore.Mvc;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(string nombreUsuario, string contraseña)
        {
            var usuario = RepositorioUsuarios.Usuarios.Find(u => u.NombreUsuario == nombreUsuario && u.Contraseña == contraseña);
            if (usuario != null)
            {
                // En una aplicación real, deberías usar autenticación de ASP.NET Core
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Nombre de usuario o contraseña incorrectos.";
                return View();
            }
        }
    }
}
