using Libreria.LogicaAplicacion.Dtos.DtoMovimiento;
using Libreria.LogicaAplicacion.Dtos.DtoTipoMovimiento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUAltaMovimiento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUMovimiento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento;
using Libreria.LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoStockController : ControllerBase
    {
        private readonly ICUAltaMovimiento _alta;
        private readonly ICUGetMovimiento _get;
        private readonly ICUGetResumen _resumen;
        private readonly ICUGetMovimientosPaginados _movimientos;



        // Constructor
        public MovimientoStockController(ICUAltaMovimiento alta, ICUGetMovimiento get, ICUGetResumen resumen, ICUGetMovimientosPaginados movimientos)
        {
            _alta = alta;
            _get = get;
            _resumen = resumen;
            _movimientos = movimientos;
        }

        // GET: api/MovimientoStock
        /// <summary>
        /// Obtiene todos los movimientos.
        /// </summary>
        [HttpGet]
        [Route("")]
        [SwaggerOperation(Summary = "Obtiene todos los movimientos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de movimientos obtenida correctamente.", typeof(IEnumerable<MovimientoDto>))]
        public IActionResult Get()
        {
            var movimientos = _get.GetAll();
            var movimientoDtos = MapeoMovimientoDto.FromListado(movimientos);

            return Ok(movimientoDtos);
        }

        /// <summary>
        /// Obtiene un movimiento por su ID.
        /// </summary>
        /// <param name="id">ID del movimiento.</param>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un movimiento por su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movimiento encontrado.", typeof(MovimientoDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Movimiento no encontrado.")]
        public ActionResult<MovimientoDto> Get(int id)
        {
            var movimientoStock = _get.GetById(id);

            if (movimientoStock == null)
            {
                return NotFound();
            }

            var movimientoDto = MapeoMovimientoDto.ToDto(movimientoStock);

            return Ok(movimientoDto);
        }

        // POST: api/MovimientoStock
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo movimiento.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Movimiento creado correctamente.", typeof(MovimientoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Petición inválida.")]
        public IActionResult Post([FromBody] MovimientoDto movimientoDto)
        {
            if (movimientoDto == null)
            {
                return BadRequest("Debe indicar un movimiento");
            }

            try
            {

                MovimientoStock movimientoStock = MapeoMovimientoDto.ToEntidad(movimientoDto);
                _alta.Ejecutar(movimientoStock); 
                movimientoDto.Id = movimientoStock.Id;
                
                return CreatedAtAction(nameof(Get), new { id = movimientoDto.Id }, movimientoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/MovimientoStock/Resumen
        [HttpGet("Resumen")]
        [SwaggerOperation(Summary = "Obtiene un resumen de los movimientos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Resumen de movimientos obtenido correctamente.")]

        public IActionResult Resumen()
        {
            var movimientos = _resumen.Ejecutar();
            return Ok(movimientos);
        }


        // GET: api/MovimientoStock/PorArticuloYTipo/{articuloId}/{tipoId}/{pagina}
        [HttpGet("PorArticuloYTipo/{articuloId}/{tipoId}/{pagina}")]
        [SwaggerOperation(Summary = "Obtiene los movimientos filtrados por artículo y tipo.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movimientos obtenidos correctamente.", typeof(IEnumerable<MovimientoDto>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]

        public IActionResult PorArticuloYTipo(int articuloId, int tipoId, int pagina)
        {
            try
            {
                int itemsPagina = _movimientos.GetCantidadItemsPagina();

                var movimientos = _movimientos.Ejecutar(articuloId, tipoId, pagina, itemsPagina);
                return Ok(movimientos);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

    }
}
