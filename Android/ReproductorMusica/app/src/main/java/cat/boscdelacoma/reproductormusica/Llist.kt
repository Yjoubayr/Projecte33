package cat.boscdelacoma.reproductormusica

import android.R.attr.path
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.provider.MediaStore
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView


class Llist : AppCompatActivity() {

    //public val lv = ListView()

    private var listName: String? = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_llist)

        val returnBtn : TextView = findViewById(R.id.returnBtn)
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
        returnBtn.setOnClickListener(){
            val intent = Intent(this ,MainActivity::class.java)
            startActivity(intent)
        }
    }

}