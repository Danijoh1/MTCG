using System.Text;
using MTCG.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;


namespace MTCG.Http
{
    public class userendpoint
    {
        public userendpoint(httprequest request, httpresponse response, Dictionary<string, byte[]> database)
        {
            if(request.content != null)
            {
                user user = JsonConvert.DeserializeObject<user>(request.content);
                if(request.method == "POST")
                {
                    if (request.path == "/users")
                    {
                        if (user != null)
                        {
                            try
                            {
                                byte[] passwordSource = ASCIIEncoding.ASCII.GetBytes(user.password);
                                byte[] passwordHash = MD5.HashData(passwordSource);
                                database.Add(user.username, passwordHash);
                                response.sendResponse(201, "Created", "");
                            }
                            catch (ArgumentException)
                            {
                                string error = "A user with that username already exists.";
                                string json = JsonConvert.SerializeObject(error);
                                response.sendResponse(400, "Bad Request", json);
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
                            byte[] savedHash;
                            if (database.TryGetValue(user.username, out savedHash))
                            {
                                byte[] passwordSource = ASCIIEncoding.ASCII.GetBytes(user.password);
                                byte[] passwordHash = MD5.HashData(passwordSource);
                                bool passwortEqual = false;
                                if (passwordHash.Length == savedHash.Length)
                                {
                                    int i = 0;
                                    while ((i < passwordHash.Length) && (passwordHash[i] == savedHash[i]))
                                    {
                                        i += 1;
                                    }
                                    if (i == passwordHash.Length)
                                    {
                                        passwortEqual = true;
                                    }
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
                                    string json = JsonConvert.SerializeObject(error);
                                    response.sendResponse(400, "Bad Request", json);
                                }
                            }
                            else
                            {
                                string error = "user does not exist.";
                                string json = JsonConvert.SerializeObject(error);
                                response.sendResponse(404, "Not Found", json);
                            }
                        }
                    }
                }
                else
                {
                    response.sendResponse(405, "Method Not Allowed", "");
                }
            }
        }
    }
}
