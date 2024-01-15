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
        val songList = listOf(

            SongAdapter.SongItem(songName ="Song 1"),
            SongAdapter.SongItem(songName = "Song 2"),
            // Add other SongItem objects as needed
        )
        val adapter = SongAdapter(songList)
        recyclerView.layoutManager = LinearLayoutManager(this)
        recyclerView.adapter = adapter

    }
}