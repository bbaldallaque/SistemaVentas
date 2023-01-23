using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contratos;
using SistemaVenta.DAL.Repositorios.Contratos;
using SistemaVenta.DTO;
using SistemaVenta.MODEL;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepositorio<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepositorio<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }


        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar();
                //TODO: si presenta error, cambiar AsNoTracking por ToList     
                var listaUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).AsNoTracking();

                return _mapper.Map<List<UsuarioDTO>>(listaUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SeccionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.Correo == correo && u.Clave == clave);

                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario o contraseña no existe");

                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SeccionDTO>(devolverUsuario);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("usuario  no creado");

                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = await query.Include(rol => rol.IdRolNavigation).FirstAsync();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);

                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("usuario no existe");

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("no se puede editar el usuario");

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
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("usuario no existe");

                bool respuesta = await _usuarioRepositorio.Eiliminar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("no se puede eliminar el usuario");

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
