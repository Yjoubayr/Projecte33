using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class ParticipaConfiguration : IEntityTypeConfiguration<Participa> {
        public void Configure(EntityTypeBuilder<Participa> builder)
        {
            builder.HasKey(p => new { p.IDCanco, p.NomMusic, p.NomGrup, p.NomInstrument });
        }
    }
}