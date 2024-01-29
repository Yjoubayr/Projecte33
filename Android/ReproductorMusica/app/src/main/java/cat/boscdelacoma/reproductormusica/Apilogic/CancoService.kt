package cat.boscdelacoma.reproductormusica.Apilogic

import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.POST


interface CancoService {
    @POST("api/v1/Canco")
    fun postcanco(@Body request: Canco): Call<ResponseBody>
}
