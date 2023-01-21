using AutoMapper;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;
using System.Globalization;

namespace SistemaVenta.UTILITY
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<Menu, MenuDTO>().ReverseMap();

            #region usuario
            CreateMap<Usuario, UsuarioDTO>()
            .ForMember(destino => destino.RolDescripcion, opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
            )
            .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<Usuario, SeccionDTO>()
            .ForMember(destino => destino.RolDescripcion, opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
            );

            CreateMap<UsuarioDTO, Usuario>()
           .ForMember(destino => destino.IdRolNavigation, opt => opt.Ignore())
           .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));

            #endregion

            CreateMap<Categoria, CategoriaDTO>().ReverseMap();

            #region Producto

            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino => destino.DescripcionCategoria, opt =>
                opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino => destino.Precio, opt =>
                opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("en-US")))
                )
                .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<ProductoDTO, Producto>()
              .ForMember(destino => destino.IdCategoriaNavigation, opt =>
              opt.Ignore()
              )
              .ForMember(destino => destino.Precio, opt =>
              opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("en-US")))
              )
              .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));

            #endregion

            #region Venta
            CreateMap<Venta, VentaDTO>()
             .ForMember(destino => destino.TotalTexto, opt =>
             opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("en-US")))
             )
              .ForMember(destino => destino.FechaRegistro, opt =>
             opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
             );

            CreateMap<VentaDTO, Venta>()
                .ForMember(destino => destino.Total, opt =>
              opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("en-US"))));
            #endregion

            #region Detalle Venta
            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino => destino.DescripcionProducto, opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                 .ForMember(destino => destino.PrecioTexto, opt =>
                opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("en-US"))))
                  .ForMember(destino => destino.TotalTexto, opt =>
                opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("en-US"))));



            CreateMap<DetalleVentaDTO, DetalleVenta>()
                 .ForMember(destino => destino.Precio, opt =>
                opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("en-US"))))
                  .ForMember(destino => destino.Total, opt =>
                opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("en-US"))));
            #endregion

            #region Detalle Venta Reporte

            CreateMap<DetalleVenta, ReporteDTO>()
                 .ForMember(destino => destino.FechaRegistro, opt =>
             opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
             )
                 .ForMember(destino => destino.NumeroDocumento, opt =>
             opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
             )
                 .ForMember(destino => destino.TipoPago, opt =>
             opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
             )
                  .ForMember(destino => destino.TotalVenta, opt =>
             opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("en-US")))
             )
                    .ForMember(destino => destino.Producto, opt =>
             opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
             )
                     .ForMember(destino => destino.Precio, opt =>
             opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("en-US")))
             )
                     .ForMember(destino => destino.Total, opt =>
             opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("en-US"))));

            #endregion
        }
    }
}
