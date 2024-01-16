using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class VersioConfiguration : IEntityTypeConfiguration<Versio> {
        public void Configure(EntityTypeBuilder<Versio> builder)
        {
            builder.HasKey(v => new { v.IDCanco, v.NomVersio });
        }
    }
}