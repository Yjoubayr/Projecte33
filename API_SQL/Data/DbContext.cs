using dymj.ReproductorMusica;
using Microsoft.EntityFrameworkCore;

namespace mba.basquet {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Canco> Cancons { get; set; }
        public DbSet<Album> Albums { get; set; }

        public DbSet<Grup> Grups { get; set; }
        public DbSet<Artista> Artistes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}