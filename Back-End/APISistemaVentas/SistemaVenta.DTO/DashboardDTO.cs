namespace SistemaVenta.DTO
{
    public class DashboardDTO
    {
        public int? TotalVenta { get; set; }

        public string? TotalIngresos { get; set; }

        public List<VentaSemanaDTO>? VentaUltimaSemana { get; set; }
    }
}
