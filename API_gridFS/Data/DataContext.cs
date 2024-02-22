using Microsoft.EntityFrameworkCore;
namespace gridfsapi {
    /// <summary>
    /// Context de dades per a l'API de GridFS.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Obt� o estableix el nom de la base de dades.
        /// </summary>
        public string DatabaseName { get; set; } = null!;

        /// <summary>
        /// Obt� o estableix la cadena de connexi�.
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Obt� o estableix el nom de la col�lecci� d'�udio.
        /// </summary>
        public string AudioCollectionName { get; set; } = null!;
    }
}