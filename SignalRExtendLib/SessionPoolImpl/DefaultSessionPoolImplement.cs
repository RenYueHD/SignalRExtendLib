using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRExtendLib.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.SessionPoolImpl
{
    class DefaultSessionPoolImplement : ISessionPool
    {
        Hashtable dic = Hashtable.Synchronized(new System.Collections.Hashtable());
        private TimeSpan timeOut;

        public Session Take(string sessionId)
        {
            if (dic.ContainsKey(sessionId))
            {
                Session s= dic[sessionId] as Session;
                if (DateTime.Now - s.LastActive >= timeOut)
                {
                    s = new Session(sessionId);
                    s.SessionPool = this;
                    dic[sessionId] = s;
                }
                return s;
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
            ClearOutOfDate();

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

        private void ClearOutOfDate()
        {
            List<string> keys = new List<string>();
            foreach (var k in dic.Keys)
            {
                if (DateTime.Now - (dic[k] as Session).LastActive >= timeOut)
                {
                    keys.Add(k.ToString());
                }
            }

            keys.ForEach(p =>
            {
                dic.Remove(p);
            });
#if DEBUG
            Debug.WriteLine("SessionPool已自动清除"+keys.Count+"条过期Session");
#endif
        }


        public TimeSpan TimeOut
        {
            set
            {
                this.timeOut = value;
            }
            get
            {
                return this.timeOut;
            }
        }
    }
}