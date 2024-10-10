using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.Http
{
    public class userendpoint
    {
        public userendpoint(httprequest request, httpresponse response)
        {
            handleRequest(request);
            
        }
        public void handleRequest(httprequest request)
        {
            if(request.method == "GET")
            {

            }
            else if(request.method == "POST")
            {

            }
        }
        public void login()
        {

        }
        public void register()
        {

        }
    }
}
