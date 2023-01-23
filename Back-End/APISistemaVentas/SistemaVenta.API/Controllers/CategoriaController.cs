using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [Route("Lista")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _categoriaService.Lista();
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
