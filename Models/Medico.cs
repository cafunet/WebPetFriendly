using System;
using System.Collections.Generic;

namespace WebPetFriendly.Models;

public partial class Medico
{
    public long IdMedico { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string Email { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string? Genero { get; set; }

    public string? Pais { get; set; }

    public string? Departamento { get; set; }

    public string? Ciudad { get; set; }

    public virtual ICollection<CitaMedica> CitaMedicas { get; set; } = new List<CitaMedica>();
}
