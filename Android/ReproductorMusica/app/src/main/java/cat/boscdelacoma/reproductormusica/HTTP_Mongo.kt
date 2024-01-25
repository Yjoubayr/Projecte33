package cat.boscdelacoma.reproductormusica

import AudioApi
import android.content.ContentValues
import android.content.Context
import android.os.Build
import android.os.Environment
import android.provider.MediaStore
import androidx.annotation.RequiresApi
import com.github.kittinunf.fuel.Fuel
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import okhttp3.ResponseBody
import java.io.File
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.FileOutputStream


class HTTP_Mongo(private val context: Context) {
    private lateinit var audioApi: AudioApi

    @RequiresApi(Build.VERSION_CODES.Q)
    fun init() {
        val gson = Gson()
        val retrofit = createRetrofitInstance()

        audioApi = retrofit.create(AudioApi::class.java)
    }

    fun createRetrofitInstance(): Retrofit {
        val gson = GsonBuilder()
            .setLenient()
            .create()

        return Retrofit.Builder()
            .baseUrl("http://172.23.2.141:5264")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
    }

    /**
     * Metodo que ens permet descarregar una cançó de l'api de mongo
     * @param songId GUID de la cancion
     * @return {ByteArray} Retorna un array de bytes con la cancion
     * */
    fun DownloadSongFromMongoDb(audioId : String) {
        init()
        GlobalScope.launch(Dispatchers.Main) {
            try {

                val responseBody: ResponseBody = audioApi.getAudio(audioId)
                val audioBytes: ByteArray? = responseBody.bytes()

                if (audioBytes != null) {
                    println("Audio data is not null.")
                    saveAudioToMediaStore(audioBytes, "test.mp3")
                } else {
                    println("Error: Audio data is null.")
                }
            } catch (e: Exception) {
                println("${e.message}")
            }
        }
    }
    /**
     * Metode que ens permet pujar una cançó a l'api de mongo
     * @param filePath Ruta de la cançó
     * @return {Boolean} Retorna true si s'ha pujat la cançó
     * */
    fun uploadSongToAPI(filePath: String): Boolean {
        init()
        return true
    }

    /**
     * Metode que nes permet guardar un fitxer de audio a la memoria del dispositiu
     * @param audioBytes Array de bytes de la cançó
     * @param fileName Nom del fitxer
     * */
    @RequiresApi(Build.VERSION_CODES.Q)
    private fun saveAudioToMediaStore(audioBytes: ByteArray, fileName: String) {
        val resolver = context.contentResolver
        val contentValues = ContentValues().apply {
            put(MediaStore.Audio.Media.DISPLAY_NAME, fileName)
            put(MediaStore.Audio.Media.MIME_TYPE, "audio/mp3")
            put(MediaStore.Audio.Media.RELATIVE_PATH, Environment.DIRECTORY_MUSIC)
        }
        val audioUri = resolver.insert(MediaStore.Audio.Media.EXTERNAL_CONTENT_URI, contentValues)
        audioUri?.let { uri ->
            resolver.openFileDescriptor(uri, "w", null)?.use { parcelFileDescriptor ->
                val fileDescriptor = parcelFileDescriptor.fileDescriptor
                val fileOutputStream = FileOutputStream(fileDescriptor)
                fileOutputStream.write(audioBytes)
                fileOutputStream.close()

                // Notify media scanner about the new file
                resolver.update(uri, contentValues, null, null)
            }
        }
    }
}