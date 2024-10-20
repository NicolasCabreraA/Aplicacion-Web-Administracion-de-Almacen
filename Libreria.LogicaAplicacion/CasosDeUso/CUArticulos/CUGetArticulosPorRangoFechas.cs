using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUArticulo;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUArticulos
{
    public class CUGetArticulosPorRangoFechas : ICUGetArticulosPorRangoFechas
    {
        private IRepositorioArticulo _repoArticulo;
        private readonly IRepositorioConfiguracion _repositorioParametro;
        public CUGetArticulosPorRangoFechas(IRepositorioArticulo repoArticulo, IRepositorioConfiguracion repositorioParametro)
        {
            _repoArticulo = repoArticulo;
            _repositorioParametro = repositorioParametro;
        }
        public int GetCantidadItemsPagina()
        {
            return _repositorioParametro.GetValor("ItemsXPagina");
        }
        public IEnumerable<dynamic> Ejecutar(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros)
        {
            try
            {
                var resumen = _repoArticulo.GetArticulosPorRangoFechas(fechaInicio, fechaFin, numPagina, cantidadRegistros);
                return resumen;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
