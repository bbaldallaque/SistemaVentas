using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;
using System.Globalization;

namespace SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IGenericRepositorio<DetalleVenta> _detalleVentaRepositorio;
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IGenericRepositorio<DetalleVenta> detalleVentaRepositorio, IVentaRepositorio ventaRepositorio,
            IMapper mapper)
        {
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _ventaRepositorio = ventaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("no se puedo generar la venta");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var listaResutado = new List<Venta>();

            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-US"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-US"));

                    listaResutado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
                else
                {
                    listaResutado = await query.Where(v => v.NumeroDocumento == numeroVenta).Include(dv => dv.DetalleVenta)
                   .ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<VentaDTO>>(listaResutado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();

            try
            {
                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-US"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-US"));

                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
