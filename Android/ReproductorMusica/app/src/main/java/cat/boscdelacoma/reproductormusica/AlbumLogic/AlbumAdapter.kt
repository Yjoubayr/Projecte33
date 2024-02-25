package cat.boscdelacoma.reproductormusica.AlbumLogic

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.R

data class Album(
    val _ID: String,
    val any: Int,
    val titol: String,
    val genere: String,
    val uidSong: String,
    val imatgePortadaId: ImageInfo,
    val imatgeContraPortadaId: ImageInfo
)
class AlbumAdapter : RecyclerView.Adapter<AlbumViewHolder>() {

    private var albums: List<Album> = listOf()

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlbumViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.album_item, parent, false)
        return AlbumViewHolder(view)
    }

    override fun onBindViewHolder(holder: AlbumViewHolder, position: Int) {
        val album = albums[position]
        holder.bind(album)
    }

    override fun getItemCount(): Int = albums.size

    fun setAlbums(albums: List<Album>) {
        this.albums = albums
        notifyDataSetChanged()
    }
}

class AlbumViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    private val albumName = itemView.findViewById(R.id.textViewAlbumName) as TextView
    private val albumYear = itemView.findViewById(R.id.textViewAlbumYear) as TextView


    fun bind(album: Album) {
        albumName.text = album.titol
        albumYear.text = album.any.toString()

    }
}
