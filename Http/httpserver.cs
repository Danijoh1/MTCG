using System.Net.Sockets;
using System.Net;

namespace MTCG.Http
{
    public class httpserver
    {
        public httpserver(IPAddress address, int port)
        {
            httpServer = new TcpListener(address, port);
        }
        public TcpListener httpServer { get; private set;}
        public void Start()
        {
            httpServer.Start();
        }
        public TcpClient AcceptClient()
        {
            return httpServer.AcceptTcpClient();
        }
    }

}
