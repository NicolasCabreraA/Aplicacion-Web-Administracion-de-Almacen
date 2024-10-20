using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUAltaMovimiento;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUMovimientos
{
    public class CUAltaMovimiento : ICUAltaMovimiento
    {
        private IRepositorioMovimientoStock _repoMovimiento;
        private IRepositorioConfiguracion _repoConf;
        public CUAltaMovimiento(IRepositorioMovimientoStock repoMovimiento, IRepositorioConfiguracion repoConf)
        {
            _repoMovimiento = repoMovimiento;
            _repoConf = repoConf;
        }

        public void Ejecutar(MovimientoStock movimiento)
        {
           movimiento.EsValido();
            var tope = _repoConf.GetValor("TopeUnits");
            if (movimiento.CantidadAMover > tope)
                throw new ArgumentException($"La cantidad no puede exceder el tope de {tope} unidades");
            _repoMovimiento.Add(movimiento);
        }
    }
}
