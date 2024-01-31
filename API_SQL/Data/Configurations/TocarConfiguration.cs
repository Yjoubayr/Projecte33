using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dymj.ReproductorMusica.API_SQL.Model;

namespace dymj.ReproductorMusica.API_SQL.Configuration {
    public class TocarConfiguration : IEntityTypeConfiguration<Tocar> {
        public void Configure(EntityTypeBuilder<Tocar> builder)
        {
            builder.HasKey(t => new { t.IDCanco, t.NomMusic, t.NomInstrument });
            builder.HasOne(t => t.CancoObj).WithMany(c => c.LTocar).HasForeignKey(t => t.IDCanco);
            builder.HasOne(t => t.MusicObj).WithMany(m => m.LTocar).HasForeignKey(t => t.NomMusic);
            builder.HasOne(t => t.GrupObj).WithMany(g => g.LTocar).HasForeignKey(t => t.NomGrup);
            builder.HasOne(t => t.InstrumentObj).WithMany(i => i.LTocar).HasForeignKey(t => t.NomInstrument);
        }
    }
}