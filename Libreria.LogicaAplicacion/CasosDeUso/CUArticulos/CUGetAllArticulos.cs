using Libreria.LogicaAplicacion.Dtos.DtoArticulo;
using Libreria.LogicaAplicacion.InterfacesCasosDeUso.ICUArticulo;
using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAplicacion.CasosDeUso.CUArticulos
{
    public class CUGetAllArticulos : ICUGetAllArticulos
    {
        public IRepositorioArticulo RepoArticulo { get; set; }
        public CUGetAllArticulos(IRepositorioArticulo repo)
        {
            RepoArticulo = repo;
        }
        public IEnumerable<ArticuloDto> Ejecutar()
        {
            
            return RepoArticulo.GetAll()
                .OrderBy(articulo => articulo.Nombre)
                .Select(articulo => new ArticuloDto
                {
                    Id = articulo.Id,
                    Nombre = articulo.Nombre,
                    Descripcion = articulo.Descripcion,
                    Precio = articulo.Precio,
                })
                .ToList();
        }
    }
}
