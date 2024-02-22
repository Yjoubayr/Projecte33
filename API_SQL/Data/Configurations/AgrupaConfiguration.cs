using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class AgrupaConfiguration : IEntityTypeConfiguration<Agrupa> {
        public void Configure(EntityTypeBuilder<Agrupa> builder)
        {
        }
    }
}