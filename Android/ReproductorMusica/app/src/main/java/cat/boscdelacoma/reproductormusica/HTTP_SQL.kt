package cat.boscdelacoma.reproductormusica

import android.content.Context
import android.util.Log
import android.widget.Toast
import cat.boscdelacoma.reproductormusica.Apilogic.CancoSQL
import cat.boscdelacoma.reproductormusica.Apilogic.CancoSQLService
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class HTTP_SQL {
    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://192.168.1.60:5025/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()

    private val cancosqlservice: CancoSQLService = retrofit.create(CancoSQLService::class.java)

    fun postSONG(IdCanco : Any?,nom: String, any: Int, context: Context) {
        val canco = CancoSQL(IdCanco,nom, any)

        GlobalScope.launch(Dispatchers.IO) {
            try {
                val response: retrofit2.Response<ResponseBody> = cancosqlservice.postcanco(canco).execute()
                if (response.isSuccessful) {
                    launch(Dispatchers.Main) {
                        Toast.makeText(context, "Archivo subido con éxito", Toast.LENGTH_SHORT).show()
                        Log.d("Upload", "subido con éxito ${response.code()}")
                    }
                }else{
                    launch(Dispatchers.Main) {
                        Toast.makeText(context, "Archivo no se ha subido ${response.code()}", Toast.LENGTH_SHORT).show()
                        Log.d("Upload", "subido con sin existo ${response.code()}")
                    }
                }
            } catch (e: Exception) {
                launch(Dispatchers.Main) {
                    Toast.makeText(context, "Archivo no se ha subido ${e.message}", Toast.LENGTH_SHORT).show()
                    Log.d("Upload", "Archivo no se ha subido")
                }
            }
        }
    }
    suspend fun getAllSongs(): Response<List<CancoSQL>> {
        return withContext(Dispatchers.IO) {
            cancosqlservice.getAllCancos().execute()
        }
    }

}