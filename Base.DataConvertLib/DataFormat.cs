using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("字节顺序，大小端")]
    public enum DataFormat
    {
        [Description("按照顺序排序")]
        ABCD,
        [Description("按照单字反转")]
        BADC,
        [Description("按照双字反转")]
        CDAB,
        [Description("按照倒序排序")]
        DCBA
    }
}
