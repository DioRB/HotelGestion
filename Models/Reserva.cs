using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? IdPersona { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Estacionamiento> Estacionamientos { get; set; } = new List<Estacionamiento>();

    public virtual Cliente? IdPersonaNavigation { get; set; }
}
