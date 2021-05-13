using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: CategoriaPeliculas
        public async Task<IActionResult> Index()
        {
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

        // GET: CategoriaPeliculas/Create
        public IActionResult Create()
        {
            return View();
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
