package cat.boscdelacoma.reproductormusica

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
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.SimpleCursorAdapter
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
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

    fun getMp3File(fileName : String): MediaPlayer?{
        val directory = File(
            Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_MUSIC).toString()
        )
        try {
            if (directory.isDirectory) {
                val listFiles = directory.listFiles()
                val file = listFiles?.filter { it.name == fileName }
                val mediaPlayer = MediaPlayer()
                mediaPlayer.setDataSource(file!!.get(0).absolutePath)
                mediaPlayer.prepare()
                return mediaPlayer
            } else {
                return null
            }
        } catch (e: Exception) {
            return null
        }
    }

}

