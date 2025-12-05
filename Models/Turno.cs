using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public string TipoTurno { get; set; } = null!;

    public DateTime HoraInicio { get; set; }

    public DateTime HoraFin { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
