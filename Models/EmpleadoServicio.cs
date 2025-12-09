using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class EmpleadoServicio
{
    public int IdPersona { get; set; }

    public int IdServicio { get; set; }

    public DateTime Fecha { get; set; }
}
