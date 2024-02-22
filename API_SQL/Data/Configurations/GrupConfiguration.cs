using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class GrupConfiguration : IEntityTypeConfiguration<Grup> {
        public void Configure(EntityTypeBuilder<Grup> builder)
        {
            builder.HasKey(g => new{g.Nom, g.DataFundacio});
        }
    }
}