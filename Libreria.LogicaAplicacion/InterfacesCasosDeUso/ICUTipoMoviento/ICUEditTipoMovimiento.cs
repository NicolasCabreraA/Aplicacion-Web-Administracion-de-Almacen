using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento
{
    public interface ICUEditTipoMovimiento
    {
        void Ejecutar(int id, TipoMovimiento tipoMovimiento);
    }
}
