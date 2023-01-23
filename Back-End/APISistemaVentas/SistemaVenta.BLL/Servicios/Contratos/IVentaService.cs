using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contratos
{
    public interface IVentaService
    {
        Task<VentaDTO> Registrar(VentaDTO modelo);

        Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);

        Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin);
    }
}
