using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class Solicitude
{
    public int Id { get; set; }

    public string CodigoCliente { get; set; } = null!;

    public string CodigoSeguro { get; set; } = null!;

    public string CodigoPlan { get; set; } = null!;

    public int CodigoEstado { get; set; }

    public virtual Cliente CodigoClienteNavigation { get; set; } = null!;

    public virtual EstadosSolicitud CodigoEstadoNavigation { get; set; } = null!;

    public virtual Plane CodigoPlanNavigation { get; set; } = null!;

    public virtual Seguro CodigoSeguroNavigation { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
