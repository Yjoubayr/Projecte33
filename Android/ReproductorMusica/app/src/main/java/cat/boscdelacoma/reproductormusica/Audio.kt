package cat.boscdelacoma.reproductormusica

import android.app.DownloadManager
import android.content.ContentValues
import android.content.Context
import android.media.MediaPlayer
import android.net.Uri
import android.os.Build
import android.os.Environment
import android.provider.MediaStore
import android.util.Log
import android.widget.Toast
import androidx.annotation.RequiresApi
import java.io.File
import java.io.FileOutputStream
import java.nio.file.Files
import java.nio.file.Path
import java.nio.file.StandardCopyOption



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
     * Obte una llista de noms de fitxers al directori de música,
     * excluint els fitxers ocults i aquells que acaben amb ".mp3".
     * @return {ArrayList<String>} Llistat dels noms.
     */
    fun getAllFilesListMP3(): ArrayList<String> {
        val list: ArrayList<String> = ArrayList()
        val musicDirectory =
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val files = musicDirectory.listFiles()

        if (files != null) {
            for (file in files) {
                val path = file.absolutePath
                val relativePath = path.substring(path.lastIndexOf("/") + 1)
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

    /**
     * Ens permet obtenir tota la ruta d'un directori en concret
     * @param FolderName Nom de la carpeta a obtenir
     * @return {String} Ens retorna una string que fa referencia al path de la carpeta
     * */
    fun getFolderPath(folderName: String): String {
        val musicDirectory = File(
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC),
            folderName
        )
        return  musicDirectory.toString()
    }

    /**
     * Ens permet obtenir la ruta total de les cançons
     * @param FileName Es el nom del arxiu
     * */
    fun getMp3Path(fileName: String): String {
        val musicDirectory = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)

        if (musicDirectory.exists() && musicDirectory.isDirectory) {
            val musicFolder = File(musicDirectory, fileName)

            if (musicFolder.exists() && musicFolder.isFile) {
                return musicFolder.absolutePath
            } else {
                return "El archivo MP3 '$fileName' no se encontró en el directorio de música."
            }
        } else {
            return "El directorio de música no existe o no es un directorio válido."
        }
    }
    /**
     * Ens permet crear un link d'una canço dins d'un directori
     * @param songPath Nom del fitxer
     * @param FolderName Nom de la carpeta
     * @return {Unit} No retorna res
     * */
    fun putSongIntoPlaylist(songPath: Path, folderPath: Path): Boolean {
        try {
            if (!Files.exists(songPath)) {
                println("Error: La cançó no existeix en la ruta especificada.")
                return false
            }
            if (!Files.exists(folderPath)) {
                Files.createDirectories(folderPath)
            }
            val destinationPath = folderPath.resolve(songPath.fileName)
            Files.copy(songPath, destinationPath, StandardCopyOption.REPLACE_EXISTING)
            println("La cançó s'ha afegit a la carpeta amb èxit.")
            return true
        } catch (e: Exception) {
            println("S'ha produït un error en afegir la cançó a la carpeta: ${e.message}")
            return false
        }
    }
}

