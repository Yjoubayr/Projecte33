using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dymj.ReproductorMusica.API_SQL.Configurations {
    public class AlbumConfiguration : IEntityTypeConfiguration<Album> {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => new { a.Titol, a.Any });
        }
    }
}