using MTCG.Handlers;
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
        public deckendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    user user = handler.UserHandler.GetByUsername(request.identity);
                    if (request.identity != null)
                    {
                        if (request.method == "GET")
                        {
                            handler.CardHandler.GetDeck(user);
                            string json = JsonConvert.SerializeObject(user.deck);
                            response.sendResponse(200, "OK", json);
                        }
                        else if (request.method == "PUT")
                        {
                            List<string> cardstrings = JsonConvert.DeserializeObject<List<string>>(request.content);
                                List<card> cards = null;
                                if (cardstrings.Count < 4 && cardstrings != null)
                                {
                                    for (int i = 0; i < cardstrings.Count; i++)
                                    {
                                        cards.Add(handler.CardHandler.GetCardById(cardstrings[i]));
                                    }
                                    handler.CardHandler.AddToDeck(user, cards[0]);
                                    handler.CardHandler.AddToDeck(user, cards[1]);
                                    handler.CardHandler.AddToDeck(user, cards[2]);
                                    handler.CardHandler.AddToDeck(user, cards[3]);
                                    response.sendResponse(201, "Deck configured", "");
                                }
                                else
                                {
                                    response.sendResponse(400, "Deck already full", "");
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