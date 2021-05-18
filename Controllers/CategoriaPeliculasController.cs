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
    public class CategoriaPeliculasController : Controller
    {
        private readonly AplicationDBContext _context;

        public CategoriaPeliculasController(AplicationDBContext context)
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

        // GET: CategoriaPeliculas
        public async Task<IActionResult> Index(string searchcat, string searchpel)
        {
            if (!acceso()) return NotFound();
            if (!String.IsNullOrEmpty(searchcat) && !String.IsNullOrEmpty(searchpel))
            {
                return View(await _context.RelacionesCategoriaPelicula.
                    Where(c => c.categoria.Nombre.Contains(searchcat) && c.pelicula.Titulo.Contains(searchpel)).
                    ToListAsync());
            }
            if(String.IsNullOrEmpty(searchcat) && !String.IsNullOrEmpty(searchpel))
            {
                return View(await _context.RelacionesCategoriaPelicula.
                    Where(c => c.pelicula.Titulo.Contains(searchpel)).
                    ToListAsync());
            }
            if(!String.IsNullOrEmpty(searchcat) && String.IsNullOrEmpty(searchpel))
            {
                return View(await _context.RelacionesCategoriaPelicula.
                    Where(c => c.categoria.Nombre.Contains(searchcat)).
                    ToListAsync());
            }
            return View(await _context.RelacionesCategoriaPelicula.ToListAsync());
        }

        // GET: CategoriaPeliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaPelicula = await _context.RelacionesCategoriaPelicula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaPelicula == null)
            {
                return NotFound();
            }

            return View(categoriaPelicula);
        }

        public async Task<IActionResult> AsignarCategoria(int? id, string searchString)
        {
            if (!acceso()) return NotFound();
            
            if (id == null)
            {
                return NotFound();
            }

            List<CategoriaPelicula> r = await _context.RelacionesCategoriaPelicula.Include(p => p.pelicula).
                Include(c => c.categoria).Where(p => p.pelicula.Id == id).ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                List<Categoria> lc = await _context.Categorias.Where(c => c.Nombre.Contains(searchString)).ToListAsync();
                
                IntermedioCategoriasRelaciones cr = new IntermedioCategoriasRelaciones(lc, r, (int)id);
                return View(cr);
            }
            IntermedioCategoriasRelaciones i = new IntermedioCategoriasRelaciones(await _context.Categorias.ToListAsync(), r, (int)id);
            return View(i);
        }

        // GET: CategoriaPeliculas/Create
        public async Task<IActionResult> Create(int? idpel, int? idcat)
        {
            if (!acceso()) return NotFound();
            if (idpel == null || idcat == null) return NotFound();
            var pel = await _context.Peliculas.FirstAsync(i => i.Id == idpel);
            var cat = await _context.Categorias.FirstAsync(i => i.Id == idcat);
            if (cat == null || pel == null) return NotFound();
            CategoriaPelicula cp = new CategoriaPelicula();
            cp.categoria = cat;
            cp.pelicula = pel;
            if (TryValidateModel(cp))
            {
                _context.Add(cp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AsignarCategoria), new { id = idpel });
        }

        public async Task<IActionResult> QuitarCategoria(int? idpel, int? idcat)
        {
            if (!acceso()) return NotFound();
            if (idpel == null || idcat == null) return NotFound();

            var categoriaPelicula = _context.RelacionesCategoriaPelicula.
                Where(p => p.pelicula.Id == idpel && p.categoria.Id == idcat).
                First();
            _context.RelacionesCategoriaPelicula.Remove(categoriaPelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AsignarCategoria), new { id = idpel });
        }

        // POST: CategoriaPeliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] CategoriaPelicula categoriaPelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaPelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaPelicula);
        }

        // GET: CategoriaPeliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaPelicula = await _context.RelacionesCategoriaPelicula.FindAsync(id);
            if (categoriaPelicula == null)
            {
                return NotFound();
            }
            return View(categoriaPelicula);
        }

        // POST: CategoriaPeliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] CategoriaPelicula categoriaPelicula)
        {
            if (id != categoriaPelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaPelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaPeliculaExists(categoriaPelicula.Id))
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
            return View(categoriaPelicula);
        }

        // GET: CategoriaPeliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaPelicula = await _context.RelacionesCategoriaPelicula
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaPelicula == null)
            {
                return NotFound();
            }

            return View(categoriaPelicula);
        }

        // POST: CategoriaPeliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaPelicula = await _context.RelacionesCategoriaPelicula.FindAsync(id);
            _context.RelacionesCategoriaPelicula.Remove(categoriaPelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaPeliculaExists(int id)
        {
            return _context.RelacionesCategoriaPelicula.Any(e => e.Id == id);
        }
    }
}
