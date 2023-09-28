using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seguros_restapi.Services;

namespace seguros_restapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly CommonService commonService = new();

        [HttpGet("seguros")]
        public async Task<IActionResult> GetSeguros()
        {
            try
            {
                var result = await commonService.GetSeguros();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("planes")]
        public async Task<IActionResult> GetPlanes()
        {
            try
            {
                var result = await commonService.GetPlanes();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("estados")]
        public async Task<IActionResult> GetEstadosSolicitud()
        {
            try
            {
                var result = await commonService.GetEstadosSolicitud();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("cuentas")]
        public async Task<IActionResult> GetTiposdeCuentas()
        {
            try
            {
                var result = await commonService.GetTiposdeCuentas();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
