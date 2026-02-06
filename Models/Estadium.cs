using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Estadium
{
    public int IdEstadia { get; set; }

    public int? IdReserva { get; set; }

    public DateTime? FechaCheckIn { get; set; }

    public DateTime? FechaCheckOut { get; set; }

    public int CantidadPersonas { get; set; }

    public string? Observaciones { get; set; }

    public string EstadoEstadia { get; set; } = null!;

    public virtual ICollection<EstadiaServicio> EstadiaServicios { get; set; } = new List<EstadiaServicio>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Reserva? IdReservaNavigation { get; set; }
}
