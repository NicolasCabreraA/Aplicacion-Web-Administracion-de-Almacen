using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioMovimientoStockEF : IRepositorioMovimientoStock
    {
        private readonly PapeleriaContext _db;

        public RepositorioMovimientoStockEF(PapeleriaContext db)
        {
            _db = db;
        }

        public void Add(MovimientoStock obj)
        {
            if (obj == null)
                throw new ArgumentNullException("El movimiento de stock no puede ser nulo");

            try
            {
                // Validación: Las cantidades deben ser positivas.
                if (obj.CantidadAMover <= 0)
                    throw new ArgumentException("La cantidad debe ser positiva");

                // Validación: El usuario debe existir y tener el rol de encargado.
                var usuario = _db.Usuarios.SingleOrDefault(u => u.Email == obj.EmailUsuario);
                if (usuario == null || usuario.Rol != "Encargado")
                {
                    throw new UnauthorizedAccessException("El usuario no existe o no tiene el rol de encargado");
                }



                // Validación: El artículo debe ser uno de los existentes.
                var articulo = _db.Articulos.Find(obj.ArticuloId);
                if (!_db.Articulos.Any(a => a.Id == obj.ArticuloId))
                    throw new ArgumentException("El artículo no existe");
                obj.Articulo = articulo;
                // Validación: Solo se manejan ingresos y egresos.
                var tipoMovimiento = _db.TipoMovimientos.Find(obj.TipoId);
                if (tipoMovimiento.Descripcion != "Ingreso" && tipoMovimiento.Descripcion != "Egreso")
                    throw new ArgumentException("El tipo de movimiento debe ser 'Ingreso' o 'Egreso'");
                obj.Tipo = tipoMovimiento;

                // Fecha del sistema
                obj.FechaMovimiento = DateTime.Now;

                _db.MovientoStocks.Add(obj);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    SqlException exSql = ex.InnerException as SqlException;
                    if (exSql != null && (exSql.Number == 2601 || exSql.Number == 2627))
                    {
                        throw new Exception("Ya existe un movimiento de stock con los mismos datos");
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public IEnumerable<MovimientoStock> GetAll()
        {
            try
            {
                return _db.MovientoStocks
                    .ToList();
            }
            catch (Exception ex)
            {
                return new List<MovimientoStock>();
            }
        }

        public MovimientoStock GetById(int id)
        {
            try
            {
                return _db.MovientoStocks
                    .SingleOrDefault(ms => ms.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<dynamic> GetResumenMovimiento()
        {
            try
            {
                var resumen = _db.MovientoStocks
                    .Include(m => m.Tipo) // Incluye la entidad TipoMovimiento
                    .GroupBy(m => m.FechaMovimiento.Year)
                    .Select(g => new
                    {
                        Ano = g.Key,
                        Tipos = g.GroupBy(m => m.Tipo.Nombre)
                            .Select(tg => new { Tipo = tg.Key, Cantidad = tg.Sum(m => m.CantidadAMover) })
                            .ToList(),
                        Total = g.Sum(m => m.CantidadAMover)
                    })
                    .ToList();

                return resumen;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el resumen de movimientos", ex);
            }
        }

        // TO DO HACER QUE EN LUGAR DE COMPARAR POR tipoId AGARRE EL TIPO Y LO COMPARE POR DESCRIPCION(Ingreso o Egreso)
        public IEnumerable<dynamic> GetMovimientosPorArticuloYTipo(int articuloId, int tipoId)
        {
            try
            {
                var movimientos = _db.MovientoStocks
                .Where(m => m.ArticuloId == articuloId && m.TipoId == tipoId)
                .OrderByDescending(m => m.FechaMovimiento)
                .ThenBy(m => m.CantidadAMover)
                .Select(m => new
                {
                    Id = m.Id,
                    NombreArticulo = m.Articulo.Nombre,
                    NombreTipo = m.Tipo.Nombre,
                    Fecha = m.FechaMovimiento,
                    Cantidad = m.CantidadAMover
                })
                .ToList();
                return movimientos;
            }catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }


        #region Paginación

        private static int cantMovimientos = 0;
        public IEnumerable<dynamic> FindMovsPaginados(int articuloId, int tipoId, int numPagina, int cantidadRegistros)
        {
            try
            {
                if (numPagina <= 1)
                    cantMovimientos = _db.MovientoStocks.Count();
                int numRegistrosAnteriores = cantidadRegistros * (numPagina - 1);
                IEnumerable<dynamic> losDocentes =
                    GetMovimientosPorArticuloYTipo(articuloId, tipoId)//.Include(doc => doc.MiCategoria)

                    .Skip(numRegistrosAnteriores)
                    .Take(cantidadRegistros)
                    .ToList();
                if (!losDocentes.Any())
                    cantMovimientos = 0;
                return losDocentes;

            }
            catch (Exception ex)
            {
                return new List<MovimientoStock>();
            }

        }

        #endregion
        


    }

}


