using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Empleado
{
    public int IdPersona { get; set; }

    public int IdTurno { get; set; }

    public string? Documento { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string Cargo { get; set; } = null!;

    public decimal Salario { get; set; }

    public DateOnly FechaContratacion { get; set; }

    public virtual Turno IdTurnoNavigation { get; set; } = null!;

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();
}
