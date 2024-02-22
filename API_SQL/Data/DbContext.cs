using dymj.ReproductorMusica;
using Microsoft.EntityFrameworkCore;
using dymj.ReproductorMusica.API_SQL.Model;
using dymj.ReproductorMusica.API_SQL.Configuration;

namespace dymj.ReproductorMusica.API_SQL.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Canco> Cancons { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Extensio> Extensions { get; set; }
        public DbSet<Grup> Grups { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Llista> Llista { get; set; }
        public DbSet<Tocar> Tocar { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Agrupa> Agrupa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new LlistesConfiguration());
            modelBuilder.ApplyConfiguration(new TocarConfiguration());
            modelBuilder.ApplyConfiguration(new GrupConfiguration());
        }
    }
}