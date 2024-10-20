using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Libreria.LogicaNegocio.ValueObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUAltaUsuario(IRepositorioUsuario repo) 
        {
            RepoUsuario = repo;
        }
        public void AltaUsuario(EditDto dto)
        {

                if(RepoUsuario.ExisteCorreoElectronico(dto.Email))
                {
                    throw new UsuarioException("No se puede crear el usuario por favor ingrese otros datos");
                }
                Usuario u = new Usuario()
                {
                    Email = dto.Email,
                    NombreCompleto = new NombreCompleto(dto.Nombre, dto.Apellido),
                    Password = dto.Password,
                    PasswordSE = dto.PasswordSE,
                };
                RepoUsuario.Add(u);

        }

        public void ExisteUsuario(string email)
        {
            RepoUsuario.ExisteCorreoElectronico(email);
        }
    }
}
