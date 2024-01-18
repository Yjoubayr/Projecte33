package cat.boscdelacoma.reproductormusica

import android.app.DownloadManager
import android.content.Context
import android.media.MediaPlayer
import android.net.Uri
import android.os.Environment
import android.util.Log
import android.widget.Toast
import java.io.File

class Audio {

    var titol: String? = ""
    var path: String? = ""
    var autor: String? = ""
    lateinit var uri: Uri
    var duration: String? = ""
    var mediaPlayer: MediaPlayer = MediaPlayer()

    constructor() {}


    /**
     * Metode que ens ajuda a crear una carpeta en el directori de musica.
     * @param carpetaNombre Nom de la carpeta.
     * @return {Boolean} Retorna true si s'ha creat la carpeta.
     * */
    fun createFolder(carpetaNombre: String): Boolean {
        if (carpetaNombre.isNotEmpty()) {
            val carpetaPath =
                Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
                    .toString() + File.separator + carpetaNombre
            val carpeta = File(carpetaPath)
            return carpeta.mkdirs()
        }
        return false
    }

    /**
     * Ens ajuda a descarregar una cançó a partir d'una URL.
     * @param context Context de l'activitat.
     * @param songUrl URL de la cançó.
     * @return {Unit} No retorna res.
     * */
    fun downloadSongAPI(context: Context, songUrl: String) {
        val downloadManager = context.getSystemService(Context.DOWNLOAD_SERVICE) as DownloadManager
        val request = DownloadManager.Request(Uri.parse(songUrl))

        request.setTitle("Descarga de canción")
        request.setDescription("Descargando archivo MP3")

        val musicDirectory =
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val fileName = "cancion_descargada.mp3"
        val destinationFile = File(musicDirectory, fileName)
        request.setDestinationUri(Uri.fromFile(destinationFile))

        val downloadId = downloadManager.enqueue(request)

        Toast.makeText(context, "Descarga iniciada", Toast.LENGTH_SHORT).show()
    }

    /**
     * Obte una llista de noms de fitxers al directori de música,
     * excluint els fitxers ocults i aquells que acaben amb ".mp3".
     * @return {ArrayList<String>} Llistat dels noms.
     */
    fun getAllFilesList(): ArrayList<String> {
        val list: ArrayList<String> = ArrayList()
        val musicDirectory =
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val files = musicDirectory.listFiles()

        if (files != null) {
            for (file in files) {
                val path = file.absolutePath
                val relativePath = path.substring(path.lastIndexOf("/") + 1)
                if (!file.isHidden && !relativePath.startsWith(".")) {
                    if (!path.endsWith(".mp3")) {
                        list.add(relativePath)
                    }
                }
            }
        }

        return list
    }

    /**
     * Ens permet borrar una carpeta del directori de musica.
     * @param folderName Nom de la carpeta.
     * @return {Unit} No retorna res.
     * */
    fun deleteFileInMusicFolder(fileName: String) {
        val musicDirectory =
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val file = File(musicDirectory, fileName)

        if (file.exists()) {
            val success = file.delete()

            if (success) {
                Log.d("File", "El archivo $fileName se ha borrado correctamente.")
            } else {
                Log.d("File", "El archivo $fileName no se ha podido borrar.")
            }
        } else {
            Log.d("File", "El archivo $fileName no existe en la carpeta de música.")
        }
    }

    /**
     * Ens permet borrar una canço d'un directori en especific
     * @param currentItemSong Nom de la canço.
     * @param FolderName Nom de la carpeta.
     * @return {Unit} No retorna res.
     * */
    fun deleteMusicInTrack(currentItemSong: String, FolderName: String) {
        val musicDirectory = File(
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC),
            FolderName
        )
        val file = File(musicDirectory, currentItemSong)

        if (file.exists()) {
            val success = file.delete()

            if (success) {
                Log.d("File", "El archivo $currentItemSong se ha borrado correctamente.")
            } else {
                Log.d("File", "El archivo $currentItemSong no se ha podido borrar.")
            }
        } else {
            Log.d("File", "El archivo $currentItemSong no existe en la carpeta de música.")
        }
    }

    /**
     * Ens permet obtenir una llista de cançons d'un directori en especific
     * @param FolderName Nom de la carpeta.
     * @return {ArrayList<String>} Llistat dels noms.
     * */
    fun getSongList(FolderName: String): ArrayList<String> {
        val list: ArrayList<String> = ArrayList()
        val musicDirectory = File(
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC),
            FolderName
        )
        val files = musicDirectory.listFiles()

        if (files != null) {
            for (file in files) {
                val path = file.absolutePath
                val relativePath = path.substring(path.lastIndexOf(File.separator) + 1)

                // Verificar si el archivo no es oculto
                if (!file.isHidden && !relativePath.startsWith(".")) {
                    if (path.endsWith(".mp3")) {
                        list.add(relativePath)
                    }
                }
            }
        }

        return list
    }

    /**
     * Ens permet obtenir la ruta absoluta d'un fitxer mp3.
     * @param fileName Nom del fitxer.
     * @param folderName Nom de la carpeta.
     * @return {String} Ruta absoluta del fitxer.
     * */
    fun getAbsolutePathMp3File(fileName: String, folderName: String): String {
        val musicDirectory = File(
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC),
            folderName
        )
        val files = musicDirectory.listFiles()
        if (files != null) {
            for (file in files) {
                val path = file.absolutePath
                val relativePath = path.substring(path.lastIndexOf(File.separator) + 1)
                if (!file.isHidden && !relativePath.startsWith(".") && relativePath == fileName){
                    return path.toString()
                }else{
                    return ""
                }
            }
        }else{
            return ""
        }
        return ""
    }


}

