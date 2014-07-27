using Microsoft.AspNet.SignalR.Hubs;
using SignalRExtendLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib
{
    /// <summary>
    /// Session存储池接口
    /// 此接口的实现类必须实现服务器端对Session对象的存取等操作
    /// </summary>
    public interface ISessionPool
    {
        /// <summary>
        /// 通过SessionID从SessionPool获取请求对应的Session
        /// </summary>
        /// <param name="sessionID">SessionID</param>
        /// <returns></returns>
        Session Take(string sessionID);

        /// <summary>
        /// 更新SessionID对应的Session至SessionPool
        /// </summary>
        /// <param name="sessionID">SessionID</param>
        /// <param name="session">新的Session信息</param>
        void Update(string sessionID, Session session);

        /// <summary>
        /// 创建一个新的Session并添加至SessionPool
        /// </summary>
        /// <returns>创建的SessionID</returns>
        string Create();

        /// <summary>
        /// 清除过期Session
        /// </summary>
        void ClearOutOfDate();
    }
}