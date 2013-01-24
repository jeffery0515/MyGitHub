using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xktec.hqfq.Common
{
    public class CustomerJsonResult
    {
        public Boolean Success { get; set; }
        public String Message { get; set; }
        public Object Data { get; set; }
    }

    public enum LineState
    {
        所有 = 0,
        首部广告 = 1,
        首页列表 = 2,
        正常显示 = 3,
        关闭 = 4,
        正常且不在首页=5
    }
}