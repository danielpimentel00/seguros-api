using Microsoft.EntityFrameworkCore;
using seguros_restapi.DTO;
using seguros_restapi.Models;

namespace seguros_restapi.Services
{
    public class CommonService
    {
        private readonly SegurosContext context = new();

        public async Task<List<SegurosDto>> GetSeguros()
        {
            try
            {
                var result = await context.Seguros
                    .Select(x => new SegurosDto
                    {
                        Codigo = x.Codigo,
                        Nombre = x.Nombre
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PlanDto>> GetPlanes()
        {
            try
            {
                var result = await context.Planes
                    .Select(x => new PlanDto
                    {
                        Nombre = x.Nombre,
                        Codigo = x.Codigo,
                        Cuota = x.Cuota,
                        MaxEdad = x.MaxEdad,
                        MinEdad = x.MinEdad,
                        CodigoSeguro = x.CodigoSeguro
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EstadoSolicitudDto>> GetEstadosSolicitud()
        {
            try
            {
                var result = await context.EstadosSolicituds
                    .Select(x => new EstadoSolicitudDto
                    {
                        Nombre = x.Nombre,
                        Id = x.Id,
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TipoCuentaDto>> GetTiposdeCuentas()
        {
            try
            {
                var result = await context.TipoCuentas
                    .Select(x => new TipoCuentaDto
                    {
                        Nombre = x.Nombre,
                        Id = x.Id,
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
