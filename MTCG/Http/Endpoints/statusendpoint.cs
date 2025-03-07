using Newtonsoft.Json;
using MTCG.Handlers;
using MTCG.Models;
using MTCG.Repositories;
using MTCG.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Http.Endpoints
{
    public class statusendpoint
    {
        public statusendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    if (request.method == "GET")
                    {
                        if (request.path == "/stats")
                        {
                            user user = handler.UserHandler.GetStatsByUsername(request.identity);
                            string json = JsonConvert.SerializeObject(user);
                            response.sendResponse(200, "OK", json);
                        }
                        else if (request.path == "/scoreboard")
                        {
                            List<user> list = handler.UserHandler.GetScore();
                            string json = JsonConvert.SerializeObject(list);
                            response.sendResponse(200, "OK", json);
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
