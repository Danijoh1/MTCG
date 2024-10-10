using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MTCG.Http.Server;

namespace MTCG.Http.Server
{
    public class httpserver
    {
        //Console.WriteLine("Our first simple HTTP-Server: http://localhost:10001/");
        // ===== I. Start the HTTP-Server =====
        var httpServer = new TcpListener(IPAddress.Loopback, 10001);

    }
    httpServer.Start();
}
