using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Tarifa { get; set; } = null!;

    public string NombreServicio { get; set; } = null!;

    public virtual ICollection<Factura> IdFacturas { get; set; } = new List<Factura>();

    public virtual ICollection<Habitacion> IdHabitacions { get; set; } = new List<Habitacion>();

    public virtual ICollection<Cliente> IdPersonas { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> IdPersonasNavigation { get; set; } = new List<Empleado>();
}
