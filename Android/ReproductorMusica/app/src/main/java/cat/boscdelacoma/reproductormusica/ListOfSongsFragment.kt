package cat.boscdelacoma.reproductormusica

import android.annotation.SuppressLint
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.animation.AlphaAnimation
import android.view.animation.Animation
import android.widget.TextView
import androidx.fragment.app.FragmentTransaction
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import cat.boscdelacoma.reproductormusica.Adapters.track_song_list_adapter

// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [ListOfSongsFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class ListOfSongsFragment : Fragment() {
    // TODO: Rename and change types of parameters
    private var param1: String? = null
    private var param2: String? = null
    private var audio: Audio = Audio()
    var SongName : String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        arguments?.let {
            param1 = it.getString(ARG_PARAM1)
            param2 = it.getString(ARG_PARAM2)
        }

    }

    @SuppressLint("UseRequireInsteadOfGet")
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_list_of_songs, container, false)

        val createNewPlaylist: TextView = view.findViewById(R.id.CreateNewPlayList)
        createNewPlaylist.setOnClickListener {
            // Crear una instancia del fragmento TrackName
            val trackNameFragment = TrackName()

            // Obtener el FragmentManager desde el Activity
            val fragmentManager = requireActivity().supportFragmentManager

            // Iniciar una transacción de fragmento
            val transaction: FragmentTransaction = fragmentManager.beginTransaction()

            // Configurar la animación de fadeIn
            val fadeIn: Animation = AlphaAnimation(0f, 1f)
            fadeIn.duration = 500 // Duración de la animación en milisegundos
            transaction.setCustomAnimations(R.anim.fade_in, R.anim.fade_out)

            // Reemplazar el contenido actual con el fragmento TrackName
            transaction.replace(R.id.fragment_container, trackNameFragment)

            // Agregar la transacción al back stack
            transaction.addToBackStack(null)

            // Confirmar la transacción
            transaction.commit()
        }

        val back : TextView = view.findViewById(R.id.Back)
        back.setOnClickListener(){
            val fragmentManager = requireActivity().supportFragmentManager
            fragmentManager.popBackStack()
        }


        val list = Audio().getAllFilesList()
        val trackList: MutableList<track_song_list_adapter.trackSongListItem> = mutableListOf()
        val recyclerView: RecyclerView = view.findViewById(R.id.recyclerView)

        for (i in 1..list.size) {
            val trackName = list[i-1].toString()
            val trackItem = track_song_list_adapter.trackSongListItem(trackName = trackName, SongName = SongName.toString())
            trackList.add(trackItem)
        }

        val adapter = track_song_list_adapter(trackList)
        recyclerView.layoutManager = LinearLayoutManager(requireContext())
        recyclerView.adapter = adapter

        return view
    }

    companion object {
        @JvmStatic
        fun newInstance(param1: String, param2: String) =
            ListOfSongsFragment().apply {
                arguments = Bundle().apply {
                    putString(ARG_PARAM1, param1)
                    putString(ARG_PARAM2, param2)
                }
            }
    }
}