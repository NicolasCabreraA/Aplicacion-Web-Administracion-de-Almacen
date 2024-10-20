using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUMovimientos
{
    public class CUGetMovimiento : ICUGetMovimiento
    {
        private IRepositorioMovimientoStock _repoMovimiento;
        public CUGetMovimiento(IRepositorioMovimientoStock repoMoviento)
        {
            _repoMovimiento = repoMoviento;
        }

        public IEnumerable<MovimientoStock> GetAll()
        {
            return _repoMovimiento.GetAll();
        }

        public MovimientoStock GetById(int id)
        {
            MovimientoStock obj = null;
            if (id != null)
            {
                obj = _repoMovimiento.GetById(id);
            }
            return obj;
        }
    }
}
