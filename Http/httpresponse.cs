using System.Net.Sockets;

namespace MTCG.Http
{
    public class httpresponse
    {
        public httpresponse(StreamWriter writer) {
            localwriter = writer;
        }
         public StreamWriter localwriter {  get; set; }
        public void sendResponse(int status, string statusMessage, string message)
        {
            // ----- 3. Write the HTTP-Response -----
            StreamTracer writerAlsoToConsole = new StreamTracer(localwriter);  // we use a simple helper-class StreamTracer to write the HTTP-Response to the client and to the console

            string firstline = "HTTP/1.0 " + status + " " + statusMessage;
            writerAlsoToConsole.WriteLine(firstline);    // first line in HTTP-Response contains the HTTP-Version and the status code
            writerAlsoToConsole.WriteLine("Content-Type: application/json");     // the HTTP-headers (in HTTP after the first line, until the empy line)
            writerAlsoToConsole.WriteLine();
            writerAlsoToConsole.WriteLine(message);//"<html><body><h1>Hello World!</h1></body></html>");    // the HTTP-content (here we just return a minimalistic HTML Hello-World)
            Console.WriteLine("========================================");
        }    
    }
}
