using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelGestion.Models;

[Table("Usuario")]
public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Contraseña { get; set; }
}
