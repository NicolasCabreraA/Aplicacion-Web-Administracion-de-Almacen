using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CUObtenerUsuarios : ICUObtenerUsuarios
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUObtenerUsuarios(IRepositorioUsuario repo)
        {
            RepoUsuario = repo;
        }
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return RepoUsuario.GetAll();
        }

        public Usuario ObtenerUsuarioPorEmail(object email)
        {
            return RepoUsuario.ObtenerPorEmail(email);
        }
        public Usuario ObtenerUsuarioPorId(int id)
        {
            return RepoUsuario.GetById(id);
        }
    }
}
