using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Lista")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<UsuarioDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _usuarioService.Lista();
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost("IniciarSeccion")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IniciarSeccion([FromBody] LoginDTO login)
        {
            var rsp = new Response<SeccionDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost("Registrarse")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> IniciarSeccion([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<UsuarioDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _usuarioService.Crear(usuario);
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
        public async Task<IActionResult> EditUser([FromBody] UsuarioDTO usuario)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _usuarioService.Editar(usuario);
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
                rsp.Value = await _usuarioService.Eliminar(id);
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
