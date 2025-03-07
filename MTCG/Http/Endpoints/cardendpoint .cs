using Newtonsoft.Json;
using MTCG.Handlers;
using MTCG.Models;
using MTCG.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Http.Endpoints
{
    public class cardendpoint
    {
        public cardendpoint(httprequest request, httpresponse response)
        {
            UserRepository UserRepository = new UserRepository("Host=localhost;Username=user;Password=password;Database=mtgcdb");
            UserHandler UserHandler = new UserHandler(UserRepository);
            CardRepository CardRepository = new CardRepository("Host=localhost;Username=user;Password=password;Database=mtgcdb");
            CardHandler CardHandler = new CardHandler(CardRepository);
            if (request.content != null)
            {
                try
                {
                    if (request.method == "GET")
                    {
                        if (request.identity != null)
                        {
                            user user = UserHandler.GetByUsername(request.identity);
                            CardHandler.GetStackOfUser(user);
                            response.sendResponse(200, "OK", "");
                            user.stack.ForEach(Console.WriteLine);
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