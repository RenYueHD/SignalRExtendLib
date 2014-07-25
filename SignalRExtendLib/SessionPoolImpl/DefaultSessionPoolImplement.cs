using Microsoft.AspNet.SignalR;
using SignalRExtendLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.SessionPoolImpl
{
    class DefaultSessionPoolImplement : ISessionPool
    {
        private Dictionary<string, Session> dic = new Dictionary<string, Session>();

        public Session Take(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context)
        {
            if (!context.RequestCookies.ContainsKey("ASP.NET_Signal_SessionKey"))
            {
                throw new GetSessionFaildException("获取Session失败");
            }
            else
            {
                Cookie ck = context.RequestCookies["ASP.NET_Signal_SessionKey"];
                Session s = null;
                if (dic.ContainsKey(ck.Value))
                {
                    s = dic[ck.Value];
                }
                else
                {
                    s = new Session(context);
                    s.SessionPool = this;
                    dic.Add(ck.Value, s);
                }

                s.Context = context;

                return s;
            }
        }


        public void Init(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context)
        {
            Cookie ck = new Cookie("ASP.NET_Signal_SessionKey", Guid.NewGuid().ToString());

#warning Cookie在刷新页面后失效
            context.RequestCookies.Add("ASP.NET_Signal_SessionKey", ck);

            Session session = new Session(context);
            session.SessionPool = this;
            dic.Add(ck.Value, session);

            Debug.WriteLine("为当前Connection创建Session");
        }

        public void Update(Microsoft.AspNet.SignalR.Hubs.HubCallerContext context, Session session)
        {
            //此方法在本实现类中无需手动实现
        }

        public void ClearOutOfDate()
        {
            List<string> keys = dic.Where(p => (DateTime.Now - p.Value.LastActive).TotalMinutes >= 30).Select(p => p.Key).ToList();
#if DEBUG
            int count = 0;
#endif
            lock (this)
            {
                keys.ForEach(p =>
                {
                    dic.Remove(p);
#if DEBUG
                    count++;
#endif
                });
            }

#if DEBUG
            Debug.WriteLine("已清除"+count+"条过期Session");
#endif
        }
    }
}