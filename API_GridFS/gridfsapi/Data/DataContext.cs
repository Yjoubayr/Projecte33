using Microsoft.EntityFrameworkCore;
using gridfsapi.Model;
namespace gridfsapi.DataContext
{
    public class DataContext : DbContext {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Audio> Audios { get; set; }

        // Necessari només quan l'aplicació és executada en mode consola - per tal que funcioni migrations add
        
    }
}