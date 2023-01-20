using SistemaVenta.MODEL;

namespace SistemaVenta.DAL.Repositorios.Contratos
{
    public interface IVentaRepositorio : IGenericRepositorio<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
