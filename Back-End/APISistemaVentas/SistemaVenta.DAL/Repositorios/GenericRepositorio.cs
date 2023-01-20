using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Dbcontext;
using SistemaVenta.DAL.Repositorios.Contratos;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios
{
    public class GenericRepositorio<TModelo> : IGenericRepositorio<TModelo> where TModelo : class
    {
        public readonly DbventaContext _dbcontext;

        public GenericRepositorio(DbventaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dbcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eiliminar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModelo>  queryModelo = filtro == null ? _dbcontext.Set<TModelo>(): _dbcontext.Set<TModelo>()
                    .Where(filtro);
                return queryModelo;
             }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
