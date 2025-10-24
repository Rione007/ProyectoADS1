using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;

namespace ProyectoADS1.Controllers
{
    public class MemorandumController : BaseController
    {
        DBADS1Context context;
        public MemorandumController(DBADS1Context context)
        {
            this.context = context;
        }

        public IActionResult CrearMemo(int id)
        {
            var memoExist = context.MemorandumInspeccions.FirstOrDefault(a => a.IdInspeccion == id);

            if (memoExist == null)
            {
                MemorandumInspeccion reg = new MemorandumInspeccion()
                {
                    IdInspeccion = id
                };
                context.MemorandumInspeccions.Add(reg);
                context.SaveChanges();
            }

            return RedirectToAction("MemorandumDeInspeccion", new { id = id });
        }

        public MemorandumInspeccion BuscarMemo(int id)
        {
            var reg = context.MemorandumInspeccions.Include(c => c.IdInspeccionNavigation).FirstOrDefault(c => c.IdInspeccion == id);
                return reg;
        }
        
        public IActionResult MemorandumDeInspeccion(int id)
        {
            var reg = BuscarMemo(id);
            return View(reg);
        }

        [HttpPost]
        public IActionResult MemorandumDeInspeccion(MemorandumInspeccion model)
        {

            var existente = context.MemorandumInspeccions
                .FirstOrDefault(m => m.IdInspeccion == model.IdInspeccion);

            if (existente != null)
            {

                existente.Asunto = model.Asunto;
                existente.Cuerpo = model.Cuerpo;
                existente.FirmaCoordinadorGeneral = model.FirmaCoordinadorGeneral;
                existente.FirmaCoordinador = model.FirmaCoordinador;
                existente.FirmaDirectorGeneral = model.FirmaDirectorGeneral;
                existente.FechaActualizada = DateTime.Now;
            }
            else
            {
                model.FechaRegistro = DateTime.Now;
                model.FechaActualizada = DateTime.Now;
                context.MemorandumInspeccions.Add(model);
            }

            context.SaveChanges();

            return RedirectToAction("MemorandumDeInspeccion", new { id = model.IdInspeccion });
        }

    }
}
