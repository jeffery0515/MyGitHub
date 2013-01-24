using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xktec.hqfq.Common
{
    public enum LineState
    {
        所有 = 0,
        首部广告 = 1,
        首页列表 = 2,
        正常显示 = 3,
        关闭 = 4,
        正常且不在首页 = 5
    }
}
