using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Proyecto_SW_II.Data;
using Proyecto_SW_II.Models;

namespace Proyecto_SW_II.Controllers
{
    public class AlquilersController : Controller
    {
        private readonly AplicationDBContext _context;

        public AlquilersController(AplicationDBContext context)
        {
            _context = context;
        }

        public Boolean acceso()
        {
            if (HttpContext.Session.GetString("TipoUsuario") == null) return false;
            if (HttpContext.Session.GetString("TipoUsuario") == "Cliente") return false;
            if (HttpContext.Session.GetString("TipoUsuario") == "Administrador") return true;
            return false;
        }

        public Boolean isClient()
        {
            if (HttpContext.Session.GetString("TipoUsuario") == null) return false;
            if (HttpContext.Session.GetString("TipoUsuario") == "Cliente") return true;
            if (HttpContext.Session.GetString("TipoUsuario") == "Administrador") return false;
            return false;

        }

        // GET: Alquilers
        public async Task<IActionResult> Index(string searchcompañia, string searchcuenta, string searchpel)
        {
            if (!acceso()) return NotFound();
            if(!String.IsNullOrEmpty(searchcompañia) && !String.IsNullOrEmpty(searchcuenta))
            {
                var list = await _context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    Where(c => c.compañia.Nombre == searchcompañia && c.cuenta.Nombre == searchcuenta).ToListAsync();
                if (list.Any())
                {
                    ViewData["searchcuenta"] = searchcuenta;
                    ViewData["searchcompañia"] = searchcompañia;
                    decimal suma = 0;
                    foreach (var item in list)
                    {
                        suma += item.Pago;
                    }
                    ViewBag.todas = suma;
                }
                else
                {
                    ViewData["errorcc"] = "No se encontraron coincidencias, ¿Ha escrito EXACTAMENTE los nombres?";
                }
                return View(list);
            }
            else if(!String.IsNullOrEmpty(searchcompañia) && String.IsNullOrEmpty(searchcuenta))
            {
                var list = await _context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    Where(c => c.compañia.Nombre == searchcompañia).ToListAsync();
                if (list.Any())
                {
                    ViewData["searchcompañia"] = searchcompañia;
                    decimal suma = 0;
                    foreach (var item in list)
                    {
                        suma += item.Pago;
                    }
                    ViewBag.sumacompañia = suma;
                }
                else
                {
                    ViewData["errorcc"] = "No se encontraron coincidencias, ¿Ha escrito EXACTAMENTE el nombre?";
                }
                return View(list);
            }
            else if (String.IsNullOrEmpty(searchcompañia) && !String.IsNullOrEmpty(searchcuenta))
            {
                var list = await _context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    Where(c => c.cuenta.Nombre == searchcuenta).ToListAsync();
                if (list.Any())
                {
                    ViewData["searchcuenta"] = searchcuenta;
                    decimal suma = 0;
                    foreach (var item in list)
                    {
                        suma += item.Pago;
                    }
                    ViewBag.sumacuenta = suma;
                }
                else
                {
                    ViewData["errorcc"] = "No se encontraron coincidencias, ¿Ha escrito EXACTAMENTE el nombre?";
                }
                return View(list);
            }
            else if (!String.IsNullOrEmpty(searchpel))
            {
                var list = await _context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    Where(p => p.pelicula.Titulo == searchpel).ToListAsync();
                if (list.Any())
                {
                    decimal suma = 0;
                    foreach(var item in list)
                    {
                        suma += item.Pago;
                    }
                    ViewBag.sumapeli = suma;
                }
                else
                {
                    ViewData["errorp"] = "No se encontraron coincidencias, ¿Ha escrito EXACTAMENTE el nombre?";
                }
                return View(list);
            }
            return View(await _context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    ToListAsync());
        }

        public async Task<IActionResult> IndexClient()
        {
            if (!isClient()) return NotFound();
            var id = HttpContext.Session.GetInt32("ID");
            if (id != null)
            {
                return View(await _context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    Where(c => c.cuenta.Id == id).ToListAsync());
            }
            return NotFound();
        }

        public IActionResult DetailsCliente(int? id)
        {
            if (!isClient()) return NotFound();
            if (id == null) return NotFound();

            return View(_context.Alquileres.
                    Include(a => a.cuenta).
                    Include(a => a.compañia).
                    Include(a => a.pelicula).
                    Where(a => a.Id == id).First());
        }

        // GET: Alquilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquileres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // POST: Alquilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id)
        {
            var pelicula = await _context.Peliculas.Include(c => c.compañia).FirstOrDefaultAsync(i => i.Id == id);
            var cuentaid = HttpContext.Session.GetInt32("ID");
            var cuenta = _context.Cuentas.Where(i => i.Id == cuentaid).First();
            if(pelicula == null || pelicula.compañia == null || cuenta == null)
            {
                return NotFound();
            }
            var exist = await _context.Alquileres.
                    Where(c => c.cuenta.Id == cuenta.Id && c.pelicula.Id == pelicula.Id && c.compañia.Id == pelicula.compañia.Id).AnyAsync();
            if(exist)
            {
                return RedirectToAction("ExplorerDetails", "Peliculas", new { id = pelicula.Id, text = "¡Ya la tienes alquilada!" });
            }
            Alquiler a = new Alquiler();
            a.cuenta = cuenta;
            a.Pago = pelicula.Precio;
            a.compañia = pelicula.compañia;
            DateTime d = DateTime.Now;
            a.FechaComienzo = d;
            a.FechaFin = d.AddDays(30);
            a.pelicula = pelicula;
            if (TryValidateModel(a))
            {
                _context.Add(a);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DetailsCliente), new { id = a.Id });
            }
            return NotFound();
        }

        // POST: Alquilers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!acceso()) return NotFound();
            var alquiler = await _context.Alquileres.FindAsync(id);
            _context.Alquileres.Remove(alquiler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerExists(int id)
        {
            return _context.Alquileres.Any(e => e.Id == id);
        }
    }
}
