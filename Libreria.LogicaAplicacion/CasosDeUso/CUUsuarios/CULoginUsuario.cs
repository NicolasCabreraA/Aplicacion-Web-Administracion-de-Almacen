using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CULoginUsuario : ICULoginUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CULoginUsuario(IRepositorioUsuario repo)
        {
            RepoUsuario = repo;
        }
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return RepoUsuario.GetAll();
        }
        public LoginDto LoginUsuario(LoginDto dto)
        {
            IEnumerable<Usuario> usuarios = RepoUsuario.GetAll();
            if (dto.Email != null)
            {
                if (usuarios.Any(usuario => usuario.Email == dto.Email && usuario.Password == dto.Pwd))
                {
                    return dto;
                }
                throw new UsuarioException("Datos incorrectos");
            }
            throw new UsuarioException("Faltan valores");
        }

        public IEnumerable<Usuario> ObtenerUsuarios(IEnumerable<Usuario> usuarios) { return usuarios; }

       
    }
}
