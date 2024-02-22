package cat.boscdelacoma.reproductormusica

import okhttp3.MultipartBody
import okhttp3.RequestBody
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Multipart
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Path

interface AudioApiService {
    @GET("FitxersAPI/v1/Audio/GetAudio/{UID}")
    fun downloadAudio(@Path("UID") uid: String): Call<ResponseBody>
    @Multipart
    @POST("FitxersAPI/v1/Audio")
    fun uploadAudio(
        @Part("uid") uid: RequestBody,
        @Part audio: MultipartBody.Part
    ): Call<ResponseBody>
}
