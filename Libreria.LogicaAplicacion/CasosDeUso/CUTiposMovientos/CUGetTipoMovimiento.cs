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
    public class CUGetTipoMovimiento : ICUGetTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repo;
        public CUGetTipoMovimiento(IRepositorioTipoMovimiento repo)
        {
            _repo = repo;
        }
        public IEnumerable<TipoMovimiento> GetAll()
        {
            return _repo.GetAll();
        }

        public TipoMovimiento GetById(int id)
        {
            TipoMovimiento tipo = null;
            if(id != null)
            {
                tipo = _repo.GetById(id);
            }
            return tipo;
        }
    }
}
