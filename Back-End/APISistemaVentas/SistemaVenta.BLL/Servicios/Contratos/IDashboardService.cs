using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contratos
{
    public interface IDashboardService
    {
        Task<DashboardDTO> Resumen();
    }
}
