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

        for (i in 1..60) {
            val trackName = "Track $i"
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


    private fun getFiles(context: Context, folderName: String): List<Audio> {
        var audioFiles = mutableListOf<Audio>()


        val selection = "${MediaStore.Audio.Media.IS_MUSIC} != 0"

        val audioCursor = context.contentResolver.query(
            MediaStore.Audio.Media.EXTERNAL_CONTENT_URI,
            null,
            MediaStore.Audio.Media.DATA + " LIKE ? AND " + MediaStore.Audio.Media.DATA + " NOT LIKE ?",
            arrayOf(
                path.toString() + "%", path.toString() + "%/%"
            ),
            MediaStore.Audio.Media.DISPLAY_NAME + " ASC"
        )

        if (audioCursor != null && audioCursor.moveToFirst()) {
            val dnColumn = audioCursor.getColumnIndex(MediaStore.Audio.Media.DISPLAY_NAME)
            val durationColumn = audioCursor.getColumnIndex(MediaStore.Audio.Media.DURATION)
            val dataColumn = audioCursor.getColumnIndex(MediaStore.Audio.Media.DATA)

            val fileIds: MutableList<Long> = ArrayList()
            do {
                var audio = Audio()
                audio.path = audioCursor.getString(dataColumn)
                audio.duration = audioCursor.getString(durationColumn)
                audio.titol = audioCursor.getString(dnColumn)
                audioFiles.add(audio)
            } while (audioCursor.moveToNext())

        }

        return audioFiles
    }

}