using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
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

        public Session Take(string sessionId)
        {
            if (dic.ContainsKey(sessionId))
            {
                return dic[sessionId];
            }
            else
            {
                Session s = new Session(sessionId);
                s.SessionPool = this;
                dic.Add(sessionId, s);
                return s;
            }
        }


        public string Create()
        {
            string sessionId = Guid.NewGuid().ToString();   
            Session session = new Session(sessionId);
            session.SessionPool = this;
            dic.Add(sessionId, session);
            return sessionId;
        }

        public void Update(string sessionID, Session session)
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