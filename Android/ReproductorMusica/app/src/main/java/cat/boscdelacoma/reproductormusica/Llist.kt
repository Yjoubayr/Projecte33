package cat.boscdelacoma.reproductormusica

import android.content.Intent
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Adapters.TrackAdapter


class Llist : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_llist)
        val returnBtn : TextView = findViewById(R.id.returnBtn)
        fillRecycleView()


        returnBtn.setOnClickListener(){
            finish()
        }
    }

    /**
     * Metode per emplenar el recycleView amb les carpetes de musica.
     * @return {Unit} No retorna res.
     * */
    fun fillRecycleView(){
        val recyclerView: RecyclerView = findViewById(R.id.recyclerView)
        val trackList: MutableList<TrackAdapter.TrackItem> = mutableListOf()
        val list = Audio().getAllFilesList()

        for (i in 1..list.size) {
            val trackName = list[i-1].toString()
            val trackItem = TrackAdapter.TrackItem(trackName = trackName)
            trackList.add(trackItem)
        }

        val adapter = TrackAdapter(trackList)
        recyclerView.layoutManager = androidx.recyclerview.widget.LinearLayoutManager(this)
        recyclerView.adapter = adapter

    }
}