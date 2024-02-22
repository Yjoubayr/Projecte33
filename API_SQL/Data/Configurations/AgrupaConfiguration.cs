using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class AgrupaConfiguration : IEntityTypeConfiguration<Agrupa> {
        public void Configure(EntityTypeBuilder<Agrupa> builder)
        {
            builder.HasKey(a => new {a.IDGrup, a.IDMusic, a.DataIncorporacio});
            builder.HasOne(a => a.GrupObj).WithMany(g => g.LAgrupes).HasForeignKey(a => a.IDGrup);
            builder.HasOne(a => a.MusicObj).WithMany(m => m.LAgrupes).HasForeignKey(a => a.IDMusic);
        }
    }
}