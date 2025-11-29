using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string TipoTurno { get; set; } = null!;

    public string HoraInicio { get; set; } = null!;

    public string HoraFin { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
