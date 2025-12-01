using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using HotelGestion.Data;
using HotelGestion.Models;

namespace HotelGestion.Controllers
{

    public class AccountController : Controller
    {
        private readonly GestionHotelContext _context;

        public AccountController(GestionHotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Buscar el usuario en tu tabla (cambia "Usuarios" por el nombre de tu tabla)
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Nombre == model.Nombre && u.Contraseña == model.Contraseña);

                if (usuario != null)
                {
                    // Crear los claims (información del usuario)
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Iniciar sesión
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
