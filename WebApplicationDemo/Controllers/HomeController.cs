using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationDemo.Data;
using WebApplicationDemo.Models;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        /*hacemos una instancia de la base de datos*/
        /*_context es una instancia de la clase AppDbContext, que hereda de DbContext. Esta clase 
         * forma parte de Entity Framework Core y representa la conexión con la base de datos y proporciona 
         * acceso a las tablas y registros de tu base de datos.*/
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context; 
        }

        public IActionResult Index() 
        {
            var tareas = _context.Tarea.ToList();
            return View(tareas);
        }

        public IActionResult Detalles(int id)
        {
            var tarea = _context.Tarea.FirstOrDefault(t => t.Id == id);
            return View(tarea);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            _context.Tarea.Add(tarea);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var tarea = _context.Tarea.FirstOrDefault(t => t.Id == id);
            return View(tarea);
        }

        [HttpPost]
        public IActionResult Editar(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return View(tarea);
            }

            var tareaExistente = _context.Tarea.FirstOrDefault(t => t.Id == tarea.Id);
            if (tareaExistente == null)
            {
                return RedirectToAction("Index");
            }

            tareaExistente.Nombre = tarea.Nombre;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.FechaVencimiento = tarea.FechaVencimiento;
            tareaExistente.EstaCompleta = tarea.EstaCompleta;
            tareaExistente.Explicacion = tarea.Explicacion;
            tareaExistente.Prioridad = tarea.Prioridad;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var tarea = _context.Tarea.FirstOrDefault(t => t.Id == id);
            return View(tarea);
        }

        [HttpPost]
        public IActionResult Eliminar(Tarea tarea)
        {
            _context.Tarea.Remove(tarea);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarEstado(int id, bool estado)
        {
            var tarea = _context.Tarea.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                tarea.EstaCompleta = estado;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ActualizarPrioridad(int id, string prioridad)
        {
            // Busca la tarea por ID
            var tarea = _context.Tarea.FirstOrDefault(t => t.Id == id);
            if (tarea != null)
            {
                // Actualiza la prioridad
                tarea.Prioridad = prioridad;
                _context.SaveChanges(); // Guarda los cambios en la base de datos
            }

            // Redirige a la lista de tareas
            return RedirectToAction("Index");
        }

        
        //public IActionResult Calendar()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Calendar()
        {
            // Obtener todas las tareas
            var tareas = _context.Tarea.ToList() ?? new List<Tarea>();

            ViewBag.FechasVencimiento = tareas.Select(t => t.FechaVencimiento).ToList();

            return View(tareas);
        }
    }
}

