namespace seguros_restapi.DTO
{
    public class ClienteDto
    {
        public string Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string PrimerApellido { get; set; } = null!;

        public string? SegundoApellido { get; set; }

        public DateTime FechaNac { get; set; }

        public string TipoCuenta { get; set; } = null!;

        public string NumeroCuenta { get; set; } = null!;
    }
}
