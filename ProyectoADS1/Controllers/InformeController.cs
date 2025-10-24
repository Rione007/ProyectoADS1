using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;

namespace ProyectoADS1.Controllers
{
    public class InformeController : BaseController
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
                .Include(i => i.IdInspeccionNavigation) 
                    .ThenInclude(f => f.ActaInspeccion)  
                .Include(i => i.IdUsuarioNavigation)    
                .FirstOrDefault(i => i.IdInspeccion == id);
        }
        public IActionResult InformeDeInspeccion(int id)
        {
             var reg = BuscarInforme(id);
            return View(reg);
        }

        [HttpPost]
        public IActionResult InformeDeInspeccion(InformeInspeccion model)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            model.IdUsuario = usuarioId;

            var existente = context.InformeInspeccions
                .FirstOrDefault(i => i.IdInspeccion == model.IdInspeccion);

            if (existente != null)
            {
                existente.FirmaSupervisorImagen = model.FirmaSupervisorImagen;
                existente.FirmaCoordinadorImagen = model.FirmaCoordinadorImagen;
                existente.FechaRegistro = model.FechaRegistro ?? DateTime.Now;
                existente.IdUsuario = model.IdUsuario;
            }
            else
            {
                model.FechaRegistro = DateTime.Now;
                context.InformeInspeccions.Add(model);
            }

            context.SaveChanges();

            return RedirectToAction("InformeDeInspeccion", new { id = model.IdInspeccion });
        }

    }
}
