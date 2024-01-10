using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dymj.ReproductorMusica {
    public class CanconsConfiguration : IEntityTypeConfiguration<Canco>
    {
        public void Configure(EntityTypeBuilder<Canco> builder)
        {
            builder.HasKey(c => new { c.ID, c.Nom, c.NomLlista, c.NomDispositiu });
            builder.HasOne(c => c.AlbumObj).WithMany(a => a.LCancons).HasForeignKey(c => c.ID);
        }
    }
}