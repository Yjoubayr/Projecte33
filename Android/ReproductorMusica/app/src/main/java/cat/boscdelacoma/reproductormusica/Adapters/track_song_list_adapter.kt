package cat.boscdelacoma.reproductormusica.Adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.R
import org.w3c.dom.Text

class track_song_list_adapter(private  val trackSongListAdapter: List<trackSongListItem>) : RecyclerView.Adapter<track_song_list_adapter.ViewHolder>() {
    data class trackSongListItem(val trackName: String)

    class ViewHolder(itemView: View): RecyclerView.ViewHolder(itemView){
        val trackName: TextView = itemView.findViewById(R.id.TrackName)
        val addtoPlayList : TextView = itemView.findViewById(R.id.addToPlayList)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.track_songs_list_item, parent, false)
        return ViewHolder(view)
    }
    override fun getItemCount(): Int {
        return trackSongListAdapter.size
    }
    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val currentItem = trackSongListAdapter[position]

        holder.trackName.text = currentItem.trackName

        holder.addtoPlayList.setOnClickListener {
            // TODO Aqui tiene que llegar el nombre de la cancion

        }

    }

}