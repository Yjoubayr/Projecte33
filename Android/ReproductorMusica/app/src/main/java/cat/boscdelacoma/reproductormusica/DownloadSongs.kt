package cat.boscdelacoma.reproductormusica

import android.Manifest
import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.telecom.Call
import android.util.Log
import android.widget.TextView
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Adapters.SongAdapter
import cat.boscdelacoma.reproductormusica.Apilogic.CancoSQL
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class DownloadSongs : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_downloadsongs)
        val returnBtn : TextView = findViewById(R.id.back)
        returnBtn.setOnClickListener {
            finish()
        }

        lifecycleScope.launch {
            emplenarRecycleView()
        }
    }
    /**
     * Metode per emplenar el recycleView amb les cançons que es poden descarregar.
     * */
    suspend fun emplenarRecycleView(){
        val recyclerView: RecyclerView = findViewById(R.id.recyclerView)
        try{

            val songs = HTTP_SQL().getAllSongs()

            val songList = mutableListOf<SongAdapter.SongItem>()

            val songsBody = songs.body()
            if (songsBody != null) {
                for (i in songsBody.indices) {
                    val songName = "${songsBody[i]?.Nom}"
                    val UID = "${songsBody[i]?.IDCanco}"
                    val songItem = SongAdapter.SongItem(songName = songName, UID = UID)
                    songList.add(songItem)
                }
            }
            withContext(Dispatchers.Main) {
                val adapter = SongAdapter(songList)
                recyclerView.layoutManager = LinearLayoutManager(this@DownloadSongs)
                recyclerView.adapter = adapter
            }

        }catch (e: Exception){
            Log.d("GetAllSongs", "Error: ${e.message}")
        }
    }

}