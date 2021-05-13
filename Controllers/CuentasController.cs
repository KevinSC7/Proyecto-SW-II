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
    public class CuentasController : Controller
    {
        private readonly AplicationDBContext _context;

        public CuentasController(AplicationDBContext context)
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

        // GET: Cuentas
        public async Task<IActionResult> Index(string searchString, string check1, string check2)
        {
            if (!acceso()) return NotFound();
            var state = true;
            if(!String.IsNullOrEmpty(check1) && !String.IsNullOrEmpty(check2))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    var cuentas = _context.Cuentas.Include(u => u.Miusuario).Where(s => s.Nombre.Contains(searchString));
                    return View(await cuentas.ToListAsync());
                }
                else
                {
                    return View(await _context.Cuentas.Include(u => u.Miusuario).ToListAsync());
                }
            }else if (!String.IsNullOrEmpty(check1))
            {
                state = true;
            }
            else if (!String.IsNullOrEmpty(check2))
            {
                state = false;
            }
            else
            {
                return View(await _context.Cuentas.Include(u => u.Miusuario).ToListAsync());
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                var cuentas = _context.Cuentas.Include(u => u.Miusuario).Where(s => s.Nombre.Contains(searchString) && s._Estado == state);
                return View(await cuentas.ToListAsync());
            }
            else
            {
                return View(await _context.Cuentas.Include(u => u.Miusuario).Where(s => s._Estado == state).ToListAsync());
            }

        }

        public async Task<Cuenta> getCuentaById(int? id)
        {
            return await _context.Cuentas.Include(u => u.Miusuario).FirstOrDefaultAsync(m => m.Id == id);
        }

        // GET: Cuentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }
            ViewData["estado"] = cuenta.Estado;
            return View(cuenta);
        }

        // GET: Cuentas/Create
        public IActionResult Create()
        {
            if (!acceso()) return NotFound();
            return View();
        }

        // POST: Cuentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Contraseña,_Estado")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuenta);
        }

        // GET: Cuentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }
            ViewData["estado"] = cuenta.Estado;
            ViewData["nombre"] = cuenta.Nombre;
            return View(cuenta);
        }

        // POST: Cuentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cuenta cuenta)
        {
            ViewData["estado"] = cuenta._Estado;
            if (id != cuenta.Id)
            {
                return NotFound();
            }
            var c = await this.getCuentaById(cuenta.Id);
            if(c == null) return NotFound();
            if (!String.IsNullOrEmpty(cuenta.Contraseña))
            {
                if (cuenta.Contraseña.Length < 8)
                {
                    ViewData["errorcontra"] = "Debe tener al menos 8 caracteres";
                    return View(cuenta);
                }
                c.Contraseña = cuenta.Contraseña;
            }
            
            c._Estado = cuenta._Estado;
            ModelState.Clear();

            if (TryValidateModel(c))
            {
                _context.Update(c);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!acceso()) return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentaExists(int id)
        {
            return _context.Cuentas.Any(e => e.Id == id);
        }
    }
}
