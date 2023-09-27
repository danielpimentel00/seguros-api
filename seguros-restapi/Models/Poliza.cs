using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class Poliza
{
    public string Codigo { get; set; } = null!;

    public int IdSolicitud { get; set; }

    public virtual Solicitude IdSolicitudNavigation { get; set; } = null!;
}
