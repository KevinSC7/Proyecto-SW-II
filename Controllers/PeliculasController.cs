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
    public class PeliculasController : Controller
    {
        private readonly AplicationDBContext _context;

        public PeliculasController(AplicationDBContext context)
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

        // GET: Peliculas
        public async Task<IActionResult> Index(string searchpel, string searchcat, string searchcompañia)
        {
            if (!acceso()) return NotFound();
            
            if (!String.IsNullOrEmpty(searchpel))
            {
                return View(await _context.Peliculas.Include(p => p.compañia).Where(n => n.Titulo.Contains(searchpel)).ToListAsync());
            }
            else if (!String.IsNullOrEmpty(searchcat))
            {
                var r = await _context.RelacionesCategoriaPelicula.Include(p => p.pelicula).Include(c => c.categoria).
                    Where(c => c.categoria.Nombre.Contains(searchcat))
                    .ToListAsync();
                List<Pelicula> l = new List<Pelicula>();
                foreach (var item in r)
                {
                    l.Add(_context.Peliculas.First(i => i.Id == item.pelicula.Id));
                }
                return View(l);
            }
            else if (!String.IsNullOrEmpty(searchcompañia))
            {
                return View(await _context.Peliculas.Include(p => p.compañia).
                    Where(c => c.compañia.Nombre.Contains(searchcompañia)).ToListAsync());
            }
            return View(await _context.Peliculas.Include(p => p.compañia).ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.Include(p => p.compañia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        public async Task<IActionResult> selectCompañia(string searchString)
        {
            if (!acceso()) return NotFound();
            CompañiaController cc = new CompañiaController(_context);
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await cc.getLista(searchString));
            }
            
            return View(await cc.getLista());
        }

        // GET: Peliculas/Create
        public IActionResult Create(int? id)
        {
            if (!acceso()) return NotFound();
            if (id != null) 
            {
                var c = _context.Compañias.FirstOrDefault(x =>x.Id == id);
                IntermedioPeliculaCompañia i = new IntermedioPeliculaCompañia(null, c);
                return View(i);
            }
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula, Compañia compañia)
        {
            if(_context.Compañias.Where(c => c.Id == compañia.Id).Any())
            {
                var c = _context.Compañias.Where(n => n.Id == compañia.Id).First();
                pelicula.compañia = c;
            }
            else
            {
                ViewData["compa"] = "Debe escoger una compañia";
                return View(new IntermedioPeliculaCompañia(pelicula, null));
            }
            
            
            if (String.IsNullOrEmpty(pelicula.Portada))
            {
                pelicula.Portada = "portada_no_disponible.jpg";
            }

            IntermedioPeliculaCompañia i = new IntermedioPeliculaCompañia(pelicula, compañia);
            ModelState.Clear();
            if (TryValidateModel(pelicula))
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(i);
        }

        public async Task<IActionResult> vincularCompañia(int? id, string searchString)
        {
            if (!acceso()) return NotFound();
            CompañiaController cc = new CompañiaController(_context);
            ViewBag.idpeli = id;
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await cc.getLista(searchString));
            }
            return View(await cc.getLista());
        }

        public async Task<IActionResult> ExplorerDetails(int? id, string text)
        {
            if (!isClient()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }
            var pelicula = await _context.Peliculas.Include(x => x.compañia).FirstOrDefaultAsync(i => i.Id == id);
            if (!string.IsNullOrEmpty(text)) ViewData["alquilada"] = text;
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id, int? idcompañia)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.Include(x => x.compañia).FirstOrDefaultAsync(i => i.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            if(idcompañia != null)
            {
                var c = await _context.Compañias.FindAsync(idcompañia);
                if (c == null)
                {
                    return NotFound();
                }
                pelicula.compañia = c;
            }
            ViewBag.idpeli = pelicula.Id;
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,FechaLanzamiento,Precio,Portada")] Pelicula pelicula, int compañia)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }
            if (compañia != 0)
            {
                var c = await _context.Compañias.FindAsync(compañia);
                if (c == null)
                {
                    return NotFound();
                }
                pelicula.compañia = c;
            }
            if (String.IsNullOrEmpty(pelicula.Portada))
            {
                pelicula.Portada = "portada_no_disponible.jpg";
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alquileres = await _context.Alquileres.Where(p => p.pelicula.Id == id).ToListAsync();
            foreach(var item in alquileres)
            {
                _context.Alquileres.Remove(item);
            }
            var r = await _context.RelacionesCategoriaPelicula.Where(p => p.pelicula.Id == id).ToListAsync();
            foreach (var item in r)
            {
                _context.RelacionesCategoriaPelicula.Remove(item);
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> explorar(string searchpel, int? idcat)
        {
            if (!isClient()) return NotFound();
            List<Categoria> l = await _context.Categorias.ToListAsync();
            ViewBag.listacategorias = l;
            if(idcat != null)
            {
                List<Pelicula> lp = new List<Pelicula>();
                var r = await _context.RelacionesCategoriaPelicula.Include(p => p.pelicula).Where(c => c.categoria.Id == idcat).ToListAsync();
                foreach(var item in r)
                {
                    lp.Add(item.pelicula);
                }
                ViewData["cat"] =_context.Categorias.First(i => i.Id == idcat).Nombre;
                return View(lp);
            }
            if (!String.IsNullOrEmpty(searchpel))
            {
                return View(await _context.Peliculas.Where(p => p.Titulo.Contains(searchpel)).ToListAsync());
            }
            return View(await _context.Peliculas.ToListAsync());
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
