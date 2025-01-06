using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Http.Endpoints
{
    public class packageendpoint
    {
        public packageendpoint(httprequest request, httpresponse response, Dictionary<string, byte[]> database)
        {
            if (request.content != null)
            {
                try
                {
                    if (request.method == "POST")
                    {
                        if (request.path == "/packages")
                        {
                            
                        }
                        else if (request.path == "/transactions/packages")
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