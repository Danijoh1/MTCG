﻿using MTCG.Handlers;
using Newtonsoft.Json;
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
    public class deckendpoint
    {
        public deckendpoint(httprequest request, httpresponse response)
        {
            UserRepository UserRepository = new UserRepository("Host=localhost;Username=user;Password=password;Database=mtcgdb");
            UserHandler UserHandler = new UserHandler(UserRepository);
            CardRepository CardRepository = new CardRepository("Host=localhost;Username=user;Password=password;Database=mtcgdb");
            CardHandler CardHandler = new CardHandler(CardRepository);
            if (request.content != null)
            {
                try
                {
                    if (request.identity != null)
                    {
                        if (request.method == "GET")
                        {
                            user user = UserHandler.GetByUsername(request.identity);
                            CardHandler.GetDeck(user);
                            response.sendResponse(200, "OK", "");
                            user.deck.playerdeck.ForEach(Console.WriteLine);
                        }
                        else if (request.method == "PUT")
                        {
                            user user = UserHandler.GetByUsername(request.identity);

                            if (user.deck.playerdeck.Count < 4)
                            {
                                List<card> cards = JsonConvert.DeserializeObject<List<card>>(request.content);
                                if (cards.Count == 4)
                                {
                                    CardHandler.AddToDeck(user, cards[0]);
                                    CardHandler.AddToDeck(user, cards[1]);
                                    CardHandler.AddToDeck(user, cards[2]);
                                    CardHandler.AddToDeck(user, cards[3]);
                                    response.sendResponse(201, "Deck configured", "");
                                }
                                else
                                {
                                    response.sendResponse(400, "Deck already full", "");
                                }
                            }
                            else
                            {
                                response.sendResponse(400, "Bad request", "");
                            }
                        }
                        else
                        {
                            response.sendResponse(405, "Method Not Allowed", "");
                        }
                    }
                    else
                    {
                        response.sendResponse(401, "Unauthorized", "");
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