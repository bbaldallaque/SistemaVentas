using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contratos
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Lista();
    }
}
