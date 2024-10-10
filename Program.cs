using MTCG.Http;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Our first simple HTTP-Server: http://localhost:10001/");
Dictionary<string, byte[]> database = new Dictionary<string, byte[]>();
var httpserver = new httpserver(IPAddress.Loopback, 10001);
httpserver.Start();

while (true)
{
    TcpClient clientSocket = httpserver.AcceptClient();
    httprequest request = new httprequest(clientSocket);
    httpresponse response = new httpresponse(clientSocket);
    if(request.path == "/users" || request.path == "/sessions")
    {
        userendpoint userendpoint = new userendpoint(request, response, database);
    }
    else {
        response.sendResponse(404, "Not Found", "");
    }
}