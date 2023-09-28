using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using seguros_restapi.DTO;
using seguros_restapi.Models;
using System.Runtime.CompilerServices;

namespace seguros_restapi.Services
{
    public class SolicitudService
    {
        private readonly SegurosContext context = new();

        public async Task<SolicitudDto> CreateSolicitud(SolicitudDto model)
        {
            IDbContextTransaction transaction = null;
            try
            {
                //obtener cliente por el codigo de cliente
                var customer = await context.Clientes.Where(x => x.Codigo == model.CedulaCliente).FirstOrDefaultAsync();
                Cliente customer2 = new Cliente
                {
                    Codigo = model.CedulaCliente,
                    FechaNac = model.FechaNacCliente,
                    IdTipoCuenta = model.IdTipoCuenta,
                    Nombre = model.NombreCliente,
                    NumeroCuenta = model.NumeroCuenta,
                    PrimerApellido = model.PrimerApellidoCliente,
                    SegundoApellido = model.SegundoApellidoCliente,
                };

                transaction = context.Database.BeginTransaction();

                //si no existe, lo creo
                if (customer == null)
                {
                    await context.Clientes.AddAsync(customer2);
                } else
                {
                    customer.FechaNac = model.FechaNacCliente;
                    customer.IdTipoCuenta = model.IdTipoCuenta;
                    customer.Nombre = model.NombreCliente;
                    customer.NumeroCuenta = model.NumeroCuenta;
                    customer.PrimerApellido = model.PrimerApellidoCliente;
                    customer.SegundoApellido = model.SegundoApellidoCliente;

                    context.Clientes.Update(customer); //si existe, actualizo su data
                }

                await context.SaveChangesAsync();

                //valido si procede la solicitud
                var validatedRequest = await ValidateSolicitud(model);

                //si procede
                if (validatedRequest.CodigoEstado == (int)Utils.EstadosSolicitud.Aprobado)
                {
                    var polizasCliente = await context.Polizas
                        .Join(context.Solicitudes,
                        poliza => poliza.IdSolicitud,
                        solicitud => solicitud.Id,
                        (poliza, solicitud) => new
                        {
                            solicitud.CodigoCliente,
                            CodigoPoliza = poliza.Codigo,
                            IdSolicitud = solicitud.Id,
                        })
                        .Where(x => x.CodigoCliente == validatedRequest.CedulaCliente)
                        .ToListAsync();

                    //y el cliente ya tiene una poliza, elimino dicha poliza
                    if (polizasCliente.Count > 0)
                    {
                        var polizasAEliminar = polizasCliente
                            .Select(x => new Poliza
                            {
                                Codigo = x.CodigoPoliza,
                                IdSolicitud = x.IdSolicitud
                            })
                            .ToList();

                        context.Polizas.RemoveRange(polizasAEliminar);
                        await context.SaveChangesAsync();
                    }

                    //creo la solicitud con estado aprobada
                    var solicitud = (await context.Solicitudes.AddAsync(new Solicitude
                    {
                        CodigoEstado = validatedRequest.CodigoEstado,
                        CodigoCliente = validatedRequest.CedulaCliente,
                        CodigoPlan = validatedRequest.CodigoPlan,
                        CodigoSeguro = validatedRequest.CodigoSeguro
                    })).Entity;

                    await context.SaveChangesAsync();

                    //y creo la nueva poliza
                    var poliza = (await context.Polizas.AddAsync(new Poliza
                    {
                        Codigo = $"{validatedRequest.CodigoSeguro}-{validatedRequest.CodigoPlan}-{GetNextSequenceNumber()}",
                        IdSolicitud = solicitud.Id,
                        FechaVenta = DateTime.Now
                    })).Entity;

                    validatedRequest.NumeroPoliza = poliza.Codigo;
                }
                else //si no procede, creo la solicitud con estado rechazada
                {
                    await context.Solicitudes.AddAsync(new Solicitude
                    {
                        CodigoEstado = validatedRequest.CodigoEstado,
                        CodigoCliente = validatedRequest.CedulaCliente,
                        CodigoPlan = validatedRequest.CodigoPlan,
                        CodigoSeguro = validatedRequest.CodigoSeguro
                    });
                }

                await context.SaveChangesAsync();

                var plan = await context.Planes
                    .Where(x => x.Codigo == validatedRequest.CodigoPlan)
                    .FirstOrDefaultAsync();

                if(plan != null)
                {
                    validatedRequest.Cuota = plan.Cuota;
                }

                transaction.Commit();

                return validatedRequest;
            }
            catch (Exception)
            {
                transaction?.Rollback();
                throw;
            }
        }

        private async Task<SolicitudDto> ValidateSolicitud(SolicitudDto request)
        {
            try
            {
                //verificar el tipo de cuenta
                //si no es permitida, retorno con estado rechazado y mensaje
                //verifico el rango de edad
                //si no esta en el rango, retorno con estado rechazado y mensaje
                //de lo contrario, retorno con estado aprobado

                var res = await context.Clientes
                    .Where(x => x.Codigo == request.CedulaCliente)
                    .Join(
                        context.ProductosPermitidos,
                        cliente => cliente.IdTipoCuenta,
                        productoPermitido => productoPermitido.IdTipoCuenta,
                        (cliente, productoPermitido) => new
                        {
                            productoPermitido.CodigoSeguro
                        })
                    .ToListAsync();

                bool allowedProduct = res.Any(item => item.CodigoSeguro == request.CodigoSeguro);

                if (!allowedProduct)
                {
                    request.CodigoEstado = (int)Utils.EstadosSolicitud.Rechazado;
                    request.RejectionReason = "Tipo de cuenta no permitida";

                    return request;
                }

                var customerAge = GetAge(request.FechaNacCliente);

                var plans = await context.Planes
                    .Where(x => customerAge >= x.MinEdad && customerAge <= x.MaxEdad && x.Codigo == request.CodigoPlan)
                    .ToListAsync();

                if(plans.Count == 0)
                {
                    request.CodigoEstado = (int)Utils.EstadosSolicitud.Rechazado;
                    request.RejectionReason = "Edad fuera del rango permitido";

                    return request;
                } 

                request.CodigoEstado = (int)Utils.EstadosSolicitud.Aprobado;
                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // Resta un año si aún no ha pasado el cumpleaños de este año.
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        private string GetNextSequenceNumber()
        {
            var codigosPolizas = context.Polizas.Select(p => p.Codigo).ToList();

            if (codigosPolizas.Count == 0)
            {
                return 1.ToString("D5");
            }

            int maxLastFiveDigits = codigosPolizas
                .Select(codigo => int.Parse(codigo.Substring(codigo.Length - 5)))
                .Max();

            int siguienteNumeroSecuencia = maxLastFiveDigits + 1;

            string numeroSecuenciaFormateado = siguienteNumeroSecuencia.ToString("D5");

            return numeroSecuenciaFormateado;
        }
    }
}
