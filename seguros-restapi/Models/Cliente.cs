using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class Cliente
{
    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public DateTime FechaNac { get; set; }

    public int IdTipoCuenta { get; set; }

    public string NumeroCuenta { get; set; } = null!;

    public virtual TipoCuenta IdTipoCuentaNavigation { get; set; } = null!;

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();
}
