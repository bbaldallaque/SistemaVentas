using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost("Registrar")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IniciarSeccion([FromBody] VentaDTO venta)
        {
            var rsp = new Response<VentaDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _ventaService.Registrar(venta);
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet("Historial")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<VentaDTO>>();
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? null : fechaFin;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rsp.Status = true;
                rsp.Value = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio!, fechaFin);
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpGet("Reporte")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<ReporteDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _ventaService.Reporte(fechaInicio!, fechaFin!);
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
