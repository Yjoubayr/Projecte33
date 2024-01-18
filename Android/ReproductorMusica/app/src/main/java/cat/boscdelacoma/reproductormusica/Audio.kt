package cat.boscdelacoma.reproductormusica

import android.app.DownloadManager
import android.content.ContentResolver
import android.content.ContentValues
import android.content.Context
import android.database.Cursor
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.media.MediaMetadataRetriever
import android.media.MediaPlayer
import android.net.Uri
import android.os.Build
import android.os.Environment
import android.provider.MediaStore
import android.util.Log
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.SimpleCursorAdapter
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.core.content.ContextCompat.getSystemService
import androidx.core.net.toUri
import androidx.loader.content.CursorLoader
import java.io.BufferedReader
import java.io.File
import java.io.FileOutputStream
import java.io.InputStream
import java.io.InputStreamReader
import java.io.IOException
import java.io.OutputStream
import javax.xml.transform.URIResolver
import kotlin.io.path.Path

class Audio {

    var titol: String? = ""
    var path: String? = ""
    var autor: String? = ""
    lateinit var uri: Uri
    var duration: String? = ""
    var mediaPlayer: MediaPlayer = MediaPlayer()

    constructor() {}

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
    public fun getFile(fileName: String): File? {
        val directory = File(
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC).toString()
        )
        try {
            if (directory.isDirectory) {
                val listFiles = directory.listFiles()
                val file = listFiles?.filter { it.name == fileName }
                return file!!.get(0)
            } else {
                return null
            }
        } catch (e: Exception) {
            return null
        }
    }
    fun downloadSongAPI(context: Context, songUrl: String) {
        val downloadManager = context.getSystemService(Context.DOWNLOAD_SERVICE) as DownloadManager
        val request = DownloadManager.Request(Uri.parse(songUrl))

        request.setTitle("Descarga de canción")
        request.setDescription("Descargando archivo MP3")

        val musicDirectory = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val fileName = "cancion_descargada.mp3"
        val destinationFile = File(musicDirectory, fileName)
        request.setDestinationUri(Uri.fromFile(destinationFile))

        val downloadId = downloadManager.enqueue(request)

        Toast.makeText(context, "Descarga iniciada", Toast.LENGTH_SHORT).show()
    }

    fun getMusicFiles(context: Context) {
        val musicDirectory = Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val uri: Uri? = MediaStore.Audio.Media.getContentUriForPath(musicDirectory.absolutePath)
        val projection = arrayOf(
            MediaStore.Audio.Media._ID,
            MediaStore.Audio.Media.DATA
        )

        val selection = "${MediaStore.Audio.Media.DATA} like ?"
        val selectionArgs = arrayOf("${musicDirectory.absolutePath}%")

        val sortOrder = "${MediaStore.Audio.Media.DEFAULT_SORT_ORDER} ASC"

        val contentResolver: ContentResolver = context.contentResolver
        val cursor =
            uri?.let { contentResolver.query(it, projection, selection, selectionArgs, sortOrder) }

        if (cursor != null) {
            try {
                while (cursor.moveToNext()) {
                    val filePath = cursor.getString(cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.DATA))
                    if (filePath.endsWith(".mp3")) {
                        Toast.makeText(context, filePath, Toast.LENGTH_SHORT).show()
                    }
                }
            } catch (e: Exception) {
                Log.e("getMusicFiles", "Error while processing music files", e)
            } finally {
                cursor.close()
            }
        }
    }

    fun getAllFilesList(): ArrayList<String> {
        val list: ArrayList<String> = ArrayList()
        val musicDirectory =
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC)
        val files = musicDirectory.listFiles()
        if (files != null) {
            for (file in files) {
                val path = file.absolutePath
                if (path.endsWith(".mp3")){
                    continue;
                }
                list.add(path)
            }
        }
        return list
    }
}

