using System.Text;
using MTCG.Models;
using MTCG.Handlers;
using MTCG.Repositories;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using NSubstitute;
using System.Linq;


namespace MTCG.Http.Endpoints
{
    public class userendpoint
    {
        public userendpoint(httprequest request, httpresponse response, DatabaseHandlers handler)
        {
            if (request.content != null)
            {
                try
                {
                    user requestedUser = JsonConvert.DeserializeObject<user>(request.content);
                    if (request.method == "POST")
                    {
                        if (request.path == "/users")
                        {
                            if (requestedUser != null)
                            {
                                try
                                {
                                    handler.UserHandler.AddUser(requestedUser);
                                    string createmessage = "User created";
                                    response.sendResponse(201, createmessage, "");
                                }
                                catch (ArgumentException)
                                {
                                    string error = "A user with that username already exists.";
                                    response.sendResponse(400, error, "");
                                }
                            }
                            else
                            {
                                response.sendResponse(400, "Bad Request", "");
                            }
                        }
                        else if (request.path == "/sessions")
                        {
                            if (requestedUser != null)
                            {
                                user savedUser = handler.UserHandler.GetByUsername(requestedUser.username);
                                if (savedUser != null)
                                {
                                    bool passwortEqual = false;
                                    if (requestedUser.password == savedUser.password)
                                    {
                                        passwortEqual = true;
                                    }

                                    if (passwortEqual == true)
                                    {
                                        string token = savedUser.username+"-mtcgToken";
                                        string json = JsonConvert.SerializeObject(token);
                                        response.sendResponse(200, "OK", json);
                                    }
                                    else
                                    {

                                        string error = "username or passwort is wrong.";
                                        response.sendResponse(400, error, "");
                                    }
                                }
                                else
                                {
                                    string error = "user does not exist.";
                                    response.sendResponse(404, error, "");
                                }
                            }
                        }

                    }
                    else if (request.method == "PUT")
                    {
                        if (request.path.Contains(request.identity))
                        {
                            handler.UserHandler.UpdateUserInfo(requestedUser);
                            response.sendResponse(202, "Userdata updated", "");
                        }
                        else
                        {
                            response.sendResponse(403, "Forbidden", "");
                        }
                    }
                    else if (request.method == "GET")
                    {
                        if (request.path.Contains(request.identity))
                        {
                            user userinfo = handler.UserHandler.GetUserInfoByUsername(request.identity);
                            string json = JsonConvert.SerializeObject(userinfo);
                            response.sendResponse(200, "OK", json);
                        }
                        else
                        {
                            response.sendResponse(403, "Forbidden", "");
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
