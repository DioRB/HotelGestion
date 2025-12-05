using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Habitacion
{
    public int IdHabitacion { get; set; }

    public int? IdEstadia { get; set; }

    public int? IdReserva { get; set; }

    public int Numero { get; set; }

    public int Piso { get; set; }

    public string TipoHabitacion { get; set; } = null!;

    public int Capacidad { get; set; }

    public decimal PrecioNoche { get; set; }

    public bool EstadoHabitacion { get; set; }

    public virtual Reserva? Reserva { get; set; }
}
