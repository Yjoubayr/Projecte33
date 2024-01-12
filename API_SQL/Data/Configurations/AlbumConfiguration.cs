using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class AlbumConfiguration : IEntityTypeConfiguration<Album> {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => new { a.Titol, a.Any });
        }
    }
}