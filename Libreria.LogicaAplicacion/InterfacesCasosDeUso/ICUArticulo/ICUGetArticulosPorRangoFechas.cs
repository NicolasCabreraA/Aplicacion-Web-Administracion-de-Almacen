using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUArticulo
{
    public interface ICUGetArticulosPorRangoFechas
    {
        public IEnumerable<dynamic> Ejecutar(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros);
        public int GetCantidadItemsPagina();
    }
}
