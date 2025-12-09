using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int? IdPersona { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = null!;
}
