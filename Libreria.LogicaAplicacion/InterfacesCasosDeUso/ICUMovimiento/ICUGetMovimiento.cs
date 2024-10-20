using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento
{
    public interface ICUGetMovimiento
    {
        MovimientoStock GetById(int id);
        IEnumerable<MovimientoStock> GetAll();
    }
}
