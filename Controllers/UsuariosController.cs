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
    public class UsuariosController : Controller
    {
        private readonly AplicationDBContext _context;

        public UsuariosController(AplicationDBContext context)
        {
            _context = context;
        }

        public Boolean acceso()
        {
            if (HttpContext.Session.GetString("TipoUsuario") == null)return false;
            if (HttpContext.Session.GetString("TipoUsuario") == "Cliente")return false;
            if (HttpContext.Session.GetString("TipoUsuario") == "Administrador") return true;
            return false;
        }

        // GET: Usuarios    no se accede, se hace atraves de la cuenta
        /*public async Task<IActionResult> Index()
        {
            if (!acceso()) return NotFound();
            return View(await _context.Usuarios.Include(r => r.Mirol).ToListAsync());
        }*/

        public async Task<Usuario> getUsuarioById(int? id)
        {
            return await _context.Usuarios.Include(u => u.Mirol).FirstOrDefaultAsync(m => m.Id == id);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!acceso()) return NotFound("en acceso");
            if (id == null)
            {
                return NotFound("id null");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
