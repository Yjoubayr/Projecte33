package cat.boscdelacoma.reproductormusica

import android.content.Intent
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.animation.AlphaAnimation
import android.view.animation.Animation
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.FragmentTransaction
import com.google.android.material.textfield.TextInputEditText
import java.io.IOException
import java.io.InputStream

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [TrackName.newInstance] factory method to
 * create an instance of this fragment.
 */
class TrackName : Fragment() {
    // TODO: Rename and change types of parameters
    private var param1: String? = null
    private var param2: String? = null
    private val TAG: String = "ReproductorMusica"
    private lateinit var inputStream: InputStream

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            param1 = it.getString(ARG_PARAM1)
            param2 = it.getString(ARG_PARAM2)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_track_name, container, false)

        val confirmbtn: TextView = view.findViewById(R.id.Confirm)
        val exitbtn: TextView = view.findViewById(R.id.Exit)
        val playlistname = view.findViewById<TextInputEditText>(R.id.PlayListName)

        confirmbtn.setOnClickListener {
            val playlistNameText = playlistname.text.toString().trim()

            if (playlistNameText.isNotEmpty()) {
                var audio = Audio()
                if(audio.createFolder(playlistNameText)) {
                    val intent = Intent(context,Llist::class.java)
                    startActivity(intent)
                } else {
                    Toast.makeText(context, "La carpeta no s'ha pogut crear", Toast.LENGTH_SHORT).show()
                }
            } else {
                Toast.makeText(requireContext(), "Ingresa un nombre de lista válido", Toast.LENGTH_SHORT).show()
            }
        }

        exitbtn.setOnClickListener {
            (activity as? MainActivity)?.tornarDesDeFragment()
        }

        return view
    }


    companion object {
        /**
         * Use this factory method to create a new instance of
         * this fragment using the provided parameters.
         *
         * @param param1 Parameter 1.
         * @param param2 Parameter 2.
         * @return A new instance of fragment TrackName.
         */
        // TODO: Rename and change types and number of parameters
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            TrackName().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
}