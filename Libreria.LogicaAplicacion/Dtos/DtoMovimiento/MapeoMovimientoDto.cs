using Libreria.LogicaAplicacion.Dtos.DtoTipoMovimiento;
using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dtos.DtoMovimiento
{
    public class MapeoMovimientoDto
    {
        public static MovimientoStock ToEntidad(MovimientoDto movimientoDto)
        {

            return new MovimientoStock
            {
                ArticuloId = movimientoDto.ArticuloId,
                EmailUsuario = movimientoDto.EmailUsuario,
                CantidadAMover = movimientoDto.Cantidad,
                TipoId = movimientoDto.TipoId

            };
        }

        public static MovimientoDto ToDto(MovimientoStock mov)
        {
            return new MovimientoDto
            {
                ArticuloId = mov.ArticuloId,
                TipoId = mov.TipoId,
                EmailUsuario = mov.EmailUsuario,
                Cantidad = mov.CantidadAMover,
                Fecha =mov.FechaMovimiento

            };
        }

        public static IEnumerable<MovimientoDto> FromListado(IEnumerable<MovimientoStock> movimientos)
        {
            if (movimientos == null)
            {
                return null;
            }
            var listaMovimientosDto = movimientos.Select(m => ToDto(m)).ToList();
            return listaMovimientosDto;
        }
    }
}
