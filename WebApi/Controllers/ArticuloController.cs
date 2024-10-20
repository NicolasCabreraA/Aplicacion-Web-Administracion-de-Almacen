using Libreria.LogicaAplicacion.Dtos.DtoArticulo;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUArticulo;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly ICUGetAllArticulos _cuGetAllArticulos;
        private readonly ICUGetArticulosPorRangoFechas _articulos;

        public ArticuloController(ICUGetAllArticulos cuGetAllArticulos, ICUGetArticulosPorRangoFechas articulos)
        {
            _cuGetAllArticulos = cuGetAllArticulos;
            _articulos = articulos;
        }
        // GET: api/<ArticuloController>

        /// <summary>
        /// Obtiene todos los artículos.
        /// </summary>
        /// <returns>Lista de artículos.</returns>
        [HttpGet("Articulo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArticuloDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ArticuloDto>> Get()
        {
            try
            {
                var articulos = _cuGetAllArticulos.Ejecutar();
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los artículos en un rango de fechas paginados.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del rango.</param>
        /// <param name="fechaFin">Fecha de fin del rango.</param>
        /// <param name="pagina">Número de página.</param>
        /// <returns>Artículos dentro del rango de fechas.</returns>
        [HttpGet("PorRangoDeFechas/{fechaInicio}/{fechaFin}/{pagina}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArticuloDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin, int pagina)
        {
            try 
            { 
            int itemsPagina = _articulos.GetCantidadItemsPagina();
            var movimientos = _articulos.Ejecutar(fechaInicio, fechaFin, pagina, itemsPagina);
            return Ok(movimientos);
            }catch
            (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

    }
}
