using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.BLL.Servicios
{
    public class RolService : IRolService
    {
        private readonly IGenericRepositorio<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericRepositorio<Rol> rolRepositorio, IMapper mapper)
        {
            _rolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolRepositorio.Consultar();
                return _mapper.Map<List<RolDTO>>(listaRoles.ToListAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
