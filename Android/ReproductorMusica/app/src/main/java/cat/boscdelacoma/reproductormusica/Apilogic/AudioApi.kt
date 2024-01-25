
import okhttp3.ResponseBody
import retrofit2.http.GET
import retrofit2.http.Path
import com.google.gson.JsonObject

data class AudioResponse(val filename: String)

interface AudioApi {
    @GET("/api/Audio/{audioId}")
    suspend fun getAudio(@Path("audioId") audioId: String): ResponseBody

    @GET("/api/Audio/SongName/{idCanco}")
    suspend fun getJson(@Path("idCanco") idCanco: String): JsonObject
}
