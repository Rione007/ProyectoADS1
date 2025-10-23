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
    }
}
