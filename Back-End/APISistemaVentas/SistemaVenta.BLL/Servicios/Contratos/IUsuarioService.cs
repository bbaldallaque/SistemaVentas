using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contratos
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Lista();

        Task<SeccionDTO> ValidarCredenciales(string correo, string clave);

        Task<UsuarioDTO> Crear(UsuarioDTO modelo);

        Task<bool> Editar(UsuarioDTO modelo);

        Task<bool> Eliminar(int id);
    }
}
