using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Estadium
{
    public int IdEstadia { get; set; }

    public int IdFactura { get; set; }

    public DateTime FechaCheckIn { get; set; }

    public DateTime DechaCheckOut { get; set; }

    public short CantidadPersonas { get; set; }

    public string Observaciones { get; set; } = null!;

    public string EstadoEstadia { get; set; } = null!;

    public virtual ICollection<EstadiaServicio> EstadiaServicios { get; set; } = new List<EstadiaServicio>();

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
