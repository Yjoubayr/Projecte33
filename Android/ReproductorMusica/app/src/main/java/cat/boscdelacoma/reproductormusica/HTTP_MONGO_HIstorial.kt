package cat.boscdelacoma.reproductormusica

import android.util.Log
import cat.boscdelacoma.reproductormusica.Apilogic.Canco
import cat.boscdelacoma.reproductormusica.Apilogic.CancoService
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.net.NetworkInterface

class HTTP_MONGO_HIstorial {
    private lateinit var historialService: CancoService
    private var urlBase : String = "http://192.168.1.60:5050/"
    /**
     * Aquest metode ens ajuda a fer un post del historial
     * @param IDDispositiu ID del dispositiu.
     * @param data data de la canço a la que s'ha escoltat.
     * */
    fun postHistorialOfSongs(Nom : String, data : String) {
        // Aquí deberías crear una instancia de Canco con los datos relevantes
        initializeRetrpofit()
        val MAC = obtenerDireccionMac()
        val canco = Canco(MAC ,Nom, data)
        try {
            val call: Call<ResponseBody> = historialService.postcanco(canco)
            call.enqueue(object : retrofit2.Callback<ResponseBody> {
                override fun onResponse(
                    call: Call<ResponseBody>,
                    response: retrofit2.Response<ResponseBody>
                ) {
                    if (response.isSuccessful) {
                        Log.d("Post Historial", "Registro de historial enviado con éxito")
                    } else {
                        Log.e("Post Historial Error", "Error en el envío: ${response.code()}")
                    }
                }

                override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                    Log.e("Post Historial Error", "Fallo en el envío: ${t.message}")
                }
            })
        } catch (Error: Exception) {
            Log.e("Post Historial Error", "Fallo en el envío: ${Error.message}")
        }
    }
    private fun obtenerDireccionMac(): String {
        try {
            val interfaces = NetworkInterface.getNetworkInterfaces()
            while (interfaces.hasMoreElements()) {
                val networkInterface = interfaces.nextElement()
                val mac = networkInterface.hardwareAddress

                if (mac != null) {
                    val sb = StringBuilder()
                    for (b in mac) {
                        sb.append(String.format("%02X:", b))
                    }
                    if (sb.length > 0) {
                        sb.deleteCharAt(sb.length - 1)
                    }
                    return sb.toString()
                }
            }
        } catch (e: Exception) {
            e.printStackTrace()
        }
        return "No se pudo obtener la dirección MAC"
    }    private fun initializeRetrpofit() {
        val gson: Gson = GsonBuilder().setLenient().create()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl(urlBase)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
        historialService = retrofit.create(CancoService::class.java)
    }
}