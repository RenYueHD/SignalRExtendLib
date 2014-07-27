using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib
{
    /// <summary>
    /// SignalR的Session对象
    /// </summary>
    public class Session
    {
        /// <summary>
        /// 上次请求时间
        /// </summary>
        internal DateTime LastActive{get;set;}

        internal ISessionPool SessionPool { get; set; }

        public string SessionID { get; set; }

        /// <summary>
        /// 构造新的Session对象
        /// </summary>
        /// <param name="sessionID"></param>
        public Session(string sessionID)
        {
            this.SessionID = sessionID;
            dic = new Dictionary<string, object>();
            LastActive = DateTime.Now;
        }
        
        private Dictionary<string, Object> dic;

        /// <summary>
        /// 获取或设置Session中对应键的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public Object this[string key]
        {
            get
            {
                if (dic.ContainsKey(key))
                {
                    return dic[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (dic.ContainsKey(key))
                {
                    dic.Remove(key);
                }
                dic.Add(key, value);

                SessionPool.Update(SessionID, this);
            }
        }

        /// <summary>
        /// 更新Session的上次请求时间
        /// </summary>
        internal void OnInvocal()
        {
            LastActive = DateTime.Now;
        }
    }
}
