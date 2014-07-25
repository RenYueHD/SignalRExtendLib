using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib
{
    /// <summary>
    /// Session存储池接口
    /// 此接口的实现类必须实现对Session对象的存储等操作
    /// </summary>
    public interface ISessionPool
    {
        /// <summary>
        /// 通过请求上下文从SessionPool获取请求对应的Session
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        Session Take(HubCallerContext context);

        /// <summary>
        /// 更新请求上下文对应的Session至SessionPool
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="session">Session信息</param>
        void Update(HubCallerContext context,Session session);

        /// <summary>
        /// 为请求上下文初始化对应的Session并写入SessionPool
        /// </summary>
        /// <param name="context">请求上下文</param>
        void Init(HubCallerContext context);

        /// <summary>
        /// 清除过期Session
        /// </summary>
        void ClearOutOfDate();
    }
}