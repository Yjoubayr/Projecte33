package cat.boscdelacoma.reproductormusica

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Adapters.LocalSongsAdapter

class ShowLocalSongs : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_show_local_songs)
        fillRecycleView()
    }

    fun fillRecycleView(){
        val recyclerView: RecyclerView = findViewById(R.id.recyclerView)
        val SongsList: MutableList<LocalSongsAdapter.LocalSongItem> = mutableListOf()
        val list = Audio().getAllFilesListMP3()

        for (i in 1..list.size) {
            val SongsName = list[i-1].toString()
            val newSongName = SongsName.substring(0, SongsName.length - 4)
            val trackItem = LocalSongsAdapter.LocalSongItem(newSongName, SongsName)
            SongsList.add(trackItem)
        }

        val adapter = LocalSongsAdapter(SongsList)
        recyclerView.layoutManager = androidx.recyclerview.widget.LinearLayoutManager(this)
        recyclerView.adapter = adapter
    }
}