using System.Net.Sockets;

namespace MTCG.Http
{
    public class httpresponse
    {
        public httpresponse(TcpClient clientSocket) {
            writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
            // ----- 2. Do the processing -----
            // .... 
            if(clientSocket == null)
            {
                status = "408";
                statusMessage = "Request Timeout";
            }
            else
            {
                status = "200";
                statusMessage = "OK";
            }

            Console.WriteLine("----------------------------------------");

            // ----- 3. Write the HTTP-Response -----
            var writerAlsoToConsole = new StreamTracer(writer);  // we use a simple helper-class StreamTracer to write the HTTP-Response to the client and to the console

            writerAlsoToConsole.WriteLine("HTTP/1.0 "+status+" "+statusMessage);    // first line in HTTP-Response contains the HTTP-Version and the status code
            if(status == "200")
            {
                writerAlsoToConsole.WriteLine("Content-Type: txt/html; charset=utf-8");     // the HTTP-headers (in HTTP after the first line, until the empy line)
                writerAlsoToConsole.WriteLine();
                writerAlsoToConsole.WriteLine("<html><body><h1>Hello World!</h1></body></html>");    // the HTTP-content (here we just return a minimalistic HTML Hello-World)
            }
            Console.WriteLine("========================================");
        }
        public StreamWriter writer {  get; set; }
        public string status { get; set; }
        public string statusMessage { get; set; }
    }
}
