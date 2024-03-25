using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("UInt类型数据转换类")]
    public class UIntLib
    {
        [Description("字节数组中截取转成32位无符号整型")]
        public static uint GetUIntFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToUInt32(value2, 0);
        }

        [Description("将字节数组中截取转成32位无符号整型数组")]
        public static uint[] GetUIntArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 4 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为4的倍数");
            }

            uint[] array = new uint[value.Length / 4];
            for (int i = 0; i < value.Length / 4; i++)
            {
                array[i] = GetUIntFromByteArray(value, 4 * i, dataFormat);
            }

            return array;
        }

        [Description("将字符串转转成32位无符号整型数组")]
        public static uint[] GetUIntArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<uint> list = new List<uint>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToUInt32(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToUInt32(value.Trim()));
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        [Description("通过布尔长度取整数")]
        public static uint GetByteLengthFromBoolLength(int boolLength)
        {
            return (uint)((boolLength % 8 == 0) ? (boolLength / 8) : (boolLength / 8 + 1));
        }
    }
}
