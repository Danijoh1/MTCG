﻿using MTCG.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Http.Endpoints
{
    public class tradingendpoint
    {
        public tradingendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
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
