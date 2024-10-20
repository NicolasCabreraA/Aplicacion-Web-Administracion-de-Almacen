using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Libreria.LogicaNegocio.ValueObjets;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CUEditUsuario : ICUEditUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUEditUsuario(IRepositorioUsuario repo)
        {
            RepoUsuario = repo;
        }
        public void EditUsuario(EditDto dto)
        {
            foreach (Usuario u in RepoUsuario.GetAll())
            {
                if (dto.Email == u.Email)
                {
                    
                    Usuario us = new Usuario();
                    us.NombreCompleto = new NombreCompleto(dto.Nombre, dto.Apellido);
                    us.Password = dto.Password;
                    us.PasswordSE = dto.PasswordSE;

                    RepoUsuario.Update(u.Id, us);
                    return;
                }
                
            }
            throw new UsuarioException("No se encontro un usuario con ese mail");

        }
    }

}
