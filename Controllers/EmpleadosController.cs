using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelGestion.Data;
using HotelGestion.Models;

namespace HotelGestion.Controllers_
{
    public class EmpleadosController : Controller
    {
        private readonly GestionHotelContext _context;

        public EmpleadosController(GestionHotelContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var empleados = await _context.Empleados
                .Include(e => e.IdTurnoNavigation)
                .OrderBy(e => e.Documento)
                .ThenBy(e => e.Nombre)
                .ThenBy(e => e.Apellido) 
                .ToListAsync();

            return View(empleados);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdTurnoNavigation)
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public async Task<IActionResult> Create()
        {
            var turnos = await _context.Turnos
                .Select(t => new {
                    t.IdTurno,
                    // Cambia esto según las columnas de tu tabla Turno
                    Descripcion = $"Turno {t.TipoTurno}" // o t.Nombre, t.HoraInicio, etc.
                })
                .ToListAsync();

            ViewData["IdTurno"] = new SelectList(turnos, "IdTurno", "Descripcion");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTurno,Documento,Nombre,Apellido,Sexo,Telefono,Correo,Cargo,Salario,FechaContratacion")] Empleado empleado)
        {

            ModelState.Remove("IdTurnoNavigation");
            ModelState.Remove("EmpleadoServicios");
            ModelState.Remove("Mantenimientos");

            // Ver errores de validación
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"❌ Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Empleado creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Excepción: {ex.Message}");
                    ModelState.AddModelError("", $"Error: {ex.InnerException?.Message ?? ex.Message}");
                }
            }

            // Recargar turnos si falla
            ViewData["IdTurno"] = new SelectList(_context.Turnos, "IdTurno", "IdTurno", empleado.IdTurno);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdTurno"] = new SelectList(_context.Turnos, "IdTurno", "IdTurno", empleado.IdTurno);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPersona,IdTurno,Documento,Nombre,Apellido,Sexo,Telefono,Correo,Cargo,Salario,FechaContratacion")] Empleado empleado)
        {
            if (id != empleado.IdPersona)
            {
                return NotFound();
            }

            ModelState.Remove("IdTurnoNavigation");
            ModelState.Remove("EmpleadoServicios");
            ModelState.Remove("Mantenimientos");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdPersona))
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
            ViewData["IdTurno"] = new SelectList(_context.Turnos, "IdTurno", "IdTurno", empleado.IdTurno);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.IdTurnoNavigation)
                .FirstOrDefaultAsync(m => m.IdPersona == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.IdPersona == id);
        }
    }
}
