package cat.boscdelacoma.reproductormusica.AlbumLogic

import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.R
import androidx.recyclerview.widget.LinearLayoutManager
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.Response
import retrofit2.http.GET



data class ImageInfo(
    val timestamp: Long,
    val machine: Long,
    val pid: Int,
    val increment: Long,
    val creationTime: String
)

interface AlbumService {
    @GET("Album")
    suspend fun getAlbums(): Response<List<Album>>
}

class AlbumActivity : AppCompatActivity() {

    private lateinit var recyclerView: RecyclerView
    private lateinit var adapter: AlbumAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_album)

        recyclerView = findViewById(R.id.recyclerViewAlbums)
        recyclerView.layoutManager = LinearLayoutManager(this)
        adapter = AlbumAdapter()
        recyclerView.adapter = adapter

        fetchData()
    }

    private fun fetchData() {
        val retrofit = Retrofit.Builder()
            .baseUrl("http://192.168.0.16:5264/FitxersAPI/v1/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val service = retrofit.create(AlbumService::class.java)

        GlobalScope.launch(Dispatchers.Main) {
            val response = service.getAlbums()
            if (response.isSuccessful) {
                Log.d("AlbumActivity", "Response: ${response.body()}")
                val albums = response.body()
                Log.d("AlbumActivity", "Albums: $albums")
                albums?.let {
                    adapter.setAlbums(it)
                }
            } else {
                // Manejar el error de la petición
                Log.e("AlbumActivity", "Error: ${response.errorBody()}")
            }
        }
    }
}
