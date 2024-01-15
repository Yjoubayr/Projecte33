package cat.boscdelacoma.reproductormusica

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.ViewParent
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import org.w3c.dom.Text

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

        // Set data to views in the ViewHolder
        holder.trackName.text = currentItem.trackName
        holder.deleteTrack.setOnClickListener {
            //TODO : Backend, eliminar la cançó de la llista
            Toast.makeText(holder.itemView.context, "Track deleted", Toast.LENGTH_SHORT).show()
        }
        holder.playtrack.setOnClickListener {
            //TODO: Backend, agafar les cançons de la llista i reproduir-les
            Toast.makeText(holder.itemView.context, "Track playing", Toast.LENGTH_SHORT).show()
        }
    }
}