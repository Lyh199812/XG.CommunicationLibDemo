using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("Float类型数据转换类")]
    public class FloatLib
    {
        [Description("将字节数组中某4个字节转换成Float类型")]
        public static float GetFloatFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get4BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToSingle(value2, 0);
        }

        [Description("将字节数组转换成Float数组")]
        public static float[] GetFloatArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 4 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为4的倍数");
            }

            float[] array = new float[value.Length / 4];
            for (int i = 0; i < value.Length / 4; i++)
            {
                array[i] = GetFloatFromByteArray(value, 4 * i, dataFormat);
            }

            return array;
        }

        [Description("将Float字符串转换成单精度浮点型数组")]
        public static float[] GetFloatArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<float> list = new List<float>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToSingle(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToSingle(value.Trim()));
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
