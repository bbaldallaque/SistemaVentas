using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        [HttpGet("Resumen")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashboardDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _dashboardService.Resumen();
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
