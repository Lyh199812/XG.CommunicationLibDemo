using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("Byte类型数据转换类")]
    public class ByteLib
    {
        [Description("将字节中的某个位赋值")]
        public static byte SetbitValue(byte value, int offset, bool bitValue)
        {
            return bitValue ? ((byte)(value | (byte)Math.Pow(2.0, offset))) : ((byte)(value & ~(byte)Math.Pow(2.0, offset)));
        }

        [Description("从字节数组中截取某个字节")]
        public static byte GetByteFromByteArray(byte[] value, int start)
        {
            if (start > value.Length - 1)
            {
                throw new ArgumentException("字节数组长度不够或开始索引太大");
            }

            return value[start];
        }

        [Description("将布尔数组转换成字节数组")]
        public static byte GetByteFromBoolArray(bool[] value)
        {
            if (value.Length != 8)
            {
                throw new ArgumentNullException("检查数组长度是否为8");
            }

            byte b = 0;
            for (int i = 0; i < 8; i++)
            {
                b = SetbitValue(b, i, value[i]);
            }

            return b;
        }
    }
}
