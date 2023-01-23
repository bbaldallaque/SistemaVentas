using SistemaVenta.DAL.Dbcontext;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.MODEL;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepositorio : GenericRepositorio<Venta>, IVentaRepositorio
    {
        public VentaRepositorio(DbventaContext dbcontext) : base(dbcontext) { }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _dbcontext.Productos.Update(producto_encontrado);
                    }
                    await _dbcontext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;


                    _dbcontext.NumeroDocumentos.Update(correlativo);

                    await _dbcontext.SaveChangesAsync();

                    int cantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    //00001
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantidadDigitos, cantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();

                }
                catch (Exception)
                {

                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }
        }
    }
}
