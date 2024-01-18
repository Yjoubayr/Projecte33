package cat.boscdelacoma.reproductormusica

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Adapters.SongInTrackAdapter
import cat.boscdelacoma.reproductormusica.Adapters.TrackAdapter

class TrackSongs : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_track_songs)

        val recyclerView: RecyclerView = findViewById(R.id.recyclerView)
        val trackSongList : MutableList<SongInTrackAdapter.SongItem> = mutableListOf()
        val playlistName = intent.getStringExtra("playlistName")
        val returnBtn : TextView = findViewById(R.id.back)
        val tittle : TextView = findViewById(R.id.track_name)

        tittle.text = playlistName.toString()
        val list = Audio().getSongList(playlistName.toString())
        SongInTrackAdapter.folderName = playlistName.toString()
        for (i in 1..list.size) {
            val songName = list[i-1].toString()
            val songItem = SongInTrackAdapter.SongItem(songName = songName)
            trackSongList.add(songItem)
        }
        val adapter = SongInTrackAdapter(trackSongList)
        recyclerView.layoutManager= LinearLayoutManager(this)
        recyclerView.adapter = adapter

        returnBtn.setOnClickListener(){
            finish()
        }

    }
}