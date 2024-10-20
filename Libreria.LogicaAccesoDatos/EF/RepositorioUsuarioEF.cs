using Libreria.LogicaNegocio.Entidades;
using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class RepositorioUsuarioEF : IRepositorioUsuario
    {
        private PapeleriaContext _db { get; set; }
        public RepositorioUsuarioEF(PapeleriaContext db)
        {
            _db = db;
        }
        public void Add(Usuario obj)
        {
            obj.EsValido();
            _db.Usuarios.Add(obj);
            _db.SaveChanges();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _db.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return _db.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public void Remove(int id)
        {
            var usuario = GetById(id);
            if (usuario == null)
                throw new ArticuloException($"No se encontró el Usuario con Id {id}");

            _db.Usuarios.Remove(usuario);
            _db.SaveChanges();
        }

        public void Remove(Usuario obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            _db.Usuarios.Remove(obj);
            _db.SaveChanges();
        }

        public void Update(int id, Usuario obj)
        {
            Usuario u = GetById(id);
            if (u == null)
                throw new UsuarioException($"No se encontró el usuario con Id {id}");

            u.NombreCompleto = obj.NombreCompleto;
            u.Password = obj.Password;
            u.PasswordSE = obj.PasswordSE;

            _db.SaveChanges();
        }

        public Usuario ObtenerPorEmail(object email)
        {
            return _db.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public bool ExisteCorreoElectronico(string email)
        {
            return _db.Usuarios.Any(u => u.Email == email);
        }
    }
}
