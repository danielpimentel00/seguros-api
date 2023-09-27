using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class TipoCuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<ProductosPermitido> ProductosPermitidos { get; set; } = new List<ProductosPermitido>();
}
