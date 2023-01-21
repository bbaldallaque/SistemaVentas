using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contratos
{
    public interface IRolService
    {
        Task<List<RolDTO>> Lista();
    }
}
