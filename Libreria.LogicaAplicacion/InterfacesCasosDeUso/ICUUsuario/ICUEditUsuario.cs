using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario
{
    public interface ICUEditUsuario
    {
        public void EditUsuario(EditDto dto);
    }
}
