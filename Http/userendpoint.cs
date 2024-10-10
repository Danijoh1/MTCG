using System.Text;
using MTCG.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace MTCG.Http
{
    public class userendpoint
    {
        public userendpoint(httprequest request, httpresponse response, Dictionary<string, byte[]> database)
        {
            if(request.content != null)
            {
                user user = JsonConvert.DeserializeObject<user>(request.content);
                if(request.path == "/users")
                {
                    if (user != null)
                    {
                        try
                        {
                            byte[] passwordSource = ASCIIEncoding.ASCII.GetBytes(user.password);
                            byte[] passwordHash = new MD5CryptoServiceProvider().ComputeHash(passwordSource);
                            database.Add(user.username, passwordHash);
                            //JsonConvert.SerializeObject <
                            //response.sendResponse(201, "Created");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("A user with that username already exists.");
                        }
                    }
                    else
                    {
                        response.sendResponse(400, "Bad Request", "");
                    }
                }
                else if(request.path == "/sessions")
                {
                    if(user != null)
                    {
                        byte[] savedHash;
                        if (database.TryGetValue(user.username, out savedHash))
                        {
                            byte[] passwordSource = ASCIIEncoding.ASCII.GetBytes(user.password);
                            byte[] passwordHash = new MD5CryptoServiceProvider().ComputeHash(passwordSource);
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

                            }
                            else
                            {
                                Console.WriteLine("username or passwort is wrong.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("user does not exist.");
                        }
                    }
                }
                
            }
        }
    }
}
