using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUAltaMovimiento
{
    public interface ICUAltaMovimiento
    {
        void Ejecutar(MovimientoStock movimiento);
    }
}
