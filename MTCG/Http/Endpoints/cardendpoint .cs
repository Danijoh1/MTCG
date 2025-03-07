using Newtonsoft.Json;
using MTCG.Handlers;
using MTCG.Models;

namespace MTCG.Http.Endpoints
{
    public class cardendpoint
    {
        public cardendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    if (request.method == "GET")
                    {
                        if (request.identity != null)
                        {
                            user user = handler.UserHandler.GetByUsername(request.identity);
                            handler.CardHandler.GetStackOfUser(user);
                            string json = JsonConvert.SerializeObject(user.stack);
                            response.sendResponse(200, "OK", json);
                        }
                        else
                        {
                            response.sendResponse(401, "Unauthorized", "");
                        }
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