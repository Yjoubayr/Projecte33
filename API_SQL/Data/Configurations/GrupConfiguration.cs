using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class GrupConfiguracio : IEntityTypeConfiguration<Grup> {
        public void Configure(EntityTypeBuilder<Grup> builder)
        {

        }
    }
}