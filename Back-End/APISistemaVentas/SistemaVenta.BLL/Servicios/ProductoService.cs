using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepositorio<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepositorio<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consultar();

                var listaProducto = queryProducto.Include(cat => cat.IdCategoriaNavigation).AsNoTracking();
                //TODO: si presenta error, cambiar AsNoTracking por ToList
                return _mapper.Map<List<ProductoDTO>>(listaProducto.AsNoTracking());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("El producto no se pudo crear");

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);

                var productoEncontrado = await _productoRepositorio.Obtener(u => u.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null) throw new TaskCanceledException("producto no encontrado");

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("no se pudo editar");

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);

                if (productoEncontrado == null) throw new TaskCanceledException("producto no encontrado");

                bool respuesta = await _productoRepositorio.Eiliminar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("no se pudo editar");

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
