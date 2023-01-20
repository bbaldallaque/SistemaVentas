using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.Dbcontext;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.DAL.Repositorios.Contratos;

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
        }
    }
}
