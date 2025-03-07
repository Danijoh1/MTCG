using Newtonsoft.Json;
using MTCG.Handlers;
using MTCG.Models;
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
        public statusendpoint(httprequest request, httpresponse response, DataHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    if (request.method == "GET")
                    {
                        if (request.path == "/stats")
                        {
                            user user = handler.UserHandler.GetByUsername(request.identity);
                            response.sendResponse(200, "OK", "");
                            Console.WriteLine("ELO: " + user.ELO);
                            Console.WriteLine("Battles fought: " + user.battlesFought);
                        }
                        else if (request.path == "/scoreboard")
                        {
                            List<user> list = handler.UserHandler.GetScore();
                            response.sendResponse(200, "OK", "");
                            list.ForEach(Console.WriteLine);

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
