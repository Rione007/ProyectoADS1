using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoADS1.Data;
using ProyectoADS1.Models;


namespace ProyectoADS1.Controllers
{
    public class InspeccionController : BaseController
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
        [HttpGet]
        public IActionResult Index(string filtro = "")
        {
            var query = context.FichaInspeccions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.Trim().ToLower();
                query = query.Where(f =>
                    f.Ruc.ToLower().Contains(filtro) ||
                    f.Estado.ToLower().Contains(filtro) ||
                    f.Departamento.ToLower().Contains(filtro));
            }

            var list = query.ToList();

            ViewBag.FiltroActual = filtro;
            if (list.Count == 0)
            {
                ViewBag.mensaje = "No hay inspecciones que coincidan con el filtro.";
            }

            var vm = new InspeccionViewModel
            {
                Inspecciones = list,
                NuevaInspeccion = new FichaInspeccion()
            };

            return View(vm);
        }


        public JsonResult ObtenerSiguienteId() 
        {
            int ultimoId = context.FichaInspeccions.OrderByDescending(f => f.IdInspeccion).Select(f => f.IdInspeccion).FirstOrDefault();

            int siguienteId = (ultimoId > 0 ? ultimoId + 1:1);
            return Json(siguienteId);
        }

        public IActionResult NuevaInspeccion()
        {
            ViewBag.SiguienteId = ObtenerSiguienteId();
            return View(new FichaInspeccion());
        }
        [HttpPost]
        public IActionResult NuevaInspeccion([FromBody] FichaInspeccion reg)
        {
            try
            {
                    context.FichaInspeccions.Add(reg);
                    context.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Inspección añadida con éxito.",
                    id = reg.IdInspeccion
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Error al añadir inspección: " + ex.Message
                });

            }
        }

        public IActionResult BuscarConcesionarioPorRuc(string Ruc)
        {
            if (string.IsNullOrWhiteSpace(Ruc))
                return Json(new { success = false, message = "RUC Vacio" });

            Ruc = Ruc.Trim();
            var concs = BuscarPorRUC(Ruc);
            
            if (concs == null)
            {
                return Json(new { success = false, message = "Concesionario no encontrado." });
            }
            return Json(new
            {
                success = true,
                data = new
                {
                    concs.IdConcesionario,
                    concs.Ruc,
                    concs.NombreComercial,
                    concs.RazonSocial,
                    concs.Direccion,
                    concs.Departamento,
                    concs.Provincia,
                    concs.Telefono,
                    concs.Email
                }
            });

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


        [HttpGet]
        public JsonResult TotalPorDepartamento(string departamento)
        {
            int total = 0;
            if (!string.IsNullOrEmpty(departamento))
            {
                total = context.FichaInspeccions.Count(f => f.Departamento == departamento);
            }
            return Json(total);
        }

        [HttpGet]
        public JsonResult TotalGeneral()
        {
            int total = context.FichaInspeccions.Count();
            return Json(total);
        }

        [HttpGet]
        public JsonResult ObtenerDepartamentos()
        {
            var departamentos = new List<string>
    {
        "Amazonas", "Áncash", "Apurímac", "Arequipa", "Ayacucho", "Cajamarca", "Callao",
        "Cusco", "Huancavelica", "Huánuco", "Ica", "Junín", "La Libertad", "Lambayeque",
        "Lima", "Loreto", "Madre de Dios", "Moquegua", "Pasco", "Piura", "Puno",
        "San Martín", "Tacna", "Tumbes", "Ucayali"
    };

            return Json(departamentos.OrderBy(d => d).ToList());
        }
    }
}
