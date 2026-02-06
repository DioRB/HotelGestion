using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class EmpleadoServicio
{
    public int IdServicio { get; set; }

    public int IdPersona { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Empleado IdPersonaNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
