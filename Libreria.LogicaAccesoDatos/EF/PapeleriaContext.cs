using Libreria.LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.LogicaAccesoDatos.EF
{
    public class PapeleriaContext:DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<TipoMovimiento> TipoMovimientos { get; set; }
        public DbSet<MovimientoStock> MovientoStocks { get; set; }
        public DbSet<Configuracion> Configuracion { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"SERVER=(localDb)\MsSqlLocalDb;Database=PapeleriaOB2;Integrated Security=true;Encrypt=false");

        }
        public PapeleriaContext(DbContextOptions<PapeleriaContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la clave primaria para la entidad Usuario
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);

            // Opcionalmente, configurar otras propiedades si es necesario

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TipoMovimiento>()
            .HasIndex(tm => tm.Nombre)
            .IsUnique();

        }
    }
}
