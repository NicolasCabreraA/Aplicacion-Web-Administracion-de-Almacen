using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUTiposMovientos
{
    public class CUAltaTipoMovimiento : ICUAltaTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repoTipoMoviento;
        public CUAltaTipoMovimiento(IRepositorioTipoMovimiento repoTipoMoviento)
        {
            _repoTipoMoviento = repoTipoMoviento;
        }
        
        public void Ejecutar(TipoMovimiento tipoMovimiento)
        {
            _repoTipoMoviento.Add(tipoMovimiento);
        }
    }
}
