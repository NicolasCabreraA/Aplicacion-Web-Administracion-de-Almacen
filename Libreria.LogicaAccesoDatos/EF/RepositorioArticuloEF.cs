using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioArticuloEF : IRepositorioArticulo
    {
        private PapeleriaContext _db { get; set; }
        public RepositorioArticuloEF(PapeleriaContext db)
        {
            _db = db;
        }

        public Articulo GetById(int id)
        {
            return _db.Articulos.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Articulo obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(obj.Nombre) || obj.Nombre.Length < 10 || obj.Nombre.Length > 200)
                throw new ArticuloException("El nombre del artículo debe tener entre 10 y 200 caracteres.");

            // Validar código de proveedor
            if (string.IsNullOrEmpty(obj.CodigoProveedor) || obj.CodigoProveedor.Length != 13)
                throw new ArticuloException("El código del proveedor debe tener 13 dígitos.");

            // Verificar si ya existe un artículo con el mismo nombre o código
            if (_db.Articulos.Any(a => a.Nombre == obj.Nombre))
                throw new ArticuloException("Ya existe un artículo con el mismo nombre.");

            if (_db.Articulos.Any(a => a.CodigoProveedor == obj.CodigoProveedor))
                throw new ArticuloException("Ya existe un artículo con el mismo código de proveedor.");

            _db.Articulos.Add(obj);
            _db.SaveChanges();
        }

        public void Update(int id, Articulo obj)
        {
            var articulo = GetById(id);
            if (articulo == null)
                throw new ArticuloException($"No se encontró el artículo con Id {id}");

            articulo.Nombre = obj.Nombre;
            articulo.Descripcion = obj.Descripcion;
            articulo.CodigoProveedor = obj.CodigoProveedor;
            articulo.Precio = obj.Precio;

            _db.SaveChanges();
        }

        public void Remove(int id)
        {
            var articulo = GetById(id);
            if (articulo == null)
                throw new ArticuloException($"No se encontró el artículo con Id {id}");

            _db.Articulos.Remove(articulo);
            _db.SaveChanges();
        }

        public void Remove(Articulo obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _db.Articulos.Remove(obj);
            _db.SaveChanges();
        }

        public IEnumerable<Articulo> GetAll()
        {
            return _db.Articulos.ToList();
        }

        public IEnumerable<dynamic> GetArticulosPorRangoFechas(DateTime fechaInicio, DateTime fechaFin, int numPagina, int cantidadRegistros)
        {
            try
            {
                int numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);

                var articulos = _db.MovientoStocks
                    .Where(m => m.FechaMovimiento >= fechaInicio && m.FechaMovimiento <= fechaFin)
                    .Select(m => m.Articulo)
                    .Distinct()
                    .Skip(numRegistrosAnteriores)
                    .Take(cantidadRegistros)
                    .ToList();

                return articulos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
