using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contratos
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> Lista();

        Task<ProductoDTO> Crear(ProductoDTO modelo);

        Task<bool> Editar(ProductoDTO modelo);

        Task<bool> Eliminar(int id);
    }
}
