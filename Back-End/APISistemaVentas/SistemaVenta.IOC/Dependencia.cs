using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.BLL.Servicios;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Dbcontext;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.UTILITY;

namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DbventaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SistemaVenta011920232209"));
            });

            service.AddTransient(typeof(IGenericRepositorio<>), typeof(GenericRepositorio<>));
            service.AddScoped<IVentaRepositorio, VentaRepositorio>();

            service.AddAutoMapper(typeof(AutoMapperProfile));

            service.AddScoped<IRolService, RolService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<ICategoriaService, CategoriaService>();
            service.AddScoped<IProductoService, ProductoService>();
            service.AddScoped<IVentaService, VentaService>();
            service.AddScoped<IDashboardService, DashboardService>();
            service.AddScoped<IMenuService, MenuService>();
        }
    }
}
