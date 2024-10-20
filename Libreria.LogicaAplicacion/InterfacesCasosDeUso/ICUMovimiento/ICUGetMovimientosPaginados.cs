using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento
{
    public interface ICUGetMovimientosPaginados
    {
        public IEnumerable<dynamic> Ejecutar(int articuloId, int tipoId, int numPagina, int cantidadRegistros);
        int GetCantidadItemsPagina();
    }
}
