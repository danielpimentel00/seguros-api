﻿namespace seguros_restapi.DTO
{
    public class SolicitudDto
    {
        public string CodigoCliente { get; set; } = null!;

        public string NombreCliente { get; set; } = null!;

        public string PrimerApellidoCliente { get; set; } = null!;

        public string? SegundoApellidoCliente { get; set; }

        public DateTime FechaNacCliente { get; set; }

        public int IdTipoCuenta { get; set; }

        public string NumeroCuenta { get; set; } = null!;

        public string CodigoSeguro { get; set; } = null!;

        public string CodigoPlan { get; set; } = null!;
    }
}