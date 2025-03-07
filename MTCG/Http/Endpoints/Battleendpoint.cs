using Newtonsoft.Json;
using MTCG.Handlers;

namespace MTCG.Http.Endpoints
{
    public class Battleendpoint
    {
        public Battleendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    if (request.method == "POST")
                    {
                        
                    }
                    else
                    {
                        response.sendResponse(405, "Method Not Allowed", "");
                    }
                }
                catch (JsonReaderException)
                {
                    response.sendResponse(400, "Bad Request", "");
                }
            }
        }
    }
}
