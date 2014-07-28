using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRExtendLib.Exceptions;
using SignalRExtendLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.Filter
{
    /// <summary>
    /// 应用该过滤器的Hub或Action将在同一个Connection内通过SignalR内置Cookie功能来支持Session
    /// 若客户端使用jsonp连接到服务器,则无法仅通过该特性实现Session
    /// 若要在使用jsonp连接的客户端也启用Session功能,您的Hub必须同时实现ICookieDistrinctRequest接口
    /// 一旦您的Hub正确实现了ICookieDistrinctRequest接口,则该Session将跨多个页面(连接)直到浏览器/客户端关闭
    /// </summary>
    public class SessionEnableAttribute : Attribute, IAuthorizeHubMethodInvocation
    {
        public bool AuthorizeHubMethodInvocation(Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            if (hubIncomingInvokerContext.Hub.Context.RequestCookies.ContainsKey("ASP.NET_SignalRExtendLib_SessionKey"))
            {
                ServerContext.Session(hubIncomingInvokerContext.Hub.Context).OnInvocal();
            }
            else
            {
                string sessionId = ServerContext.SessionPool.Create();
                Cookie ck = new Cookie("ASP.NET_SignalRExtendLib_SessionKey", sessionId);
                hubIncomingInvokerContext.Hub.Context.RequestCookies.Add("ASP.NET_SignalRExtendLib_SessionKey", ck);
                if (hubIncomingInvokerContext.Hub is ICookieDistrinctRequest)
                {
                    (hubIncomingInvokerContext.Hub as ICookieDistrinctRequest).AppendCookie("ASP.NET_SignalRExtendLib_SessionKey", sessionId);
                }
            }
            return true;
        }
    }
}