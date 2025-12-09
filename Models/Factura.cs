using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdPersona { get; set; }

    public int? IdTipoPagoEfectivo { get; set; }

    public int? IdTipoPagoTarjeta { get; set; }

    public int? IdEstadia { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime FechaPago { get; set; }

    public string EstadoFactura { get; set; } = null!;
}
