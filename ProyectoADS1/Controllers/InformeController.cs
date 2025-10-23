using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;

namespace ProyectoADS1.Controllers
{
    public class InformeController : Controller
    {
        DBADS1Context context;

        public InformeController(DBADS1Context context)
        {
            this.context = context;

        }

        public IActionResult CrearInforme(int id)
        {
            var informeExist = context.InformeInspeccions.FirstOrDefault(a => a.IdInspeccion == id);

            if (informeExist == null)
            {
                InformeInspeccion reg = new InformeInspeccion()
                {
                    IdInspeccion = id
                };
                context.InformeInspeccions.Add(reg);
                context.SaveChanges();
            }

            return RedirectToAction("InformeDeInspeccion", new { id = id });
        }

        public InformeInspeccion BuscarInforme(int id)
        {
            return context.InformeInspeccions
                .Include(i => i.IdInspeccionNavigation) // Carga la FichaInspeccion
                    .ThenInclude(f => f.ActaInspeccion)  // Desde la Ficha, carga la ActaInspeccion
                .Include(i => i.IdUsuarioNavigation)     // Usuario que generó el informe (si existe)
                .FirstOrDefault(i => i.IdInspeccion == id);
        }
        public IActionResult InformeDeInspeccion(int id)
        {
             var reg = BuscarInforme(id);
            return View(reg);
        }
    }
}
