using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;

namespace ProyectoADS1.Controllers
{
    public class ConcesionarioController : Controller
    {
        private readonly DBADS1Context context;
        public ConcesionarioController(DBADS1Context context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var list = context.Concesionarios.ToList();
            return View(list);
        }

        public IActionResult NuevaConcesionaria()
        {
            return View(new Concesionario());
        }
        [HttpPost]
        public IActionResult NuevaConcesionaria(Concesionario reg)
        {
            if (ModelState.IsValid)
            {
                context.Concesionarios.Add(reg);
                context.SaveChanges();
                ViewBag.mensaje = "Concesionaria añadida con exito!";
                return View(reg);
            }
            else
            {
                ViewBag.mensaje = "Error al añadir";
                return View(reg);
            }

        }
        public Concesionario BuscarPorRUC(string ruc)
        {
            return context.Concesionarios.AsNoTracking().FirstOrDefault(c => c.Ruc == ruc);
        }

        public IActionResult Editar(string id)
        {
            var reg = BuscarPorRUC(id);
            return View(reg);
        }
        [HttpPost]
        public IActionResult Editar(Concesionario reg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var registroOriginal = BuscarPorRUC(reg.Ruc);
                    if (registroOriginal == null)
                    {
                        ViewBag.mensaje = "El concesionario no fue encontrado.";
                        return View(reg);
                    }

                    reg.FechaRegistro = registroOriginal.FechaRegistro;

                    context.Update(reg);
                    context.SaveChanges();

                    ViewBag.mensaje = "Concesionario actualizado correctamente!";
                    return View(reg);
                }
                catch (Exception ex)
                {
                    ViewBag.mensaje = "Error al actualizar: "+ ex.Message;
                }
            }
            return View(reg);
        }

        public IActionResult Detalles(string id)
        {
            var reg = BuscarPorRUC(id);
            return View(reg);
        }
    }
}
