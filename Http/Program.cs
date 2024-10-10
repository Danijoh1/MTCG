using MTCG.Http;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Our first simple HTTP-Server: http://localhost:10001/");
var httpserver = new httpserver(IPAddress.Loopback, 10001);
httpserver.Start();

while (true)
{
    TcpClient clientSocket = httpserver.AcceptClient();
    httprequest request = new httprequest(clientSocket);
    httpresponse response = new httpresponse(clientSocket);
    if(request.path == "user")
    {
        new userendpoint(request, response);
    }
}