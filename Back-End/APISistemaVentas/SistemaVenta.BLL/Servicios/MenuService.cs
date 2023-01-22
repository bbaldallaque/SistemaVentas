using AutoMapper;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.BLL.Servicios
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepositorio<MenuRol> _menuRolRepositorio;
        private readonly IGenericRepositorio<Usuario> _usuarioRepositorio;
        private readonly IGenericRepositorio<Menu> _menuRepositorio;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepositorio<MenuRol> menuRolRepositorio,
            IGenericRepositorio<Usuario> usuarioRepositorio,
            IGenericRepositorio<Menu> menuRepositorio,
            IMapper mapper)
        {
            _menuRolRepositorio = menuRolRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
            IQueryable<Menu> tbMenu = await _menuRepositorio.Consultar();

            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listaMenu = tbResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenu);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
