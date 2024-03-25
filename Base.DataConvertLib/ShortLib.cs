using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("Short类型数据转换类")]
    public class ShortLib
    {
        [Description("字节数组中截取转成16位整型")]
        public static short GetShortFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get2BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToInt16(value2, 0);
        }

        [Description("将字节数组中截取转成16位整型数组")]
        public static short[] GetShortArrayFromByteArray(byte[] value, DataFormat type = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 2 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为偶数");
            }

            short[] array = new short[value.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = GetShortFromByteArray(value, i * 2, type);
            }

            return array;
        }

        [Description("将字符串转转成16位整型数组")]
        public static short[] GetShortArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<short> list = new List<short>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToInt16(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToInt16(value.Trim()));
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        [Description("设置字节数组某个位")]
        public static short SetBitValueFrom2ByteArray(byte[] value, int offset, bool bitVal, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (offset >= 0 && offset <= 7)
            {
                value[1] = ByteLib.SetbitValue(value[1], offset, bitVal);
            }
            else
            {
                value[0] = ByteLib.SetbitValue(value[0], offset - 8, bitVal);
            }

            return GetShortFromByteArray(value, 0, dataFormat);
        }

        [Description("设置16位整型某个位")]
        public static short SetBitValueFromShort(short value, int offset, bool bitVal, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] byteArrayFromShort = ByteArrayLib.GetByteArrayFromShort(value, dataFormat);
            return SetBitValueFrom2ByteArray(byteArrayFromShort, offset, bitVal, dataFormat);
        }

        [Description("通过布尔长度取整数")]
        public static short GetByteLengthFromBoolLength(int boolLength)
        {
            return (boolLength % 8 == 0) ? ((short)(boolLength / 8)) : ((short)(boolLength / 8 + 1));
        }
    }
}
