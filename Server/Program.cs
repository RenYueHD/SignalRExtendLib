using Microsoft.Owin.Hosting;
using SignalRExtendLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 or http://+:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.

            using (WebApp.Start<Startup>("http://localhost:8080/"))
            {
                Console.WriteLine("SignalR服务器已在 http://localhost:8080 启动");
                Console.ReadLine();
            }
        }
    }
}