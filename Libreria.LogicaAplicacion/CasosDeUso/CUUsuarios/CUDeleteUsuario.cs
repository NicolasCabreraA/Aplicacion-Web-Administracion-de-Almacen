using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUUsuarios
{
    public class CUDeleteUsuario : ICUDeleteUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public CUDeleteUsuario(IRepositorioUsuario repo)
        {
            RepoUsuario = repo;
        }
        public void DeleteUsuario(int id)
        {
            // Obtener el usuario por su ID
            var usuario = RepoUsuario.GetById(id);

            if (usuario != null)
            {
                // Eliminar el usuario
                RepoUsuario.Remove(usuario);
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }
    }
}
