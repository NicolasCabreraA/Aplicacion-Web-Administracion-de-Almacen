using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dtos.DtoArticulo
{
    public class ArticuloDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CodigoProveedor { get; set; }
        public decimal Precio { get; set; }
        
    }
}
