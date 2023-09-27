using System;
using System.Collections.Generic;

namespace seguros_restapi.Models;

public partial class ProductosPermitido
{
    public int Id { get; set; }

    public string CodigoSeguro { get; set; } = null!;

    public int IdTipoCuenta { get; set; }

    public virtual Seguro CodigoSeguroNavigation { get; set; } = null!;

    public virtual TipoCuenta IdTipoCuentaNavigation { get; set; } = null!;
}
