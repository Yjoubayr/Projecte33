package cat.boscdelacoma.reproductormusica

import android.os.Bundle
import android.view.animation.AlphaAnimation
import android.view.animation.Animation
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.FragmentTransaction

class MainActivity : AppCompatActivity() {
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
}
