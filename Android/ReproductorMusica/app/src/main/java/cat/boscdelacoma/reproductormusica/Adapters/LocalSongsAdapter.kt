package cat.boscdelacoma.reproductormusica.Adapters

import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Audio
import cat.boscdelacoma.reproductormusica.MainActivity
import cat.boscdelacoma.reproductormusica.R

class LocalSongsAdapter(private val localSongList: List<LocalSongItem>) : RecyclerView.Adapter<LocalSongsAdapter.ViewHolder>() {

    data class LocalSongItem(
        val songName: String,
        val songPath : String
    )
    class ViewHolder(itemView: View): RecyclerView.ViewHolder(itemView){
        val trackName: TextView = itemView.findViewById(R.id.Text_Box)
        val playBtn : TextView = itemView.findViewById(R.id.Play_Btn)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.local_songs_item, parent, false)
        return ViewHolder(view)
    }

    override fun getItemCount(): Int {
        return localSongList.size
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val currentItem = localSongList[position]

        holder.trackName.text = currentItem.songName

        holder.playBtn.setOnClickListener {
            val path : String= Audio().getMp3Path(currentItem.songPath)
            val intent = Intent(holder.itemView.context, MainActivity::class.java)

            intent.putExtra("absolutepathsong", path)
            intent.putExtra("songName", currentItem.songName)

            holder.itemView.context.startActivity(intent)
        }
    }
}