using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class Seguro
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ProductosPermitido> ProductosPermitidos { get; set; } = new List<ProductosPermitido>();

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();
}
