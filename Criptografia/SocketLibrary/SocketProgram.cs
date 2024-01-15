using System.Net.Sockets;

namespace SocketLibrary
{
    public class SocketProgram
    {
        public void Init() {
            Socket socket = new Socket();

            SocketProgam.Listener();
        }
        static public void Listener() { }
        public void Sender() { }

        public void ManageRecived() {
        
        }


    }

}
