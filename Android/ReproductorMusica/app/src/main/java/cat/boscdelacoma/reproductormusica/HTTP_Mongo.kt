package cat.boscdelacoma.reproductormusica

import AudioApiService
import android.content.Context
import android.os.Environment
import android.util.Log
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
import java.io.File
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.FileOutputStream


class HTTP_Mongo(private val context: Context) {

    private lateinit var audioApi: AudioApiService
    /**
     * Metode per pujar una cançó a l'api de mongoDB gridfs
     * @param uid UID de la cançó.
     * @param filePath Path de la cançó.
     * */
    fun uploadAudio(uid: String, filePath: String) {
        val gson: Gson = GsonBuilder().setLenient().create()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl("http://172.23.2.141:5264/") // Reemplaza con tu base URL
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

    /**
     * Metode que ens ajuda a descarregar una cançó a partir d'una UID fent una petició cap a l'api.
     * @param uid UID de la cançó.
     * */
    fun downloadAudio(uid: String) {
        val gson: Gson = GsonBuilder().setLenient().create()
        val retrofit: Retrofit = Retrofit.Builder()
            .baseUrl("http://172.23.2.141:5264/") // Reemplaza con tu base URL
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()

        audioApi = retrofit.create(AudioApiService::class.java)

        val call: Call<ResponseBody> = audioApi.downloadAudio(uid)

        call.enqueue(object : retrofit2.Callback<ResponseBody> {
            override fun onResponse(call: Call<ResponseBody>, response: retrofit2.Response<ResponseBody>) {
                if (response.isSuccessful) {
                    saveAudioFile(response.body() ,"cancion_descargada")
                } else {
                    Log.e("Download Error", "Error en la descarga: ${response.code()}")
                }
            }
            override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                Log.e("Download Error", "Fallo en la descarga: ${t.message}")
            }
        })
    }

    /**
     * Metode que ens permet guardar el fitxer descarregat de la musica.
     * @param body Contingut del fitxer.
     * @param FileName Nom del fitxer.
     * */
    private fun saveAudioFile(body: ResponseBody?, Filename : String) {
        GlobalScope.launch(Dispatchers.IO) {
            try {
                val file =  File(
                    Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC),
                    "${Filename}.mp3"
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
}