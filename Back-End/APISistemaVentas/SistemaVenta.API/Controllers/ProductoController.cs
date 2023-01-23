using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("Lista")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ProductoDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Lista();
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost("Guardar")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IniciarSeccion([FromBody] ProductoDTO producto)
        {
            var rsp = new Response<ProductoDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Crear(producto);
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut("Edit")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> EditUser([FromBody] ProductoDTO producto)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Editar(producto);
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete("Eliminar/{id:int}")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _productoService.Eliminar(id);
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