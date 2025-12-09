using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Estadium
{
    public int IdEstadia { get; set; }

    public int IdReserva { get; set; }

    public DateTime FechaCheckIn { get; set; }

    public DateTime? FechaCheckOut { get; set; }

    public int CantidadPersonas { get; set; }

    public string Observaciones { get; set; } = null!;

    public string EstadoEstadia { get; set; } = null!;
}
