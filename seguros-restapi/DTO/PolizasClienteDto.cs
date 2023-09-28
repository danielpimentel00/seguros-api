namespace seguros_restapi.DTO
{
    public class PolizasClienteDto
    {
        public string CodigoSeguro { get; set; } = null!;

        public string NombreSeguro { get; set; } = null!;

        public string CodigoPoliza { get; set; } = null!;

        public string CodigoPlan { get; set; } = null!;

        public string NombrePlan { get; set; } = null!;

        public int IdTipoCuenta { get; set; }

        public string NombreTipoCuenta { get; set; } = null!;

        public string NumeroCuenta { get; set; } = null!;

        public DateTime FechaVenta { get; set; }

        public decimal Cuota { get; set; }
    }
}
