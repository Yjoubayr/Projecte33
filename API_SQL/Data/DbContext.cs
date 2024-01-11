using dymj.ReproductorMusica;
using Microsoft.EntityFrameworkCore;

namespace dymj.ReproductorMusica {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Canco> Cancons { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Extensio> Extensions { get; set; }
        public DbSet<Grup> Grups { get; set; }
        public DbSet<Music> Artistes { get; set; }
        public DbSet<Llista> Llista { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new LlistesConfiguration());
        }
    }
}