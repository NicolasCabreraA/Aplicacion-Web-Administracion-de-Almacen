using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dtos.DtoUsuario
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string? rol { get; set; }
    }
}
