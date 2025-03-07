using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Http.Endpoints
{
    public class statusendpoint
    {
        public statusendpoint(httprequest request, httpresponse response)
        { 
            if (request.content != null)
            {
                try
                {
                    if (request.method == "GET")
                    {
                        if (request.path == "/stats")
                        {
                             
                        }
                        else if (request.path == "/scoreboard")
                        {

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
