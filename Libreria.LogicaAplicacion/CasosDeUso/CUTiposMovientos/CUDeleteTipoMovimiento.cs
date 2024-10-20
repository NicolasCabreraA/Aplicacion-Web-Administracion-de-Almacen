using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUTiposMovientos
{
    public class CUDeleteTipoMovimiento : ICUDeleteTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repo;
        public CUDeleteTipoMovimiento(IRepositorioTipoMovimiento repo)
        {
            _repo = repo;
        }
        public void Ejecutar(int id)
        {
            _repo.Remove(id);
        }
    }
}
