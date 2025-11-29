using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Estacionamiento
{
    public string IdEstacionamiento { get; set; } = null!;

    public int? IdReserva { get; set; }

    public string NumeroAsignado { get; set; } = null!;

    public string Disponible { get; set; } = null!;

    public decimal CostoEstacionamiento { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }
}
