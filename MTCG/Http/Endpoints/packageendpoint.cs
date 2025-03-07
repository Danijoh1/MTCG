using MTCG.Handlers;
using MTCG.Models;
using MTCG.Repositories;
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
        public packageendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    if (request.method == "POST")
                    {
                        if (request.path == "/packages")
                        {
                            if(request.identity == "admin")
                            {
                                List<card> cards = JsonConvert.DeserializeObject<List<card>>(request.content);
                                if (cards.Count == 5)
                                {
                                    packages package = handler.PackageHandler.AddPackage();
                                    handler.CardHandler.AddCard(cards[0], package);
                                    handler.CardHandler.AddCard(cards[1], package);
                                    handler.CardHandler.AddCard(cards[2], package);
                                    handler.CardHandler.AddCard(cards[3], package);
                                    handler.CardHandler.AddCard(cards[4], package);
                                    string createmessage = "Package created";
                                    response.sendResponse(201, createmessage, "");
                                }
                                else
                                {
                                    response.sendResponse(400, "Bad Request", "");
                                }
                            }
                            else
                            {
                                response.sendResponse(403, "Forbidden", "");
                            }
                        }
                        else if (request.path == "/transactions/packages")
                        {
                            user user = handler.UserHandler.GetByUsername(request.identity);
                            if(user.coins > 0)
                            {
                                user.coins -= 5;
                                handler.UserHandler.ChangeCoins(user);
                                packages package = handler.PackageHandler.GetUnsoldPackage();
                                if (package != null)
                                {
                                    handler.CardHandler.AddOwner(user, package);
                                    string createmessage = "Package sold";
                                    response.sendResponse(201, createmessage, "");
                                }
                                else
                                {
                                    response.sendResponse(400, "No packages available", "");
                                }
                            }
                            else
                            {
                                response.sendResponse(400, "Not enough money", "");
                            }
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