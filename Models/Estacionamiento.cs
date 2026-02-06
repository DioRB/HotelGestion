using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Estacionamiento
{
    public int IdEstacionamiento { get; set; }

    public int? IdReserva { get; set; }

    public int NumeroAsignado { get; set; }

    public string Disponibilidad { get; set; } = null!;

    public decimal CostoEstacionamiento { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }
}
