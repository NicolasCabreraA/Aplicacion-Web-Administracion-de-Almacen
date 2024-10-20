using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario
{
    public interface ICULoginUsuario
    {

        public LoginDto LoginUsuario(LoginDto dto);
    }
}
