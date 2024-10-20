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
    public class CUCrearArticulo : ICUCrearArticulo
    {
        public IRepositorioArticulo RepoArticulo { get; set; }
        public CUCrearArticulo(IRepositorioArticulo repo)
        {
            RepoArticulo = repo;
        }
        public void Ejecutar(ArticuloDto articuloDTO)
        {
            var articulo = new Articulo
            {
                Nombre = articuloDTO.Nombre,
                Descripcion = articuloDTO.Descripcion,
                CodigoProveedor = articuloDTO.CodigoProveedor,
                Precio = articuloDTO.Precio,
            };

            RepoArticulo.Add(articulo);
        }
    }
}
