package cat.boscdelacoma.reproductormusica

import android.app.Activity
import android.content.Intent
import android.media.MediaPlayer
import android.os.Bundle
import android.os.Handler
import android.view.animation.AlphaAnimation
import android.view.animation.Animation
import android.widget.ImageButton
import android.widget.ImageView
import android.widget.SeekBar
import android.widget.TextView
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.FragmentTransaction
import org.w3c.dom.Text

class MainActivity : AppCompatActivity() {
    private lateinit var botoPlayPause: TextView
    private var mediaPlayer: MediaPlayer = MediaPlayer()
    private lateinit var seekBarAudio: SeekBar
    private lateinit var handler: Handler

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val returnBtn : TextView = findViewById(R.id.back)

        returnBtn.setOnClickListener(){
            finish()
        }


        initMainActivity()
        showFragments()
    }

    fun tornarDesDeFragment() {
        supportFragmentManager.popBackStack()
    }

    private fun initMainActivity() {

        botoPlayPause = findViewById(R.id.startSong)
        seekBarAudio = findViewById(R.id.progressBar)
        val absolutepathsong = intent.getStringExtra("absolutepathsong").toString()

        mediaPlayer.setDataSource(absolutepathsong)
        mediaPlayer.prepare()


        botoPlayPause.setOnClickListener {
            if (mediaPlayer.isPlaying) {
                botoPlayPause.setBackgroundResource(R.drawable.playbtn)
                mediaPlayer.pause()
            } else {
                botoPlayPause.setBackgroundResource(R.drawable.stopbtn)
                mediaPlayer.start()
            }
        }

    }

    private fun showFragments(){
        val AddSongToTrack: TextView = findViewById(R.id.AddSongToTrack)
        val addplaylist: TextView = findViewById(R.id.AddList)


        AddSongToTrack.setOnClickListener {
            // Crear una instancia del fragmento
            val listOfSongsFragment = ListOfSongsFragment()

            // Obtener el FragmentManager
            val fragmentManager = supportFragmentManager

            // Iniciar una transacción de fragmento
            val transaction: FragmentTransaction = fragmentManager.beginTransaction()

            // Configurar la animación de fadeIn
            val fadeIn: Animation = AlphaAnimation(0f, 1f)
            fadeIn.duration = 500 // Duración de la animación en milisegundos

            // Asignar la animación al fragmento
            transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out)

            // Reemplazar el contenido actual con el fragmento ListOfSongsFragment
            transaction.replace(R.id.fragment_container, listOfSongsFragment)

            // Agregar la transacción al back stack
            transaction.addToBackStack(null)

            // Confirmar la transacción
            transaction.commit()
        }


        addplaylist.setOnClickListener(){
            val trackName = TrackName()

            val fragmentManager = supportFragmentManager

            val transaction: FragmentTransaction = fragmentManager.beginTransaction()

            val fadeIn: Animation = AlphaAnimation(0f, 1f)
            fadeIn.duration = 500

            transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out)

            transaction.replace(R.id.fragment_container, trackName)

            transaction.addToBackStack(null)

            transaction.commit()
        }
    }
}
