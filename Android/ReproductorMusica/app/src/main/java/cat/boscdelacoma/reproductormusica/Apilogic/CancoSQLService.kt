package cat.boscdelacoma.reproductormusica.Apilogic

import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface CancoSQLService {
    @POST("api/Canco/postCanco")
    fun postcanco(@Body request: CancoSQL): Call<ResponseBody>
    @GET("api/Canco/getDataCancons")
    fun getAllCancos(): Call<List<CancoSQL>>
}
