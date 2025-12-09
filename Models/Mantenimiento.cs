using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Mantenimiento
{
    public int IdMantenimiento { get; set; }

    public int? IdPersona { get; set; }

    public DateTime FechaReporte { get; set; }

    public DateTime FechaMantenimiento { get; set; }

    public string Motivo { get; set; } = null!;

    public decimal CostoMantenimiento { get; set; }
}
