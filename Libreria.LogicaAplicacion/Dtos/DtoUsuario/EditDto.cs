using Libreria.LogicaNegocio.ValueObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dtos.DtoUsuario
{
    public class EditDto
    {
        public string Email { get; set; }
        public string Nombre{ get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public string PasswordSE { get; set; }
    }
}
