using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRExtendLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.Filter
{
    /// <summary>
    /// 应用该过滤器的Hub或Action将支持Session
    /// </summary>
    public class SessionEnableAttribute : Attribute, IAuthorizeHubConnection, IAuthorizeHubMethodInvocation
    {
        public bool AuthorizeHubConnection(Microsoft.AspNet.SignalR.Hubs.HubDescriptor hubDescriptor, IRequest request)
        {
            ServerContext.SessionPool.ClearOutOfDate();
            return true;
        }

        public bool AuthorizeHubMethodInvocation(Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            try
            {
                ServerContext.Session(hubIncomingInvokerContext.Hub.Context).OnInvocal();
            }
            catch (GetSessionFaildException)
            {
                ServerContext.SessionPool.Init(hubIncomingInvokerContext.Hub.Context);
            }
            return true;
        }
    }
}