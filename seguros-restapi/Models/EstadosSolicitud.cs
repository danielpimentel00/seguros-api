using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class EstadosSolicitud
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();
}
