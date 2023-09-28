namespace seguros_restapi.DTO
{
    public class PlanDto
    {
        public string Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public decimal Cuota { get; set; }

        public int MinEdad { get; set; }

        public int MaxEdad { get; set; }

        public string? CodigoSeguro { get; set; }
    }
}
