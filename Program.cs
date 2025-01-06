using MTCG.Http;
using MTCG.Http.Endpoints;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;

Console.WriteLine("Our first simple HTTP-Server: http://localhost:10001/");
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
        userendpoint userendpoint = new userendpoint(request, response);
    }
    else if(request.path == "/packages" || request.path == "/transactions/packages")
    {
        packageendpoint packageendpoint = new packageendpoint(request, response);
    }
    else if(request.path == "/stats"  || request.path == "/scoreboard")
    {
        statusendpoint statusendpoint = new statusendpoint(request, response);
    }
    else if(request.path == "/battles")
    {
        Battleendpoint battleendpoint = new Battleendpoint(request, response);
    }
    else if(request.path == "/tradings")
    {
        tradingendpoint tradingendpoint = new tradingendpoint(request, response);
    }
    else {
        response.sendResponse(404, "Not Found", "");
    }
}