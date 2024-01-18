package cat.boscdelacoma.reproductormusica

import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView


class SongAdapter(private val songList: List<SongItem>) : RecyclerView.Adapter<SongAdapter.ViewHolder>() {

        data class SongItem(val songName: String)

        override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
            val view = LayoutInflater.from(parent.context).inflate(R.layout.activity_song_item, parent, false)
            return ViewHolder(view)
        }

        override fun onBindViewHolder(holder: ViewHolder, position: Int) {
            val currentItem = songList[position]
            val audio = Audio()
            // Set data to views in the ViewHolder
            holder.textBox.text = currentItem.songName
            // Set other data as needed

            holder.downloadLogo.setOnClickListener {
                // TODO: Download song
                // Aqui va la logica per poder descarregar les cançons
                Toast.makeText(holder.itemView.context, "Downloading song...", Toast.LENGTH_SHORT).show()
                val intent = Intent(holder.itemView.context, MainActivity::class.java)
                holder.itemView.context.startActivity(intent)
                    audio.downloadSongAPI(context = holder.itemView.context, "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3")
            }
        }

        override fun getItemCount(): Int {
            return songList.size
        }

        class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
            val squareImage: ImageView = itemView.findViewById(R.id.squareImage)
            val textBox: TextView = itemView.findViewById(R.id.textBox)
            val downloadLogo: TextView = itemView.findViewById(R.id.downloadLogo)
        }
}