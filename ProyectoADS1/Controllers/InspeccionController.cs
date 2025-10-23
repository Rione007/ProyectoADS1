using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;


namespace ProyectoADS1.Controllers
{
    public class InspeccionController : Controller
    {
        DBADS1Context context;
        public InspeccionController(DBADS1Context contexto)
        {
            this.context = contexto;
        }
        public Concesionario BuscarPorRUC(string ruc)
        {
            return context.Concesionarios.AsNoTracking().FirstOrDefault(c => c.Ruc == ruc);
        }
        public IActionResult Index()
        {
            var list = context.FichaInspeccions.ToList();
            if (list.Count == 0)
            {
                ViewBag.mensaje = "No hay inspecciones registradas.";
                return View();
            }
            return View(list);
        }

        public int ObtenerSiguienteId() 
        {
            int siguienteId = context.FichaInspeccions.OrderByDescending(f => f.IdInspeccion).Select(f => f.IdInspeccion).FirstOrDefault() + 1;
            return siguienteId;
        }

        public IActionResult NuevaInspeccion()
        {
            ViewBag.SiguienteId = ObtenerSiguienteId();
            return View(new FichaInspeccion());
        }
        [HttpPost]
        public IActionResult NuevaInspeccion(FichaInspeccion reg)
        {
            try
            {
                    context.FichaInspeccions.Add(reg);
                    context.SaveChanges();
                    ViewBag.SiguienteId = reg.IdInspeccion;
                    ViewBag.mensaje = "Inspeccion añadida con exito!";
                    return View(reg);
            }
            catch (Exception ex)
            {
                ViewBag.SiguienteId = reg.IdInspeccion;
                ViewBag.mensaje = "Error al añadir inspeccion: "+ ex;
                
            }
            return View(reg);
        }

        public IActionResult BuscarConcesionarioPorRuc(string Ruc)
        {
            Ruc = Ruc.Trim();
            var concs = BuscarPorRUC(Ruc);
            if(concs != null)
            {
                var model = new FichaInspeccion
                {
                    IdConcesionario = concs.IdConcesionario,
                    Ruc = Ruc,
                    NombreComercial = concs.NombreComercial,
                    RazonSocial = concs.RazonSocial,
                    Direccion = concs.Direccion,
                    Departamento = concs.Departamento,
                    Provincia = concs.Provincia,
                    Telefono = concs.Telefono,
                    Email = concs.Email
                };
                ViewBag.SiguienteId = ObtenerSiguienteId();
                return View("NuevaInspeccion", model);
            }
            else
            {
                ViewBag.SiguienteId = ObtenerSiguienteId();
                ViewBag.advertencia = "RUC no encontrado.";
                return View("NuevaInspeccion");
            }

        }

        public FichaInspeccion BuscarPorId(int id)
        {
            return context.FichaInspeccions.AsNoTracking().FirstOrDefault(c => c.IdInspeccion == id);
        }

        public IActionResult FichaRegistral(int id)
        {
            FichaInspeccion reg = BuscarPorId((int)id);
            return View(reg);
        }
    }
}
