using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUMovimientos
{
    public class CUGetMovimientosPaginados : ICUGetMovimientosPaginados
    {
        private readonly IRepositorioMovimientoStock _repositorioMov;
        private readonly IRepositorioConfiguracion _repositorioParametro;


        public CUGetMovimientosPaginados(IRepositorioMovimientoStock repositorioMov, IRepositorioConfiguracion repositorioParametro)
        {
            _repositorioMov = repositorioMov;
            _repositorioParametro = repositorioParametro;

        }
        public int GetCantidadItemsPagina()
        {
            return _repositorioParametro.GetValor("ItemsXPagina");
        }
        public IEnumerable<dynamic> Ejecutar(int articuloId, int tipoId, int numPagina, int cantidadRegistros)
        {
            var docentes = _repositorioMov.FindMovsPaginados(articuloId, tipoId, numPagina, cantidadRegistros);
            return docentes;
        }
    }
}
