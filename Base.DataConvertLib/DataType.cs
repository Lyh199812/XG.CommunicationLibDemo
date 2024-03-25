using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("常规数据类型")]
    public enum DataType
    {
        [Description("布尔类型")]
        Bool,
        [Description("字节类型")]
        Byte,
        [Description("有符号16位短整型")]
        Short,
        [Description("无符号16位短整型")]
        UShort,
        [Description("有符号32位短整型")]
        Int,
        [Description("无符号32位短整型")]
        UInt,
        [Description("32位单精度浮点数")]
        Float,
        [Description("64位双精度浮点数")]
        Double,
        [Description("有符号64位长整型")]
        Long,
        [Description("无符号64位长整型")]
        ULong,
        [Description("字符串类型")]
        String,
        [Description("字节数组")]
        ByteArray,
        [Description("16进制字符串")]
        HexString
    }
}
