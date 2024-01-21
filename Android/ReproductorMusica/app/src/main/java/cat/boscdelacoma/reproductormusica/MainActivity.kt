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
    private var isPlaying = false

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

    /**
     * Metode que ens ajuda a tornar al fragment anterior.
     * @return {Unit} No retorna res.
     * */
    fun tornarDesDeFragment() {
        supportFragmentManager.popBackStack()
    }

    /**
     * Metode que ens ajuda a inicialitzar la MainActivity.
     * @return {Unit} No retorna res.
     * */
    private fun initMainActivity() {

        botoPlayPause = findViewById(R.id.startSong)
        seekBarAudio = findViewById(R.id.progressBar)

        // TODO: Pendent de revisar

        val absolutepathsong = intent.getStringExtra("absolutepathsong").toString()


        if (!(absolutepathsong.isNullOrEmpty())) {
            mediaPlayer.setDataSource(absolutepathsong)
            mediaPlayer.prepare()
        }
        seekBarAudio.setOnSeekBarChangeListener(object : SeekBar.OnSeekBarChangeListener {
            override fun onProgressChanged(seekBar: SeekBar?, progress: Int, fromUser: Boolean) {
            }

            override fun onStartTrackingTouch(seekBar: SeekBar?) {
            }

            override fun onStopTrackingTouch(seekBar: SeekBar?) {
                seekBar?.let {
                    mediaPlayer.seekTo(it.progress)
                }
            }
        })

        botoPlayPause.setOnClickListener {
            if (mediaPlayer.isPlaying) {
                botoPlayPause.setBackgroundResource(R.drawable.playbtn)
                mediaPlayer.pause()
            } else {
                botoPlayPause.setBackgroundResource(R.drawable.stopbtn)
                mediaPlayer.start()
                seekBarAudio.max = mediaPlayer.duration
                updateSeekBar()

            }
        }



    }

    /**
     * Metode que ens ajuda a mostrar els fragments.
     * @return {Unit} No retorna res.
     * */
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

    private fun updateSeekBar() {
        Thread {
            while (isPlaying) {
                runOnUiThread {
                    seekBarAudio.progress = mediaPlayer.currentPosition
                }
                try {
                    Thread.sleep(100)
                } catch (e: InterruptedException) {
                    e.printStackTrace()
                }
            }
        }.start()
    }

    override fun onDestroy() {
        super.onDestroy()
        mediaPlayer.release()
    }

}
