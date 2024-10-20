using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario
{
    public interface ICUObtenerUsuarios
    {
        Usuario ObtenerUsuarioPorEmail(object email);
        IEnumerable<Usuario> ObtenerUsuarios();
        public Usuario ObtenerUsuarioPorId(int id);
    }
}
