using System;
using System.Collections.Generic;

namespace WebPetFriendly.Models;

public partial class Mascotum
{
    public long IdMascota { get; set; }

    public string Nombre { get; set; } = null!;

    public string Especie { get; set; } = null!;

    public string? Raza { get; set; }

    public string? Color { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public string? Propietario { get; set; }

    public virtual ICollection<CitaMedica> CitaMedicas { get; set; } = new List<CitaMedica>();
}
