using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("Lista")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<RolDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _rolService.Lista();
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
