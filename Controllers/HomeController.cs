using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Proyecto_SW_II.Models;
using Proyecto_SW_II.Data;
using Microsoft.AspNetCore.Http;

namespace Proyecto_SW_II.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, AplicationDBContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Usuario usuario, Cuenta cuenta)
        {
            var r=await _context.Roles.FindAsync(2);
            
            usuario.Mirol = r;
            cuenta.Miusuario = usuario;//NUEVA
            int años=(int)DateTime.Now.Subtract(usuario.FechaNacimiento).TotalDays / 365;
            if (años >= 18)
            {
                usuario.Edad = años;
                _context.Add(usuario);
                _context.Add(cuenta);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("NombreSession", cuenta.Nombre);
                HttpContext.Session.SetInt32("ID", cuenta.Id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //error tu no tienes 18
                return RedirectToAction(nameof(Registro));
            }
            
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Cuenta cuenta)
        {
            var miCuenta = await _context.Cuentas.FindAsync(cuenta);

            if (miCuenta == null)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                HttpContext.Session.SetString("NombreSession", cuenta.Nombre);
                HttpContext.Session.SetInt32("ID", cuenta.Id);
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("NombreSession");
            HttpContext.Session.Remove("ID");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
