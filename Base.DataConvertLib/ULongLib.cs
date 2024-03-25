using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{

    [Description("ULong类型数据转换类")]
    public class ULongLib
    {
        [Description("字节数组中截取转成64位无符号整型")]
        public static ulong GetULongFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get8BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToUInt64(value2, 0);
        }

        [Description("将字节数组中截取转成64位无符号整型数组")]
        public static ulong[] GetULongArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 8 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为8的倍数");
            }

            ulong[] array = new ulong[value.Length / 8];
            for (int i = 0; i < value.Length / 8; i++)
            {
                array[i] = GetULongFromByteArray(value, 8 * i, dataFormat);
            }

            return array;
        }

        [Description("将字符串转转成64位无符号整型数组")]
        public static ulong[] GetULongArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<ulong> list = new List<ulong>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToUInt64(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToUInt64(value.Trim()));
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
