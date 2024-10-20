using Libreria.LogicaAplicacion.Dtos.DtoUsuario;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.UtilidadesJwt;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ICULoginUsuario _login;
        public UsuarioController(ICULoginUsuario login)
        {
            _login = login;
        }
        // POST: api/Usuario/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Autenticación de usuario.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Autenticación exitosa.", typeof(LoginDto))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Credenciales incorrectas.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]

        public IActionResult Login([FromBody] LoginDto usr)
        {
            try
            {
                var rol = _login.LoginUsuario(usr).rol;
                if (string.IsNullOrWhiteSpace("rol"))
                {
                    return Unauthorized("Credenciales incorrectas");
                }
                string token = ManejadorJwt.GenerarToken(usr.Email, rol);
                return Ok(new { Token = token, Rol = rol, Email = usr.Email });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }

        }

    }
}
