package cat.boscdelacoma.reproductormusica.Adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Audio
import cat.boscdelacoma.reproductormusica.R

class SongInTrackAdapter(private val songList: List<SongItem>): RecyclerView.Adapter<SongInTrackAdapter.ViewHolder>(){


    data class SongItem(val songName: String)
    class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val SongName: TextView = itemView.findViewById(R.id.songname)
        val deleteSong: TextView = itemView.findViewById(R.id.delete_song)
    }
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.song_in_track_item, parent, false)
        return ViewHolder(view)
    }
    override fun getItemCount(): Int {
        return songList.size
    }
    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val currentItem = songList[position]

        holder.SongName.text = currentItem.songName

        holder.deleteSong.setOnClickListener {
            val currentItemSong = currentItem.songName
            Audio().deleteMusicInTrack(currentItemSong)
        }


    }
}