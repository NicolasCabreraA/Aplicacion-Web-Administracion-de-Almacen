using Libreria.LogicaAplicacion.Dtos.DtoTipoMovimiento;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUTipoMoviento;
using Libreria.LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovimientoController : ControllerBase
    {
        private readonly ICUAltaTipoMovimiento _alta;
        private readonly ICUDeleteTipoMovimiento _delete;
        private readonly ICUEditTipoMovimiento _edit;
        private readonly ICUGetTipoMovimiento _get;

        // Cambiar el constructor a público
        public TipoMovimientoController(ICUAltaTipoMovimiento alta, ICUDeleteTipoMovimiento delete, ICUEditTipoMovimiento edit, ICUGetTipoMovimiento get)
        {
            _alta = alta;
            _delete = delete;
            _edit = edit;
            _get = get;
        }

        // GET: api/TipoMovimiento
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los tipos de movimiento.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tipos de movimiento obtenidos correctamente.", typeof(IEnumerable<TipoMovimientoDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontraron tipos de movimiento.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Petición inválida.")]
        public IActionResult Get()
        {
            try
            {
                var Tipos = _get.GetAll();
                if (Tipos == null || !Tipos.Any())
                {
                    return NotFound();
                }
                var TiposDto = MapeoTipoMovimientoDto.FromListado(Tipos);
                return Ok(TiposDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/TipoMovimiento/{id}
        [HttpGet("{id}", Name = "GetTipoById")]
        [SwaggerOperation(Summary = "Obtiene un tipo de movimiento por su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tipo de movimiento obtenido correctamente.", typeof(TipoMovimientoDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Tipo de movimiento no encontrado.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Petición inválida.")]
        public IActionResult Get(int id)
        {
            try
            {
                var tipo = _get.GetById(id);
                if (tipo == null)
                {
                    return NotFound();
                }
                var tipoDto = MapeoTipoMovimientoDto.ToDto(tipo);
                return Ok(tipoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST: api/TipoMovimiento
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo tipo de movimiento.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Tipo de movimiento creado correctamente.", typeof(TipoMovimientoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Petición inválida.")]
        public IActionResult Post([FromBody] TipoMovimientoDto tipoMovimientoDto)
        {
            if (tipoMovimientoDto == null)
            {
                return BadRequest("Debe indicar un tipo de movimientoDto");
            }
            try
            {
                TipoMovimiento tipo = MapeoTipoMovimientoDto.ToEntidad(tipoMovimientoDto);
                tipo.EsValido();
                _alta.Ejecutar(tipo);
                tipoMovimientoDto.Id = tipo.Id;
                return CreatedAtRoute("GetTipoById", new { id = tipoMovimientoDto.Id }, tipoMovimientoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/TipoMovimiento/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un tipo de movimiento por su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Tipo de movimiento actualizado correctamente.", typeof(TipoMovimientoDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Petición inválida.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Tipo de movimiento no encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public IActionResult Put(int id, [FromBody] EditTipoDto tipoMovimientoDto)
        {
            if (tipoMovimientoDto == null)
            {
                return BadRequest("Debe indicar un tipo de movimientoDto");
            }
            try
            {
                var tipo = MapeoTipoMovimientoDto.ToEntidad(tipoMovimientoDto);
                tipo.Id = id;
                _edit.Ejecutar(id, tipo);
                return Ok(tipo);
            }
            catch (InvalidOperationException ex)
            {
                // Si es una excepción de operación inválida, devolver BadRequest con el mensaje de la excepción
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Para otras excepciones, devolver un error interno del servidor
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        // DELETE: api/TipoMovimiento/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un tipo de movimiento por su ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Tipo de movimiento eliminado correctamente.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Petición inválida.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor.")]
        public IActionResult Delete(int id)
        {
            try
            {
                _delete.Ejecutar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
