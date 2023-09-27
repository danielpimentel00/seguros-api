using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class Plane
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public decimal Cuota { get; set; }

    public int MinEdad { get; set; }

    public int MaxEdad { get; set; }

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();
}
