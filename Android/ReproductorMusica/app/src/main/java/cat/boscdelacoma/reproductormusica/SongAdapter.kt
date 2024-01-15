package cat.boscdelacoma.reproductormusica

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView


class SongAdapter(private val songList: List<SongItem>) : RecyclerView.Adapter<SongAdapter.ViewHolder>() {

        data class SongItem(val songName: String) // Afegir img i download logo
        override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
            val view = LayoutInflater.from(parent.context).inflate(R.layout.song_item, parent, false)
            return ViewHolder(view)
        }

        override fun onBindViewHolder(holder: ViewHolder, position: Int) {
            val currentItem = songList[position]

            // Set data to views in the ViewHolder
            holder.textBox.text = currentItem.songName
            // Set other data as needed

            // Add click listeners or other actions as needed
        }

        override fun getItemCount(): Int {
            return songList.size
        }

        class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
            val squareImage: ImageView = itemView.findViewById(R.id.squareImage)
            val textBox: TextView = itemView.findViewById(R.id.textBox)
            val downloadLogo: ImageView = itemView.findViewById(R.id.downloadLogo)
            // Add other views as needed
        }
}