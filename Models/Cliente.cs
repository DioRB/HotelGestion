using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Cliente
{
    public int IdPersona { get; set; }

    public string? Documento { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string TipoCliente { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Servicio> IdServicios { get; set; } = new List<Servicio>();
}
