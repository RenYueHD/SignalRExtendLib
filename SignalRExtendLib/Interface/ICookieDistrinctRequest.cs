using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.Interface
{
    /// <summary>
    /// 客户端Cookie持久化接口
    /// </summary>
    public interface ICookieDistrinctRequest
    {
        /// <summary>
        /// 此方法必须实现将Cookie永久保存到客户端
        /// 并且能跨同一个客户端的多次请求(大部分情况下为页面刷新)操作
        /// </summary>
        /// <param name="ckName">Cookie名</param>
        /// <param name="ckValue">Cookie值</param>
        /// <param name="expire">Cookie过期时间(默认为浏览器/客户端关闭)</param>
        void AppendCookie(string ckName, string ckValue, DateTime? expire=null);
    }
}
