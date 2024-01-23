using Microsoft.EntityFrameworkCore;
namespace gridfsapi {
    public class DataContext : DbContext {
        public string DatabaseName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string AudioCollectionName { get; set; } = null!;
        
    }
}