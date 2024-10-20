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
    public class CUGetMovimientosPorArticuloYTipo : ICUGetMovimientosPorArticuloYTipo
    {
        private IRepositorioMovimientoStock _repoMovimiento;
        public CUGetMovimientosPorArticuloYTipo(IRepositorioMovimientoStock repoMovimiento)
        {
            _repoMovimiento = repoMovimiento;
        }
        public IEnumerable<dynamic> Ejecutar(int articuloId, int tipoId)
        {
            try
            {
                var resumen = _repoMovimiento.GetMovimientosPorArticuloYTipo(articuloId, tipoId);
                return resumen;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

