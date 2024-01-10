using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Classes.Model
{
    public class Historial
    {
        [BsonId]
        public string IDDispositiu {get; set;} = null!;
        
    }
}