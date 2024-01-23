package cat.boscdelacoma.reproductormusica

import com.github.kittinunf.fuel.Fuel
import java.io.File

class HTTP_Mongo {
    private val apiUrl = "URL_DE_TU_API_MONGODB"

    /**
     * Metodo que ens permet descarregar una cançó de l'api de mongo
     * @param songId Id de la cancion
     * @return {ByteArray} Retorna un array de bytes con la cancion
     * */
    fun downloadSongsFromAPI(songId: String): ByteArray? {
        val (_, response, result) = Fuel.get("$apiUrl/songs/$songId")
            .response()
        return when (response.statusCode) {
            200 -> result.component1()
            else -> {
                null
            }
        }
    }

    /**
     * Metode que ens permet pujar una cançó a l'api de mongo
     * @param filePath Ruta de la cançó
     * @return {Boolean} Retorna true si s'ha pujat la cançó
     * */
    fun uploadSongToAPI(filePath: String): Boolean {
        val songFile = File(filePath)
        if (!songFile.exists()) {
            return false
        }

        val (_, response, _) = Fuel.post("$apiUrl/songs")
            .body(songFile)
            .response()

        return response.statusCode == 200
    }
}