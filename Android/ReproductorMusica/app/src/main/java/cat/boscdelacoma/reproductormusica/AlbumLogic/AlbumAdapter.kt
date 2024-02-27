package cat.boscdelacoma.reproductormusica.AlbumLogic

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.R
import com.bumptech.glide.Glide

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
        holder.bindImagePortada()
        holder.bindImageContraPortada()
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
    private val albumPortada = itemView.findViewById(R.id.imageViewPortada) as ImageView
    private val albumContraPortada = itemView.findViewById(R.id.imageViewContraPortada) as ImageView


    private lateinit var album : Album

    fun bind(album: Album) {
        this.album = album
        albumName.text = album.titol
        albumYear.text = album.any.toString()

    }

    fun bindImagePortada(){
        // Cargar la imagen utilizando Picasso
        Glide.with(itemView)
            .load("http://172.23.3.204:5264/FitxersAPI/v1/Album/GetPortada/${album.titol}/${album.any.toString()}")
            .placeholder(R.drawable.btn_white) // Placeholder opcional
            .error(R.drawable.btn_exit) // Imagen de error opcional
            .into(albumPortada)
    }

    fun bindImageContraPortada() {
        // Cargar la imagen utilizando Picasso
        Glide.with(itemView)
            .load("http://172.23.3.204:5264/FitxersAPI/v1/Album/GetContraPortada/${album.titol}/${album.any}")
            .placeholder(R.drawable.btn_white) // Placeholder opcional
            .error(R.drawable.btn_exit) // Imagen de error opcional
            .into(albumContraPortada)

    }
}
