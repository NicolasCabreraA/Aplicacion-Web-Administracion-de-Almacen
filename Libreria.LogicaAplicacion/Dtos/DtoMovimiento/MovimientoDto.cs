using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dtos.DtoMovimiento
{
    public class MovimientoDto
    {
        public int Id { get; set; }
        public int ArticuloId { get; set; }
        public int TipoId { get; set; }
        public string EmailUsuario { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }

    }

}
