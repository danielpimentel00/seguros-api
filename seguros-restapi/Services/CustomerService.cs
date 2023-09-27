using Microsoft.EntityFrameworkCore;
using seguros_restapi.DTO;
using seguros_restapi.Models;

namespace seguros_restapi.Services
{
    public class CustomerService
    {
		private readonly SegurosContext context = new();

        public async Task<ClienteDto> GetCustomerById(string userCode)
        {
			try
			{
				var result = await context.Clientes
					.Join(
						context.TipoCuentas,
						cliente => cliente.IdTipoCuenta,
						tipoCuenta => tipoCuenta.Id,
						(cliente, tipoCuenta) => new ClienteDto
						{
							Codigo = cliente.Codigo,
							Nombre = cliente.Nombre,
							PrimerApellido = cliente.PrimerApellido,
							SegundoApellido = cliente.SegundoApellido,
							FechaNac = cliente.FechaNac,
							TipoCuenta = tipoCuenta.Nombre,
							NumeroCuenta = cliente.NumeroCuenta
						})
                    .Where(clienteDto => clienteDto.Codigo == userCode)
					.FirstOrDefaultAsync();

				if (result == null)
				{
					throw new Exception("Usuario no encontrado");
				}

                return result;
				
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
