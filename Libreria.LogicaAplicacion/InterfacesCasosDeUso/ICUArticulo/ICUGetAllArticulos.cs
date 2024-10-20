using Libreria.LogicaAplicacion.Dtos.DtoArticulo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUArticulo
{
    public interface ICUGetAllArticulos
    {
        IEnumerable<ArticuloDto> Ejecutar();
    }
}
