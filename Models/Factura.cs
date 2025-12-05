using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdPersona { get; set; }

    public int? IdTipoPago { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime FechaPago { get; set; }

    public string EstadoFactura { get; set; } = null!;

    public virtual ICollection<Estadium> Estadia { get; set; } = new List<Estadium>();

    public virtual Cliente? IdPersonaNavigation { get; set; }

    public virtual Tarjetum? IdTipoPago1 { get; set; }

    public virtual Efectivo? IdTipoPagoNavigation { get; set; }
}
