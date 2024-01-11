package cat.boscdelacoma.reproductormusica

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ListView
import android.widget.TextView

class Llist : AppCompatActivity() {

    //public val lv = ListView()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_llist)

        val returnBtn : TextView = findViewById(R.id.returnBtn)

        returnBtn.setOnClickListener(){
            val intent = Intent(this ,MainActivity::class.java)
            startActivity(intent)
        }
    }
}