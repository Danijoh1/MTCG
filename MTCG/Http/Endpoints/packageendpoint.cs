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
        public packageendpoint(httprequest request, httpresponse response)
        {
            PackageRepository PackageRepository = new PackageRepository("Host=localhost;Username=user;Password=password;Database=mtcgdb");
            PackageHandler PackageHandler = new PackageHandler(PackageRepository);
            CardRepository CardRepository = new CardRepository("Host=localhost;Username=user;Password=password;Database=mtcgdb");
            CardHandler CardHandler = new CardHandler(CardRepository);
            UserRepository UserRepository = new UserRepository("Host=localhost;Username=user;Password=password;Database=mtcgdb");
            UserHandler UserHandler = new UserHandler(UserRepository);
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
                                    packages package = PackageHandler.AddPackage();
                                    CardHandler.AddCard(cards[0], package);
                                    CardHandler.AddCard(cards[1], package);
                                    CardHandler.AddCard(cards[2], package);
                                    CardHandler.AddCard(cards[3], package);
                                    CardHandler.AddCard(cards[4], package);
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
                            user user = UserHandler.GetByUsername(request.identity);
                            if(user.coins > 0)
                            {
                                user.coins -= 5;
                                UserHandler.ChangeCoins(user);
                                packages package = PackageHandler.GetUnsoldPackage();
                                if (package != null)
                                {
                                    CardHandler.AddOwner(user, package);
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