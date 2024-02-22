using Microsoft.EntityFrameworkCore;
namespace gridfsapi {
    /// <summary>
    /// Context de dades per a l'API de GridFS.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Obté o estableix el nom de la base de dades.
        /// </summary>
        public string DatabaseName { get; set; } = null!;

        /// <summary>
        /// Obté o estableix la cadena de connexió.
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Obté o estableix el nom de la col·lecció d'àudio.
        /// </summary>
        public string AudioCollectionName { get; set; } = null!;
    }
}