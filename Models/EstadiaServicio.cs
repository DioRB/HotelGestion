using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class EstadiaServicio
{
    public int IdEstadia { get; set; }

    public int IdServicio { get; set; }

    public DateTime? FechaServicio { get; set; }

    public virtual Estadium IdEstadiaNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
