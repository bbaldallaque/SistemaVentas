using AutoMapper;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;
using System.Globalization;

namespace SistemaVenta.BLL.Servicios
{
    public class DashboardService : IDashboardService
    {

        private readonly IGenericRepositorio<Producto> _productoRepositorio;
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IMapper _mapper;

        public DashboardService(IGenericRepositorio<Producto> productoRepositorio, IVentaRepositorio ventaRepositorio,
            IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _ventaRepositorio = ventaRepositorio;
            _mapper = mapper;
        }

        private IQueryable<Venta> RetornarVenta(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentaUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, -7);
                total = tablaVenta.Count();
            }

            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, 7);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-US"));
        }

        private async Task<int> TotalProducto()
        {
            IQueryable<Producto> _productoQuery = await _productoRepositorio.Consultar();

            int total = _productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Venta> _ventaQuery = await _ventaRepositorio.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVenta(_ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;
        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboard = new DashboardDTO();
            try
            {
                vmDashboard.TotalVenta = await TotalVentaUltimaSemana();
                vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboard.TotalProductos = await TotalProducto();

                List<VentaSemanaDTO> listadeVentaSemana = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listadeVentaSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value,
                    });
                }

                vmDashboard.VentaUltimaSemana = listadeVentaSemana;

            }
            catch (Exception)
            {

                throw;
            }

            return vmDashboard;
        }
    }
}
