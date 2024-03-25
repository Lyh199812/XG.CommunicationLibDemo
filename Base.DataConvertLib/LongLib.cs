using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("Long类型数据转换类")]
    public class LongLib
    {
        [Description("字节数组中截取转成64位整型")]
        public static long GetLongFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get8BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToInt64(value2, 0);
        }

        [Description("将字节数组中截取转成64位整型数组")]
        public static long[] GetLongArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 8 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为8的倍数");
            }

            long[] array = new long[value.Length / 8];
            for (int i = 0; i < value.Length / 8; i++)
            {
                array[i] = GetLongFromByteArray(value, 8 * i, dataFormat);
            }

            return array;
        }

        [Description("将字符串转转成64位整型数组")]
        public static long[] GetLongArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<long> list = new List<long>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToInt64(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToInt64(value.Trim()));
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }
    }
}
