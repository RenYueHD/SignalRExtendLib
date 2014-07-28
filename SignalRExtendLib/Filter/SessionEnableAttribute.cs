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
    /// 应用该过滤器的Hub或Action将在同一个Connection/页面内通过SignalR内置Cookie功能来支持Session
    /// 若您的Hub同时实现了ICookieDistrinctRequest接口,则该Session将跨多个Connection/页面直到浏览器/客户端关闭
    /// 注:Session无法被跨域访问
    /// </summary>
    public class SessionEnableAttribute : Attribute,IAuthorizeHubMethodInvocation
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