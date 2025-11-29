using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdPersona { get; set; }

    public int? IdTipoPago { get; set; }

    public string ValorTotal { get; set; } = null!;

    public DateOnly FechaPago { get; set; }

    public string EstadoFactura { get; set; } = null!;

    public virtual Cliente? IdPersonaNavigation { get; set; }

    public virtual Tarjetum? IdTipoPago1 { get; set; }

    public virtual Efectivo? IdTipoPagoNavigation { get; set; }

    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();
}
