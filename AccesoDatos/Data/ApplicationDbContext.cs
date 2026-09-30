using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Artista> Artista { get; set; }
        public DbSet<Cancion> Cancion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // El archivo .db se crea junto al ejecutable (no hace falta crear carpetas a mano)
            var ruta = Path.Combine(AppContext.BaseDirectory, "PlataformaMusica.db");
            optionsBuilder.UseSqlite($"Data Source={ruta}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artista>()
                .Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Cancion>()
                .Property(c => c.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            // Relación 1:N -> un Artista tiene muchas Canciones; cada Cancion tiene un único Artista
            modelBuilder.Entity<Cancion>()
                .HasOne(c => c.Artista)
                .WithMany(a => a.Canciones)
                .HasForeignKey(c => c.ArtistaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}