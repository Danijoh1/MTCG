using System.Text;
using MTCG.Models;
using MTCG.Handlers;
using MTCG.Repositories;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;


namespace MTCG.Http.Endpoints
{
    public class userendpoint
    {
        public userendpoint(httprequest request, httpresponse response)
        {
            
            UserRepository UserRepository = new UserRepository("Host=localhost;Username=user;Password=password;Database=mtcgdb");
            UserHandler handler = new UserHandler(UserRepository);
            if (request.content != null)
            {
                try
                {
                    user user = JsonConvert.DeserializeObject<user>(request.content);
                    if (request.method == "POST")
                    {
                        if (request.path == "/users")
                        {
                            if (user != null)
                            {
                                try
                                {
                                    
                                    handler.AddUser(user);
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
                            if (user != null)
                            {
                                user savedUser = handler.GetByUsername(user.Username);
                                if (savedUser != null)
                                {
                                    bool passwortEqual = false;
                                    if (user.Password == savedUser.Password)
                                    {
                                        passwortEqual = true;
                                    }

                                    if (passwortEqual == true)
                                    {
                                        string token = "-mtcgToken";
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
