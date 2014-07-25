using Microsoft.AspNet.SignalR.Hubs;
using SignalRExtendLib.SessionPoolImpl;
using System;
using System.Collections.Generic;
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
        internal static ISessionPool SessionPool { get; set; }

        static ServerContext()
        {
#warning 未实现反射
            SessionPool = new DefaultSessionPoolImplement();
        }

        /// <summary>
        /// 通过请求上下文获取请求对应的Session
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Session Session(HubCallerContext context)
        {
            return SessionPool.Take(context);
        }
    }
}