using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seguros_restapi.Services;

namespace seguros_restapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly PolizaService polizaService = new();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            try
            {
                var result = await polizaService.GetCustomerPolizas(id);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
