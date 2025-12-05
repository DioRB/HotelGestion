using System;
using HotelGestion.Models;
using System.Collections.Generic;

namespace HotelGestion.Models;
public partial class HabitacionFiltroViewModel
{
    // Filtros
    public int? Piso { get; set; }
    public string? Tipo { get; set; }
    public bool? Disponible { get; set; }

    // Resultados
    public List<Habitacion> Resultados { get; set; } = new List<Habitacion>();

    // Listas para los dropdowns
    public List<int> PisosDisponibles { get; set; } = new List<int>();
    public List<string> TiposDisponibles { get; set; } = new List<string>();
}