using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Habitacion
{
    public int IdHabitacion { get; set; }

    public string Numero { get; set; } = null!;

    public string Piso { get; set; } = null!;

    public string TipoHabitacion { get; set; } = null!;

    public int Capacidad { get; set; }

    public string PrecioNoche { get; set; } = null!;

    public string EstadoHabitacion { get; set; } = null!;

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();
}
