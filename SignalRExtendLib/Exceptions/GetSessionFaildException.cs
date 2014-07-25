using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRExtendLib.Exceptions
{
    public class GetSessionFaildException :Exception
    {
        public GetSessionFaildException(string msg = null)
            :base(msg)
        {

        }
    }
}
