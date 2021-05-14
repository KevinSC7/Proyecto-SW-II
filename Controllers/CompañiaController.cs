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
    public class CompañiaController : Controller
    {
        private readonly AplicationDBContext _context;

        public CompañiaController(AplicationDBContext context)
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
        public async Task<Compañia> getCompañiaById(int? id)
        {
            return await _context.Compañias.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Compañia>> getLista()
        {
            return await _context.Compañias.ToListAsync();
        }
        // GET: Compañia
        public async Task<IActionResult> Index(string searchString)
        {         
            if(!acceso())return NotFound();
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.Compañias.Where(c => c.Nombre.Contains(searchString)).ToListAsync());
            }
            return View(await _context.Compañias.ToListAsync());
        }

        // GET: Compañia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var compañia = await _context.Compañias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compañia == null)
            {
                return NotFound();
            }

            return View(compañia);
        }

        // GET: Compañia/Create
        public IActionResult Create()
        {
            if (!acceso()) return NotFound();
            return View();
        }

        // POST: Compañia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Compañia compañia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compañia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compañia);
        }

        // GET: Compañia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var compañia = await _context.Compañias.FindAsync(id);
            if (compañia == null)
            {
                return NotFound();
            }
            return View(compañia);
        }

        // POST: Compañia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Compañia compañia)
        {
            if (id != compañia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compañia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompañiaExists(compañia.Id))
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
            return View(compañia);
        }

        // GET: Compañia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var compañia = await _context.Compañias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compañia == null)
            {
                return NotFound();
            }

            return View(compañia);
        }

        // POST: Compañia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compañia = await _context.Compañias.FindAsync(id);
            _context.Compañias.Remove(compañia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompañiaExists(int id)
        {
            return _context.Compañias.Any(e => e.Id == id);
        }
    }
}
