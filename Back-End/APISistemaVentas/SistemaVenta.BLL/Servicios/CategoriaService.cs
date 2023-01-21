using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.BLL.Servicios
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepositorio<Categoria> _categoriaRepositorio;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepositorio<Categoria> categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try
            {
                var listaCategoria = await _categoriaRepositorio.Consultar();
                //TODO: si presenta error, cambiar AsNoTracking por ToList
                return _mapper.Map<List<CategoriaDTO>>(listaCategoria.AsNoTracking());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
