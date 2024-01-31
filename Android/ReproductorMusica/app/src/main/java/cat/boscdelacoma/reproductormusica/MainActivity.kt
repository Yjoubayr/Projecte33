package cat.boscdelacoma.reproductormusica

import android.animation.ObjectAnimator
import android.content.Intent
import android.media.MediaPlayer
import android.os.Bundle
import android.os.Handler
import android.view.animation.AlphaAnimation
import android.view.animation.Animation
import android.view.animation.LinearInterpolator
import android.view.animation.RotateAnimation
import android.widget.ImageView
import android.widget.SeekBar
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.FragmentTransaction
import cat.boscdelacoma.reproductormusica.Apilogic.Canco
import org.w3c.dom.Text
import java.io.File
import java.io.FileInputStream
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.util.Date

class MainActivity : AppCompatActivity() {
    private lateinit var botoPlayPause: TextView
    private lateinit var passSing : TextView
    private lateinit var prevSong : TextView
    private lateinit var downloadSong : TextView
    private lateinit var coverImage : ImageView
    private var mediaPlayer: MediaPlayer = MediaPlayer()
    private lateinit var seekBarAudio: SeekBar
    private var isPlaying = false
    private val handler = Handler()
    private var rotationAnimator: ObjectAnimator? = null


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val returnBtn : TextView = findViewById(R.id.back)
        val title : TextView = findViewById(R.id.songname)

        returnBtn.setOnClickListener(){
            finish()
        }

        initMainActivity()
        changeSongName()

        showFragments()
    }
    private fun rotationAnim(){
        coverImage = findViewById(R.id.CoverImage)
        val rotation = RotateAnimation(
            0f, 360f,
            Animation.RELATIVE_TO_SELF, 0.5f,
            Animation.RELATIVE_TO_SELF, 0.5f
        )

        rotation.duration = 3000
        rotation.repeatCount = Animation.INFINITE
        rotation.interpolator = LinearInterpolator()
        coverImage.startAnimation(rotation)
    }
    /**
     * Metode que ens ajuda a tornar al fragment anterior.
     * @return {Unit} No retorna res.
     * */
    fun tornarDesDeFragment() {
        supportFragmentManager.popBackStack()
    }
    /**
     * Ens permet posar el nom de la canço que estem reproduïnt
     * */
    fun changeSongName(){
        val songName = intent.getStringExtra("songName").toString()
        val title : TextView = findViewById(R.id.songname)

        if (songName == "null"){
            title.text = ""

        }else{
            title.text = songName

        }
    }
    /**
     * Metode que ens ajuda a inicialitzar la MainActivity.
     * @return {Unit} No retorna res.
     * */
    private fun initMainActivity() {

        botoPlayPause = findViewById(R.id.startSong)
        seekBarAudio = findViewById(R.id.progressBar)
        prevSong = findViewById(R.id.BackSong)
        passSing = findViewById(R.id.PassSong)
        downloadSong = findViewById(R.id.DownloadSong)

        val absolutepathsong = intent.getStringExtra("absolutepathsong").toString()

        if (absolutepathsong != "null") {
            mediaPlayer.setDataSource(absolutepathsong)
            mediaPlayer.prepare()
        }
        downloadSong.setOnClickListener{
            val intent = Intent(this, DownloadSongs::class.java)
            this.startActivity(intent)

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
                if (absolutepathsong != "null") {

                    botoPlayPause.setBackgroundResource(R.drawable.stopbtn)
                    mediaPlayer.start()
                    rotationAnim()
                    seekBarAudio.max = mediaPlayer.duration
                    updateSeekBar()
                    postHistorial()
                }else{
                    showlocalSongs()
                }

            }
        }
    }

    private fun showlocalSongs() {
        val intent = Intent(this, ShowLocalSongs::class.java)
        this.startActivity(intent)
    }

    /**
     * Metode que ens ajuda a mostrar els fragments.
     * @return {Unit} No retorna res.
     * */
    private fun showFragments(){
        val AddSongToTrack: TextView = findViewById(R.id.AddSongToTrack)
        val addplaylist: TextView = findViewById(R.id.AddList)

        AddSongToTrack.setOnClickListener {
            val listOfSongsFragment = ListOfSongsFragment()

            listOfSongsFragment.SongName = intent.getStringExtra("absolutepathsong").toString()
            val fragmentManager = supportFragmentManager


            val transaction: FragmentTransaction = fragmentManager.beginTransaction()
            val fadeIn: Animation = AlphaAnimation(0f, 1f)
            fadeIn.duration = 500 // Duración de la animación en milisegundos
            transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out)
            transaction.replace(R.id.fragment_container, listOfSongsFragment)
            transaction.addToBackStack(null)
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

    /**
     * Metode que ens ajuda a actualitzar la barra de la musca
     * @return {Unit} No retorna res.
     * */
    private fun updateSeekBar() {
        isPlaying = true
        handler.postDelayed(object : Runnable {
            override fun run() {
                if (isPlaying) {
                    seekBarAudio.progress = mediaPlayer.currentPosition
                    handler.postDelayed(this, 100)
                }
            }
        }, 100)
    }
    override fun onDestroy() {
        super.onDestroy()
        mediaPlayer.release()
    }


    fun postHistorial() {
        val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'")
        val formattedDate = LocalDateTime.now().format(formatter).toString()
        // TODO : Agafar la MAC del dispositiu
        var songname = intent.getStringExtra("songName").toString()

        HTTP_Mongo(this).postHistorialOfSongs("45",songname, formattedDate)

    }
}
