using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.Dtos.DtoTipoMovimiento
{
    public class MapeoTipoMovimientoDto
    {
        public static TipoMovimiento ToEntidad(TipoMovimientoDto tipoMovimientoDto)
        {
            var tipo = new TipoMovimiento
            {
                Id = tipoMovimientoDto.Id,
                Nombre = tipoMovimientoDto.Nombre,
                Descripcion = tipoMovimientoDto.Descripcion
            };
            
            return tipo;
        }
        public static TipoMovimiento ToEntidad(EditTipoDto tipoMovimientoDto)
        {
            var tipo = new TipoMovimiento
            {
                Nombre = tipoMovimientoDto.Nombre,
                Descripcion = tipoMovimientoDto.Descripcion
                // El Id será asignado en el controlador
            };

            return tipo;
        }
        public static TipoMovimientoDto ToDto(TipoMovimiento tipo)
        {
            return new TipoMovimientoDto
            {
                Id = tipo.Id,
                Nombre = tipo.Nombre,
                Descripcion = tipo.Descripcion
            };
        }

        public static IEnumerable<TipoMovimientoDto>FromListado (IEnumerable<TipoMovimiento>tipoMovimientos) 
        {
            if(tipoMovimientos == null)
            {
                return null;
            }
            var listaModelo =tipoMovimientos.Select(t => ToDto(t)).ToList();
            return listaModelo;
        }

    }
}
