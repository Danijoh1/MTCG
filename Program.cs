using MTCG.Http;
using MTCG.Http.Endpoints;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;

Console.WriteLine("Our first simple HTTP-Server: http://localhost:10001/");
Dictionary<string, byte[]> database = new Dictionary<string, byte[]>();
var httpserver = new httpserver(IPAddress.Loopback, 10001);
httpserver.Start();

while (true)
{
    TcpClient clientSocket = httpserver.AcceptClient();
    using StreamWriter writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
    using StreamReader reader = new StreamReader(clientSocket.GetStream());
    httprequest request = new httprequest(reader);
    httpresponse response = new httpresponse(writer);
    if(request.path == "/users" || request.path == "/sessions")
    {
        userendpoint userendpoint = new userendpoint(request, response, database);
    }
    else {
        response.sendResponse(404, "Not Found", "");
    }
}