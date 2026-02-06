using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int? IdEstadia { get; set; }

    public int? IdPersona { get; set; }

    public int? IdTarjeta { get; set; }

    public int? IdEfectivo { get; set; }

    public decimal ValorTotal { get; set; }

    public DateTime? FechaPago { get; set; }

    public string EstadoFactura { get; set; } = null!;

    public virtual Efectivo? IdEfectivoNavigation { get; set; }

    public virtual Estadium? IdEstadiaNavigation { get; set; }

    public virtual Cliente? IdPersonaNavigation { get; set; }

    public virtual Tarjetum? IdTarjetaNavigation { get; set; }
}
