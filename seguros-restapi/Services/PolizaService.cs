using Microsoft.EntityFrameworkCore;
using seguros_restapi.DTO;
using seguros_restapi.Models;
using System.Numerics;

namespace seguros_restapi.Services
{
    public class PolizaService
    {
        private readonly SegurosContext context = new();

        public async Task<List<PolizasClienteDto>> GetCustomerPolizas(string customerCode)
        {
            try
            {
                var result = await context.Polizas
                    .Join(
                        context.Solicitudes,
                        poliza => poliza.IdSolicitud,
                        solicitud => solicitud.Id,
                        (poliza, solicitud) => new
                        {
                            PolizaCodigo = poliza.Codigo,
                            ClienteCodigo = solicitud.CodigoCliente,
                            SeguroCodigo = solicitud.CodigoSeguro,
                            PlanCodigo = solicitud.CodigoPlan,
                            PolizaFechaVenta = poliza.FechaVenta,
                        })
                    .Where(x => x.ClienteCodigo == customerCode)
                    .Join(
                        context.Seguros,
                        res1 => res1.SeguroCodigo,
                        seguro => seguro.Codigo,
                        (res1, seguro) => new
                        {
                            res1.PolizaCodigo,
                            res1.SeguroCodigo,
                            SeguroNombre = seguro.Nombre,
                            res1.ClienteCodigo,
                            res1.PlanCodigo,
                            res1.PolizaFechaVenta
                        })
                    .Join(
                        context.Planes,
                        res2 => res2.PlanCodigo,
                        plan => plan.Codigo,
                        (res2, plan) => new
                        {
                            res2.PolizaCodigo,
                            res2.SeguroCodigo,
                            res2.SeguroNombre,
                            res2.ClienteCodigo,
                            res2.PlanCodigo,
                            res2.PolizaFechaVenta,
                            PlanNombre = plan.Nombre,
                            PlanCuota = plan.Cuota
                        })
                    .Join(
                        context.Clientes,
                        res3 => res3.ClienteCodigo,
                        cliente => cliente.Codigo,
                        (res3, cliente) => new
                        {
                            res3.PolizaCodigo,
                            res3.SeguroCodigo,
                            res3.SeguroNombre,
                            res3.ClienteCodigo,
                            res3.PlanCodigo,
                            res3.PolizaFechaVenta,
                            res3.PlanNombre,
                            res3.PlanCuota,
                            cliente.NumeroCuenta,
                            cliente.IdTipoCuenta
                        })
                    .Join(
                        context.TipoCuentas,
                        res4 => res4.IdTipoCuenta,
                        tipoCuenta => tipoCuenta.Id,
                        (res4, tipoCuenta) => new
                        {
                            res4.PolizaCodigo,
                            res4.SeguroCodigo,
                            res4.SeguroNombre,
                            res4.ClienteCodigo,
                            res4.PlanCodigo,
                            res4.PolizaFechaVenta,
                            res4.PlanNombre,
                            res4.PlanCuota,
                            res4.NumeroCuenta,
                            res4.IdTipoCuenta,
                            NombreTipoCuenta = tipoCuenta.Nombre
                        })
                    .Select(res5 => new PolizasClienteDto
                    {
                        IdTipoCuenta = res5.IdTipoCuenta,
                        CodigoPlan = res5.PlanCodigo,
                        CodigoPoliza = res5.PolizaCodigo,
                        CodigoSeguro = res5.SeguroCodigo,
                        Cuota = res5.PlanCuota,
                        FechaVenta = res5.PolizaFechaVenta,
                        NombrePlan = res5.PlanNombre,
                        NombreSeguro = res5.SeguroNombre,
                        NombreTipoCuenta = res5.NombreTipoCuenta,
                        NumeroCuenta = res5.NumeroCuenta
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
