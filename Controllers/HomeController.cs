﻿using System;
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
using System.Text.RegularExpressions;

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
            IntermedioCuentaUsuarioRol datos = new IntermedioCuentaUsuarioRol(usuario, cuenta, null);
            //ModelState.Clear();

            if (!TryValidateModel(datos))
            {
                return View(datos);
            }
            
            if (_context.Cuentas.Any(c => c.Nombre == cuenta.Nombre)) return View(datos);
            var r=await _context.Roles.FindAsync(2);
            
            usuario.Mirol = r;
            cuenta.Miusuario = usuario;
            int años=(int)DateTime.Now.Subtract(usuario.FechaNacimiento).TotalDays / 365;
            if (años >= 18)
            {
                _context.Add(usuario);
                _context.Add(cuenta);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("NombreSession", cuenta.Nombre);
                HttpContext.Session.SetInt32("ID", cuenta.Id);
                HttpContext.Session.SetString("TipoUsuario", "Cliente");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //error tu no tienes 18
                ViewData["mensaje"] = "Debes de ser mayor de edad para registrarte";
                return View(datos);
            }
            
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Nombre, string Contraseña)
        {
            var miCuenta =_context.Cuentas.FirstOrDefault(n => n.Nombre == Nombre && n.Contraseña == Contraseña);
            
            CuentasController cc = new CuentasController(_context);
            UsuariosController uc = new UsuariosController(_context);
            miCuenta = await cc.getCuentaById(miCuenta.Id);
            var user = await uc.getUsuarioById(miCuenta.Miusuario.Id);

            if (miCuenta == null)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                HttpContext.Session.SetString("NombreSession", Nombre);
                HttpContext.Session.SetInt32("ID", miCuenta.Id);
                if(user.Mirol.TipoUsuario == 'A')
                {
                    HttpContext.Session.SetString("TipoUsuario", "Administrador");
                }
                else
                {
                    HttpContext.Session.SetString("TipoUsuario", "Cliente");
                }
                
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("NombreSession");
            HttpContext.Session.Remove("ID");
            HttpContext.Session.Remove("TipoUsuario");
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> MisDatos()
        {
            CuentasController cc = new CuentasController(_context);
            
            var cuenta = await cc.getCuentaById(HttpContext.Session.GetInt32("ID"));

            if (cuenta == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var user = await _context.Usuarios.FindAsync(cuenta.Miusuario.Id);
            if(user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            IntermedioCuentaUsuarioRol datos = new IntermedioCuentaUsuarioRol(user, cuenta, null);
            return View(datos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MisDatos(Usuario usuario, Cuenta cuenta)
        {
            IntermedioCuentaUsuarioRol datos = new IntermedioCuentaUsuarioRol(usuario, cuenta, null);
            if (!TryValidateModel(datos))
            {
                View(datos);
            }
            _context.Update(usuario);
            _context.Update(cuenta);
            await _context.SaveChangesAsync();
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
