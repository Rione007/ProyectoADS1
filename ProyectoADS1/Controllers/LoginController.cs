using Microsoft.AspNetCore.Mvc;
using ProyectoADS1.Models;

namespace ProyectoADS1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
