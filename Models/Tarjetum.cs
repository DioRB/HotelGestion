using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Tarjetum
{
    public int IdTipoPago { get; set; }

    public int IdTarjeta { get; set; }

    public string NumeroTarjeta { get; set; } = null!;

    public string TipoTarjeta { get; set; } = null!;

    public string BancoEmisor { get; set; } = null!;
}
