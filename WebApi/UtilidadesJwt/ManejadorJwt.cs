using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.UtilidadesJwt
{
    public class ManejadorJwt
    {

        /// <summary>
        /// Método para generar el token JWT usando una función estática (no es necesario tener instancias)
        /// </summary>

        /// <remarks> Creación del "payload" con tiene la información del usuario que se logueó (subject)
        /// El usuario tiene "claims", que son pares nombre/valor que se utilizan para guardar
        /// en el cliente. No pueden ser sensibles
        /// Se le debe setear el periodo temporal de validez (Expires)
        ///Se utiliza un algoritmo de clave simétrica para generar el token</remarks>

        public static string GenerarToken(string email, string rol)
        {
            //    //Se crea un objeto de la clase JwtSecurityTokenHandler
            //    var tokenHandler = new JwtSecurityTokenHandler();

            ////clave secreta, es un string largo y lo más complejo posible, generalmente se incluye en el archivo de configuración
            ////Debe convertirse a un vector de bytes 





            ////Se incluye un claim para el rol

            //var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //        new Claim(ClaimTypes.Email, email),
            //        new Claim(ClaimTypes.Role, rol)
            //        }),
            //        Expires = DateTime.UtcNow.AddMonths(1),

            //        SigningCredentials = new SigningCredentials(claveDificilEncriptada,
            //                                SecurityAlgorithms.HmacSha512Signature)
            //    };

            //    var token = tokenHandler.CreateToken(tokenDescriptor);

            //    return tokenHandler.WriteToken(token);

            var claveDificil = "ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1_ClaveMuySecreta1";
            var claveDificilEncriptada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
            List<Claim> claims = [
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, rol)
                ];

            var credenciales = new SigningCredentials(claveDificilEncriptada, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credenciales);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }



    }
}

