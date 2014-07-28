using Microsoft.AspNet.SignalR.Hubs;
using SignalRExtendLib.Exceptions;
using SignalRExtendLib.SessionPoolImpl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib
{
    /// <summary>
    /// 服务器上下文对象
    /// </summary>
    public class ServerContext
    {
        /// <summary>
        /// 初始化ServerContext
        /// </summary>
        /// <param name="pool">ServerContext使用的SessionPool(null则使用系统默认的SessionPool)</param>
        /// <param name="timeOut">Session超时时间(默认30分钟)</param>
        public static void Init(ISessionPool pool=null,TimeSpan? timeOut= null)
        {
            if (pool == null)
            {
                SessionPool = new DefaultSessionPoolImplement();
            }
            else
            {
                SessionPool = pool;
            }
            SessionPool.TimeOut = timeOut.HasValue ? timeOut.Value : new TimeSpan(0, 30, 0);
            Debug.WriteLine("ServerContext初始化成功,Session超时时间:"+SessionPool.TimeOut.TotalMinutes+"分钟");
        }

        static ServerContext()
        {
            Init();
        }

        internal static ISessionPool SessionPool { get; set; }

        /// <summary>
        /// 通过请求上下文获取请求对应的Session
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public static Session Session(HubCallerContext context)
        {
            if (!context.RequestCookies.ContainsKey("ASP.NET_SignalRExtendLib_SessionKey"))
            {
                throw new GetSessionFaildException("无法获取Session或该Hub未添加SessionEnableAttribute特性");
            }
            else
            {
                return SessionPool.Take(context.RequestCookies["ASP.NET_SignalRExtendLib_SessionKey"].Value);
            }
        }
    }
}