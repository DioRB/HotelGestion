using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Estacionamiento
{
    public int IdEstacionamiento { get; set; }

    public int? IdEstadia { get; set; }

    public int? IdReserva { get; set; }

    public int NumeroAsignado { get; set; }

    public short Disponible { get; set; }

    public decimal CostoEstacionamiento { get; set; }

    public virtual Reserva? Reserva { get; set; }
}
