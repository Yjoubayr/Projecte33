using Microsoft.AspNetCore.Http;

namespace gridfsapi
{
    /// <summary>
    /// Representa una càrrega d'àudio.
    /// </summary>
    public class AudioUpload
    {
        /// <summary>
        /// Obté o estableix l'identificador únic de la càrrega d'àudio.
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Obté o estableix el fitxer d'àudio a carregar.
        /// </summary>
        public IFormFile Audio { get; set; }
    }
}