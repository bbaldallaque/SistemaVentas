using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios.Contratos
{
    public interface IGenericRepositorio<TModelo> where TModelo : class
    {
        Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro);

        Task<TModelo> Crear(TModelo modelo);

        Task<bool> Editar(TModelo modelo);

        Task<bool> Eiliminar(TModelo modelo);

        Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null);
    }
}
