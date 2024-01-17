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
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.FragmentTransaction

class MainActivity : AppCompatActivity() {
    private var progressLevel = 0
    private var cancoEscollida: String = "false"
    private lateinit var audio: Audio

    private lateinit var botoStop: ImageButton
    private lateinit var botoPlayPause: ImageButton
    private lateinit var seekBarAudio: SeekBar
    private lateinit var song: Audio
    private var mediaPlayer: MediaPlayer = MediaPlayer()
    private var audioIniciat = false
    private lateinit var handler: Handler


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val AddSongToTrack: TextView = findViewById(R.id.AddSongToTrack)


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

        val addplaylist: TextView = findViewById(R.id.AddList)

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

    public fun tornarDesDeFragment() {
        supportFragmentManager.popBackStack()
    }

    private fun initMainActivity() {

        botoPlayPause = findViewById(R.id.startSong)

        seekBarAudio = findViewById(R.id.progressBar)

        registerForActivityResult(ActivityResultContracts.StartActivityForResult()) { result ->

            if (result.resultCode == Activity.RESULT_OK) {
                val data: Intent? = result.data
                data?.data?.let { uri ->
                    audio.uri = uri
                    audio.titol = "back_in_black.mp3"
                    //saveSong(this, audio.uri, audio.titol!!)

                    if (audio == obtenirDades(this, audio.uri)) {
                        startActivity(intent)
                    }
                }
            }
        }

        botoPlayPause.setOnClickListener {
            if (audioIniciat == false) {
                startAudio()
            } else {
                pauseAudio()
            }
        }

        song = Audio()

    }

    private fun initializeSeekBar() {
        seekBarAudio?.setOnSeekBarChangeListener(object :
            SeekBar.OnSeekBarChangeListener {

            override fun onProgressChanged(seek: SeekBar,
                                           progress: Int, fromUser: Boolean) {
                if(fromUser) {
                    mediaPlayer?.seekTo(progress)
                }
            }

            override fun onStartTrackingTouch(seek: SeekBar) {
                if (progressLevel == seek.max) {
                    stopAudio()
                    seekBarAudio.setProgress(progressLevel)
                }
            }

            override fun onStopTrackingTouch(seek: SeekBar) {
                if (progressLevel == seek.max) {
                    stopAudio()
                    seekBarAudio.setProgress(progressLevel)
                } else {
                    progressLevel = seek.progress
                    mediaPlayer.seekTo(progressLevel * 1000)
                    seekBarAudio.setProgress(progressLevel)
                }
            }
        })
    }

    private fun initializeTimer() {
        handler = Handler()
        handler.postDelayed(object : Runnable{
            override fun run() {
                try{
                    seekBarAudio.progress = mediaPlayer!!.currentPosition / 1000
                    handler.postDelayed(this, 1000)
                }catch (e: Exception){
                    seekBarAudio.progress = 0
                }
            }
        },0)
    }

    private fun initializeMediaPlayer(file: String) {

        val fd = assets.openFd(file)

        mediaPlayer.setDataSource(
            fd.fileDescriptor,
            fd.startOffset,
            fd.length
        )

        fd.close()

        mediaPlayer.prepare()
        seekBarAudio.max = (mediaPlayer.duration / 1000).toInt()
    }

    private fun initializeSong(songName: String) {

        val fd = assets.openFd(songName)

        mediaPlayer.setDataSource(
            fd.fileDescriptor,
            fd.startOffset,
            fd.length
        )

        fd.close()

        mediaPlayer.prepare()
        seekBarAudio.max = (mediaPlayer.duration / 1000).toInt()
    }

    private fun startAudio() {

        mediaPlayer = MediaPlayer()

        initializeMediaPlayer("back_in_black.mp3")

        mediaPlayer.seekTo(progressLevel)
        initializeTimer()
        initializeSeekBar()

        mediaPlayer.start()

        botoPlayPause.setBackgroundResource(R.drawable.pause_circle_outline)
        botoPlayPause.scaleType = ImageView.ScaleType.CENTER_INSIDE
        audioIniciat = true

    }

    private fun pauseAudio() {
        progressLevel = mediaPlayer.getCurrentPosition()
        mediaPlayer.pause()
        botoPlayPause.setBackgroundResource(R.drawable.pause_circle_outline)
        botoPlayPause.scaleType = ImageView.ScaleType.CENTER_INSIDE
        audioIniciat = false
    }

    private fun stopAudio() {
        progressLevel = 0
        mediaPlayer.stop()
        seekBarAudio.setProgress(progressLevel)
        botoPlayPause.setBackgroundResource(R.drawable.playbtn)
        botoPlayPause.scaleType = ImageView.ScaleType.CENTER_INSIDE
        audioIniciat = false
    }
}
