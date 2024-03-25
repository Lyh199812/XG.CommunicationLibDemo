using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("Double类型数据转换类")]
    public class DoubleLib
    {
        [Description("将字节数组中某8个字节转换成Double类型")]
        public static double GetDoubleFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get8BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToDouble(value2, 0);
        }

        [Description("将字节数组转换成Double数组")]
        public static double[] GetDoubleArrayFromByteArray(byte[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 8 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为8的倍数");
            }

            double[] array = new double[value.Length / 8];
            for (int i = 0; i < value.Length / 8; i++)
            {
                array[i] = GetDoubleFromByteArray(value, 8 * i, dataFormat);
            }

            return array;
        }

        [Description("将Double字符串转换成双精度浮点型数组")]
        public static double[] GetDoubleArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<double> list = new List<double>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToDouble(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToDouble(value.Trim()));
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
