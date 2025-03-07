using MTCG.Handlers;
using MTCG.Http;
using MTCG.Http.Endpoints;
using MTCG.Repositories;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Threading;

Console.WriteLine("Our first simple HTTP-Server: http://localhost:10001/");
var httpserver = new httpserver(IPAddress.Loopback, 10001);
httpserver.Start();
while (true)
{
        ThreadPool.QueueUserWorkItem(delegate { 
        TcpClient clientSocket = httpserver.AcceptClient();
        using StreamWriter writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
        using StreamReader reader = new StreamReader(clientSocket.GetStream());
        DatabaseHandlers handler = new DatabaseHandlers();
        httprequest request = new httprequest(reader);
        httpresponse response = new httpresponse(writer);
        if (request.path == "/users" || request.path == "/sessions" || request.path.Contains("/users"))
        {
            userendpoint userendpoint = new userendpoint(request, response, handler);
        }
        else if (request.path == "/packages" || request.path == "/transactions/packages")
        {
            packageendpoint packageendpoint = new packageendpoint(request, response, handler);
        }
        else if (request.path == "/cards")
        {
            cardendpoint cardendpoint = new cardendpoint(request, response, handler);
        }
        else if (request.path == "/deck")
        {
            deckendpoint deckendpoint = new deckendpoint(request, response, handler);
        }
        else if (request.path == "/stats" || request.path == "/scoreboard")
        {
            statusendpoint statusendpoint = new statusendpoint(request, response, handler);
        }
        /*
        else if (request.path == "/battles")
        {
            Battleendpoint battleendpoint = new Battleendpoint(request, response, handler);
        }
        else if (request.path == "/tradings")
        {
            tradingendpoint tradingendpoint = new tradingendpoint(request, response, handler);
        }*/
        else
        {
            response.sendResponse(404, "Not Found", "");
        }
            Thread.Sleep(1000);
        });
}
