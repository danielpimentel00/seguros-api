using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using seguros_restapi.DTO;
using seguros_restapi.Services;

namespace seguros_restapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly SolicitudService solicitudService = new();

        [HttpPost]
        public async Task<IActionResult> CreateSolicitud([FromBody] SolicitudDto model)
        {
            try
            {
                var result = await solicitudService.CreateSolicitud(model);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
