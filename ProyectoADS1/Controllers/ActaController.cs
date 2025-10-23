using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;

namespace ProyectoADS1.Controllers
{
    public class ActaController : Controller
    {
        DBADS1Context context;
        public ActaController(DBADS1Context context)
        {
            this.context = context;   
        }
        public IActionResult CrearActa(int id)
        {
            var actaExistente = context.ActaInspeccions.FirstOrDefault(a => a.IdInspeccion == id);

            if (actaExistente == null)
            {
                ActaInspeccion reg = new ActaInspeccion()
                {
                    IdInspeccion = id
                };
                context.ActaInspeccions.Add(reg);
                context.SaveChanges();
            }

            return RedirectToAction("ActaDeInspeccion", new { id = id });
        }

        public ActaInspeccion BuscarActa(int id)
        {
            return context.ActaInspeccions.Include(c => c.IdInspeccionNavigation)
                .FirstOrDefault(a => a.IdInspeccion == id);
        }
        public IActionResult ActaDeInspeccion(int id)
        {
            var reg = BuscarActa(id);
            return View(reg);
        }
    }
}
