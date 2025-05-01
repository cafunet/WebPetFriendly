using System;
using System.Collections.Generic;

namespace WebPetFriendly.Models;

public partial class CitaMedica
{
    public long IdCita { get; set; }

    public long MedicoId { get; set; }

    public long MascotaId { get; set; }

    public DateTime FechaCita { get; set; }

    public string Sintomas { get; set; } = null!;

    public string Diagnostico { get; set; } = null!;

    public string? Formula { get; set; }

    public virtual Mascotum? Mascota { get; set; }

    public virtual Medico? Medico { get; set; }
}
