using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)  { }
        public DbSet<Personaje> Personaje { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<PeliculaPersonaje> PeliculaPersonajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Personaje>().ToTable("Personaje");
            modelBuilder.Entity<Pelicula>().ToTable("Pelicula");
            modelBuilder.Entity<Genero>().ToTable("Genero");
            modelBuilder.Entity<PeliculaPersonaje>().ToTable("PeliculaPersonaje");
            modelBuilder.Entity<PeliculaPersonaje>().HasKey(c => new { c.PeliculaId, c.PersonajeId });
        }
    }
}
