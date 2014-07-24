using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8080/";
            var connection = new HubConnection(url);

            //必须在Connection连接之前创建Hub
            var serviceHub = connection.CreateHubProxy("MyServiceHub");

            //注册该Hub的Hit方法对应的客户端处理程序
            serviceHub.On<string,string>("pushMessage", (p,q) =>
            {
                Console.WriteLine(p+":"+q);
            });

            Console.WriteLine("正在连接服务器...");
            connection.Start().Wait();

            if (connection.State == ConnectionState.Disconnected)
            {
                Console.WriteLine("连接服务器失败");
            }
            else
            {
                Console.WriteLine("连接服务器成功");
                serviceHub.Invoke("HelloWorld").Wait();
                while (true)
                {
                    Console.WriteLine("请选择:\n1.发送消息\n2.发送测试对象\n0.退出");
                    string key = Console.ReadLine();
                    if (key == "1")
                    {
                        Console.WriteLine("请输入消息内容:");
                        serviceHub.Invoke("SendMessage", Console.ReadLine()).Wait();
                    }
                    else if (key == "2")
                    {
                        var obj = new { Name = "小明", Age = 18 };
                        serviceHub.Invoke("SendObject",obj).Wait();
                    }
                    else if (key == "0")
                    {
                        connection.Stop();
                        return;
                    }
                }
            }
        }
    }
}