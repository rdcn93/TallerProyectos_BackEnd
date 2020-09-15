using Microsoft.EntityFrameworkCore;
using TallerProyectos_BackEnd.Models;

namespace TallerProyectos_BackEnd.DataAccess
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }

        #region Usuarios
        public DbSet<Usuario> Usuario { get; set; }
        #endregion

        #region Productos
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Fabricante> Fabricante { get; set; }

        public DbSet<ProductoCategoria> ProductoCategoria { get; set; }
        public DbSet<ProductoImagen> ProductoImagen { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.HasDefaultSchema("Seguridad");


            #region Producto Categoria
            builder.Entity<ProductoCategoria>()
                    .HasKey(bc => new { bc.idProducto, bc.idCategoria });
            builder.Entity<ProductoCategoria>()
                .HasOne(bc => bc.producto)
                .WithMany(b => b.productoCategorias)
                .HasForeignKey(bc => bc.idProducto);
            builder.Entity<ProductoCategoria>()
                .HasOne(bc => bc.categoria)
                .WithMany(c => c.productoCategorias)
                .HasForeignKey(bc => bc.idCategoria);
            #endregion

            #region Producto Imagen
            builder.Entity<ProductoImagen>()
                    .HasKey(bc => new { bc.idProducto, bc.idImagen });
            builder.Entity<ProductoImagen>()
                .HasOne(bc => bc.producto)
                .WithMany(b => b.productoImagenes)
                .HasForeignKey(bc => bc.idProducto);
            builder.Entity<ProductoImagen>()
                .HasOne(bc => bc.imagen)
                .WithMany(c => c.productoImagenes)
                .HasForeignKey(bc => bc.idImagen);
            #endregion

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
