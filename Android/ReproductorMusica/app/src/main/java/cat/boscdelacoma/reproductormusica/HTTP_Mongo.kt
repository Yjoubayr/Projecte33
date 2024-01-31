package cat.boscdelacoma.reproductormusica

import android.content.Context
import android.content.Intent
import android.os.Environment
import android.util.Log
import cat.boscdelacoma.reproductormusica.Apilogic.Canco
import cat.boscdelacoma.reproductormusica.Apilogic.CancoService
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import okhttp3.MediaType
import okhttp3.MultipartBody
import okhttp3.RequestBody
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Response
import java.io.File
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.FileOutputStream


class HTTP_Mongo(private val context: Context) {

    private lateinit var audioApi: AudioApiService
    private lateinit var historialService: CancoService

    private var urlBase : String = "http://192.168.18.11:5264/"
    /**
     * Metode per pujar una cançó a l'api de mongoDB gridfs
     * @param uid UID de la cançó.
     * @param filePath Path de la cançó.
     * */
    fun uploadAudio(uid: String, filePath: String) {
        val gson: Gson = GsonBuilder().setLenient().create()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl(urlBase) // Reemplaza con tu base URL
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
        audioApi = retrofit.create(AudioApiService::class.java)
        val audioFile = File(filePath)
        val requestFile: RequestBody = RequestBody.create(MediaType.parse("multipart/form-data"), audioFile)
        val body: MultipartBody.Part = MultipartBody.Part.createFormData("audio", audioFile.name, requestFile)
        val uidPart: RequestBody = RequestBody.create(MediaType.parse("text/plain"), uid)

        val call: Call<ResponseBody> = audioApi.uploadAudio(uidPart, body)

        call.enqueue(object : retrofit2.Callback<ResponseBody> {
            override fun onResponse(call: Call<ResponseBody>, response: retrofit2.Response<ResponseBody>) {
                if (response.isSuccessful) {
                    Log.d("Upload", "Archivo subido con éxito")
                } else {
                    Log.e("Upload Error", "Error en la subida: ${response.code()}")
                }
            }
            override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                Log.e("Upload Error", "Fallo en la subida: ${t.message}")
            }
        })
    }

    fun downloadAudio(uid: String, FileName : String) {
        val gson: Gson = GsonBuilder().setLenient().create()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl("http://192.168.18.11:5264/") // Reemplaza con tu base URL
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()

        audioApi = retrofit.create(AudioApiService::class.java)

        val call: Call<ResponseBody> = audioApi.downloadAudio(uid)

        call.enqueue(object : retrofit2.Callback<ResponseBody> {
            override fun onResponse(call: Call<ResponseBody>, response: retrofit2.Response<ResponseBody>) {
                if (response.isSuccessful) {
                    saveAudioFile(response.body(), FileName)
                    val intent = Intent(context, MainActivity::class.java)
                    val path = Audio().getMp3Path("${FileName}.mp3")
                    intent.putExtra("absolutepathsong", path)
                    intent.putExtra("songname", FileName)
                    context.startActivity(intent)
                } else {
                    Log.e("Download Error", "Error en la descarga: ${response.code()}")
                }
            }

            override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                // Manejar el fallo
                Log.e("Download Error", "Fallo en la descarga: ${t.message}")
            }
        })
    }

    private fun saveAudioFile(body: ResponseBody?, FileName: String) {
        GlobalScope.launch(Dispatchers.IO) {
            try {
                val file =  File(
                    Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC),
                    "${FileName}.mp3"
                ) // Almacenamiento interno de la aplicación
                val inputStream = body?.byteStream()
                val outputStream = FileOutputStream(file)
                val buffer = ByteArray(4096)
                var bytesRead: Int
                while (inputStream?.read(buffer).also { bytesRead = it!! } != -1) {
                    outputStream.write(buffer, 0, bytesRead)
                }
                outputStream.close()
                inputStream?.close()
                Log.d("Download", "Archivo guardado en: ${file.absolutePath}")
            } catch (e: Exception) {
                Log.e("Download Error", "Error al guardar el archivo: ${e.message}")
            }
        }
    }
    /**
     * Aquest metode ens ajuda a fer un post del historial
     * @param IDDispositiu ID del dispositiu.
     * @param data data de la canço a la que s'ha escoltat.
     * */
    fun postHistorialOfSongs(MAC : String ,Nom : String, data : String) {
        // Aquí deberías crear una instancia de Canco con los datos relevantes
        initializeRetrpofit()
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

    private fun initializeRetrpofit() {
        val gson: Gson = GsonBuilder().setLenient().create()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl(urlBase)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
        historialService = retrofit.create(CancoService::class.java)
    }
}