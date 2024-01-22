package cat.boscdelacoma.reproductormusica.Adapters

import android.Manifest
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.core.app.ActivityCompat
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Audio
import cat.boscdelacoma.reproductormusica.R
import org.w3c.dom.Text
import java.nio.file.Path
import java.nio.file.Paths

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
    /**
     * Metode que fa que es mostri la informació de la cançó a la llista.
     * @param holder ViewHolder de la llista.
     * @param position Posició de la llista.
     * */
    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val currentItem = trackSongListAdapter[position]

        holder.trackName.text = currentItem.trackName

        holder.addtoPlayList.setOnClickListener {
            // TODO Aqui tiene que llegar el nombre de la cancion

            val FolderName = Audio().getFolderPath(currentItem.trackName)
            val songName = Audio().getMp3Path("cancion_descargada.mp3")

            val folderPath: Path = Paths.get(FolderName)
            val songPath: Path = Paths.get(songName)
            if (Audio().putSongIntoPlaylist(songPath,folderPath)){
                holder.addtoPlayList.setBackgroundResource(R.drawable.confirm_song_to_track)
            }else{
                holder.addtoPlayList.setBackgroundResource(R.drawable.addsongplaylist_track_songs_list_item)
            }
        }
    }
}