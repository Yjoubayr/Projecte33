package cat.boscdelacoma.reproductormusica

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class DownloadSongs : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_downloadsongs)
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