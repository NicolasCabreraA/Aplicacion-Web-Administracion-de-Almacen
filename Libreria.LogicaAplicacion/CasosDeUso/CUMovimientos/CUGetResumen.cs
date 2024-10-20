using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUMovimientos
{
    public class CUGetResumen : ICUGetResumen
    {
        private IRepositorioMovimientoStock _repoMovimiento;
        public CUGetResumen(IRepositorioMovimientoStock repoMovimiento)
        {
            _repoMovimiento = repoMovimiento;
        }
        public IEnumerable<dynamic> Ejecutar()
        {
            try
            {
                var resumen = _repoMovimiento.GetResumenMovimiento();
                return resumen;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
