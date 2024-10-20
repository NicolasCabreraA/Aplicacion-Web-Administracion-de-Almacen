using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento
{
    public interface ICUAltaTipoMovimiento
    {
        void Ejecutar(TipoMovimiento tipoMovimiento);
    }
}
