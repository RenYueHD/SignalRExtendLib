using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.Interface
{
    /// <summary>
    /// 客户端Cookie持久化接口
    /// 若您的Hub需要启用jsonp模式下的Session,或者你的Session必须跨越多个页面,则您的Hub必须正确实现该接口
    /// </summary>
    public interface ICookieDistrinctRequest
    {
        /// <summary>
        /// 此方法必须实现将Cookie永久保存到客户端,并且保证客户端的下次请求中能包含该Cookie
        /// 例:当您的客户端为一个WEB浏览器,您必须保证将此Cookie正确写入浏览器中
        /// </summary>
        /// <param name="ckName">Cookie名</param>
        /// <param name="ckValue">Cookie值</param>
        /// <param name="expire">Cookie过期时间(默认为浏览器/客户端关闭)</param>
        void AppendCookie(string ckName, string ckValue, DateTime? expire=null);
    }
}
