using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class TocarConfiguration : IEntityTypeConfiguration<Tocar> {
        public void Configure(EntityTypeBuilder<Tocar> builder)
        {
            builder.HasKey(p => new { p.IDCanco, p.NomMusic, p.NomGrup, p.NomInstrument });
        }
    }
}