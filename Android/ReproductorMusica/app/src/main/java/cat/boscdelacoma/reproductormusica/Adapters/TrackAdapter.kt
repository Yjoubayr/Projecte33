package cat.boscdelacoma.reproductormusica.Adapters

import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Audio
import cat.boscdelacoma.reproductormusica.MainActivity
import cat.boscdelacoma.reproductormusica.R
import cat.boscdelacoma.reproductormusica.TrackSongs

class TrackAdapter(private val trackList: List<TrackItem>): RecyclerView.Adapter<TrackAdapter.ViewHolder>(){
    data class TrackItem(val trackName: String)

    class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val trackName: TextView = itemView.findViewById(R.id.track_name)
        val deleteTrack: TextView = itemView.findViewById(R.id.delete_track)
        val playtrack : TextView = itemView.findViewById(R.id.play_track)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.track_item, parent, false)
        return  ViewHolder(view)    }

    override fun getItemCount(): Int {
        return trackList.size
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val currentItem = trackList[position]

        holder.trackName.text = currentItem.trackName

        holder.deleteTrack.setOnClickListener {
            val currentItemTitle = currentItem.trackName
            Audio().deleteFileInMusicFolder(currentItemTitle)
        }

        holder.playtrack.setOnClickListener {
            val intent = Intent(holder.itemView.context, TrackSongs::class.java)
            intent.putExtra("playlistName", currentItem.trackName)
            holder.itemView.context.startActivity(intent)
            Toast.makeText(holder.itemView.context, "Track playing", Toast.LENGTH_SHORT).show()
        }
    }
}