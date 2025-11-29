using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Efectivo
{
    public int IdTipoPago { get; set; }

    public string? NombreTipo { get; set; }

    public decimal Attribute43 { get; set; }

    public decimal? Cambio { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
