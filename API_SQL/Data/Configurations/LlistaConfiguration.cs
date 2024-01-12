using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class LlistesConfiguration : IEntityTypeConfiguration<Llista> {
        public void Configure(EntityTypeBuilder<Llista> builder)
        {
            builder.HasKey(l => new { l.MACAddress, l.NomLlista });
        }
    }
}