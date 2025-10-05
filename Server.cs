using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_C_Sharp
{  
    class Server
    {
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static IPAddress ip = IPAddress.Parse(MySQL.fileconfig.Read("server.host", "Server"));
        private static int porta = Int32.Parse(MySQL.fileconfig.Read("server.port", "Server"));
        private static IPEndPoint endpoint = new IPEndPoint(ip, porta);
 



        public static void Avvia()
        {
            socket.Bind(endpoint);
            socket.Listen(5);
            socket.BeginAccept(new AsyncCallback(iniziaAccettazione), null);

        }

        private static void iniziaAccettazione(IAsyncResult AR)
        {
            socket.BeginAccept(new AsyncCallback(iniziaAccettazione), null);
            
        }

        private static void iniziaRecezione(IAsyncResult AR)
        {

        }
    }
}