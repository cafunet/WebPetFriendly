using System;
using System.Collections.Generic;

namespace WebPetFriendly.Models;

public partial class Veterinarium
{
    public long Id { get; set; }

    public string Nit { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public int? Empleados { get; set; }

    public DateOnly? FechaFundacion { get; set; }
}
