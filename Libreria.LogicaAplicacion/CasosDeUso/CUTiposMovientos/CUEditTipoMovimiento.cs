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
    public class CUEditTipoMovimiento : ICUEditTipoMovimiento
    {
        private IRepositorioTipoMovimiento _repo;
        public CUEditTipoMovimiento(IRepositorioTipoMovimiento repo)
        {
            _repo = repo;
        }
        public void Ejecutar(int id, TipoMovimiento tipoMovimiento)
        {
            _repo.Update(id, tipoMovimiento);
        }
    }
}
