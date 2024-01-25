package cat.boscdelacoma.reproductormusica.Adapters

import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Audio
import cat.boscdelacoma.reproductormusica.HTTP_Mongo
import cat.boscdelacoma.reproductormusica.MainActivity
import cat.boscdelacoma.reproductormusica.R


class SongAdapter(private val songList: List<SongItem>) : RecyclerView.Adapter<SongAdapter.ViewHolder>() {


    data class SongItem(val songName: String)

        override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
            val view = LayoutInflater.from(parent.context).inflate(R.layout.activity_song_item, parent, false)
            return ViewHolder(view)
        }

        override fun onBindViewHolder(holder: ViewHolder, position: Int) {
            val currentItem = songList[position]
            val audio = Audio()
            holder.textBox.text = currentItem.songName

            holder.downloadLogo.setOnClickListener {
                val intent = Intent(holder.itemView.context, MainActivity::class.java)
                val path = audio.getMp3Path("test.mp3")

                intent.putExtra("absolutepathsong", path)
                //audio.downloadSongAPI(context = holder.itemView.context, "https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3")

                HTTP_Mongo(context = holder.itemView.context).DownloadSongFromMongoDb("e138ac81-ba13-4e6e-8d44-e39ad83d4600")
                holder.itemView.context.startActivity(intent)
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