using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelGestion.Models;
using HotelGestion.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;

namespace HotelGestion.Controllers
{
    
    public class HabitacionesController : Controller
    {
        private readonly GestionHotelContext _context;

        public HabitacionesController(GestionHotelContext context)
        {
            _context = context;
        }

        // GET: Habitaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Habitacions.ToListAsync());
        }

        // GET: Habitaciones/Buscar
        public async Task<IActionResult> Buscar(int? piso, string? tipo, string? estado)
        {
            var viewModel = new HabitacionFiltroViewModel();

            // Cargar opciones para los filtros
            viewModel.PisosDisponibles = await _context.Habitacions
                .Select(h => h.Piso)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            viewModel.TiposDisponibles = await _context.Habitacions
                .Select(h => h.TipoHabitacion)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();

            viewModel.EstadosDisponibles = await _context.Habitacions
                .Select(h => h.EstadoHabitacion)
                .Distinct()
                .OrderBy(t => t)
                .ToListAsync();


            // Si hay filtros aplicados, ejecutar abúsqueda
            if (piso.HasValue || !string.IsNullOrEmpty(tipo) || !string.IsNullOrEmpty(estado))
            {
                viewModel.Piso = piso;
                viewModel.Tipo = tipo;
                viewModel.Estado = estado;

                // OPCIÓN 1: Consulta SQL cruda (para demostrar conocimientos SQL)
                var resultados = await BuscarConSQL(piso, tipo, estado);
                viewModel.Resultados = resultados;
            }

            return View(viewModel);
        }

        // MÉTODO CON SQL CRUDO - Demuestra conocimientos en SQL
        private async Task<List<Habitacion>> BuscarConSQL(int? piso, string? tipo, string? estado)
        {
            // Construir la consulta SQL dinámicamente
            var query = @"
            SELECT * FROM HABITACION 
            WHERE 1=1";

            var parametros = new List<SqlParameter>();

            if (piso.HasValue)
            {
                query += " AND Piso = @Piso";
                parametros.Add(new SqlParameter("@Piso", piso.Value));
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                query += " AND TIPO_HABITACION = @Tipo";
                parametros.Add(new SqlParameter("@Tipo", tipo));
            }

            if (!string.IsNullOrEmpty(estado))
            {
                query += " AND ESTADO_HABITACION = @Estado";
                parametros.Add(new SqlParameter("@Estado", estado));
            }

            query += " ORDER BY Piso, Numero";

            // Ejecutar consulta SQL cruda
            var resultados = await _context.Habitacions
                .FromSqlRaw(query, parametros.ToArray())
                .ToListAsync();

            return resultados;
        }

        // GET: Habitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacions
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // GET: Habitaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habitaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHabitacion,Numero,Piso,TipoHabitacion,Capacidad,PrecioNoche,EstadoHabitacion")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(habitacion);
        }

        // GET: Habitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacions.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            return View(habitacion);
        }

        // POST: Habitaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHabitacion,Numero,Piso,TipoHabitacion,Capacidad,PrecioNoche,EstadoHabitacion")] Habitacion habitacion)
        {
            if (id != habitacion.IdHabitacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionExists(habitacion.IdHabitacion))
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
            return View(habitacion);
        }

        // GET: Habitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacions
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitacion = await _context.Habitacions.FindAsync(id);
            if (habitacion != null)
            {
                _context.Habitacions.Remove(habitacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitacions.Any(e => e.IdHabitacion == id);
        }
    }
}
