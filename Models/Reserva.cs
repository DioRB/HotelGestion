using System;
using System.Collections.Generic;

namespace HotelGestion.Models;

public partial class Reserva
{
    public int IdEstadia { get; set; }

    public int IdReserva { get; set; }

    public int? IdPersona { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Estacionamiento> Estacionamientos { get; set; } = new List<Estacionamiento>();

    public virtual ICollection<Habitacion> Habitacions { get; set; } = new List<Habitacion>();

    public virtual Estadium IdEstadiaNavigation { get; set; } = null!;

    public virtual Cliente? IdPersonaNavigation { get; set; }
}
