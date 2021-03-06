﻿using Microsoft.AspNet.SignalR;
using SignalRExtendLib;
using SignalRExtendLib.Filter;
using SignalRExtendLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [SessionEnable]
    public class MyServiceHub : Hub,ICookieDistrinctRequest
    {

        public void HelloWorld()
        {
            object name = ServerContext.Session(Context)["username"];
            if (name == null)
            {
                Console.WriteLine("ID为" + base.Context.ConnectionId + "的用户进入聊天室");
                Clients.All.pushMessage("管理员", "欢迎ID为" + base.Context.ConnectionId + "的用户进入聊天室");
            }
            else
            {
                Console.WriteLine("Name为" + name + "的用户回到聊天室");
                Clients.All.pushMessage("管理员", "欢迎" + name + "回到聊天室");
            }
        }

        public void SetName(string name)
        {
            ServerContext.Session(Context)["username"] = name;
        }

        public void SendMessage(string msg)
        {
            object name = ServerContext.Session(Context)["username"];

            Clients.All.pushMessage(name==null? Context.ConnectionId:name.ToString(), msg);
        }

        public void SendObject(dynamic obj)
        {
            Console.WriteLine("客户端发送了一个对象,类型为" + obj.GetType().Name.ToString());
            Clients.Caller.pushMessage("管理员", "您向我发送了一个类型为" + obj.GetType().Name.ToString() + "的对象");
        }

        public string GetSessionId()
        {
            return Context.RequestCookies["ASP.NET_SignalRExtendLib_SessionKey"].Value;
        }

        public void AppendCookie(string ckName, string ckValue, DateTime? expire = null)
        {
            if (expire.HasValue)
            {
                Clients.Caller.addCookie(ckName, ckValue, (expire.Value - new DateTime(1970, 1, 1)).TotalMilliseconds);
            }
            else
            {
                Clients.Caller.addCookie(ckName, ckValue);
            }
        }
    }
}