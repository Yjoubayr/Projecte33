package cat.boscdelacoma.reproductormusica

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Adapters.SongAdapter

class DownloadSongs : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_downloadsongs)
        val returnBtn : TextView = findViewById(R.id.back)
        returnBtn.setOnClickListener {
            finish()
        }

        emplenarRecycleView()
    }

    /**
     * Metode per emplenar el recycleView amb les cançons que es poden descarregar.
     * */
    fun emplenarRecycleView(){
        val recyclerView: RecyclerView = findViewById(R.id.recyclerView)
        val songList = mutableListOf<SongAdapter.SongItem>()


        for (i in 1..60) {
            val songName = "Song $i"
            val songItem = SongAdapter.SongItem(songName = songName)
            songList.add(songItem)
        }

        val adapter = SongAdapter(songList)
        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = adapter

    }
}