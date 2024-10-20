using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioConfiguracionEF : IRepositorioConfiguracion
    {
        private PapeleriaContext _db { get; set; }
        public RepositorioConfiguracionEF(PapeleriaContext db)
        {
            _db = db;
        }
        public int GetValor(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentNullException("Nombre del parámetro null");
            try
            {
                var param = _db.Configuracion.Find(nombre);
                var valor = param.Valor;
                return valor;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
