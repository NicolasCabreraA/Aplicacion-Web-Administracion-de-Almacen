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
    public class RepositorioTipoMovimientoEF : IRepositorioTipoMovimiento
    {
        private PapeleriaContext _db;

        public RepositorioTipoMovimientoEF(PapeleriaContext db)
        {
            _db = db;
        }

        #region CRUD básicas

        public void Add(TipoMovimiento obj)
        {
            if (obj == null)
                throw new ArgumentNullException("El tipo de movimiento no puede ser nulo");

            try
            {
                _db.TipoMovimientos.Add(obj);
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    SqlException exSql = ex.InnerException as SqlException;
                    if (exSql != null && (exSql.Number == 2601 || exSql.Number == 2627))
                    {
                        throw new Exception("Ya existe un tipo de movimiento con ese nombre");
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<TipoMovimiento> GetAll()
        {
            try
            {
                return _db.TipoMovimientos.ToList();
            }
            catch (Exception ex)
            {
                return new List<TipoMovimiento>();
            }
        }

        public TipoMovimiento GetById(int id)
        {
            try
            {
                var tipoMovimiento = _db.TipoMovimientos.Find(id);
                return tipoMovimiento;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var entity = _db.TipoMovimientos.Find(id);
                if (entity != null)
                {
                    bool isUsed = _db.MovientoStocks.Any(m => m.TipoId == id);
                    if (isUsed)
                    {
                        throw new InvalidOperationException("No se puede eliminar el tipo de movimiento porque está siendo utilizado en algún movimiento.");
                    }

                    _db.TipoMovimientos.Remove(entity);
                    _db.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException("El tipo de movimiento no existe");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Remove(TipoMovimiento obj)
        {
            if (obj == null)
                throw new ArgumentNullException("El tipo de movimiento no puede ser nulo");

            try
            {
                bool isUsed = _db.MovientoStocks.Any(m => m.TipoId == obj.Id);
                if (isUsed)
                {
                    throw new InvalidOperationException("No se puede eliminar el tipo de movimiento porque está siendo utilizado en algún movimiento.");
                }

                _db.TipoMovimientos.Remove(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(int id, TipoMovimiento obj)
        {
            if (obj == null)
                throw new ArgumentNullException("El tipo de movimiento no puede ser nulo");

            if (id != obj.Id)
                throw new InvalidOperationException("El Id del tipo de movimiento no puede ser modificado");

            try
            {
                var existingEntity = _db.TipoMovimientos.Find(id);
                if (existingEntity == null)
                    throw new KeyNotFoundException("El tipo de movimiento no existe");

                // Verificar si el nuevo nombre ya existe en otro registro
                var duplicate = _db.TipoMovimientos.FirstOrDefault(tm => tm.Nombre == obj.Nombre && tm.Id != id);
                if (duplicate != null)
                    throw new InvalidOperationException("El nombre del tipo de movimiento ya existe.");

                // No modificar el ID, sólo actualizar las propiedades restantes
                obj.Id = existingEntity.Id;

                // Actualizar las propiedades del existente con los valores de obj, excepto el Id
                _db.Entry(existingEntity).CurrentValues.SetValues(obj);
                _db.Entry(existingEntity).State = EntityState.Modified;

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the entity changes.", ex);
            }
        }


        #endregion
    }


}
