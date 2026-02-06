using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Efectivo
{
    public int IdEfectivo { get; set; }

    public decimal MontoEntregado { get; set; }

    public decimal Cambio { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
