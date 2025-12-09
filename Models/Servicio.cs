using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Tarifa { get; set; }

    public string NombreServicio { get; set; } = null!;
}
