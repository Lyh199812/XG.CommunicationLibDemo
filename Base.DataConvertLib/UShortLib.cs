using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("UShort类型数据转换类")]
    public class UShortLib
    {
        [Description("字节数组中截取转成16位无符号整型")]
        public static ushort GetUShortFromByteArray(byte[] value, int start = 0, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] value2 = ByteArrayLib.Get2BytesFromByteArray(value, start, dataFormat);
            return BitConverter.ToUInt16(value2, 0);
        }

        [Description("将字节数组中截取转成16位无符号整型数组")]
        public static ushort[] GetUShortArrayFromByteArray(byte[] value, DataFormat type = DataFormat.ABCD)
        {
            if (value == null)
            {
                throw new ArgumentNullException("检查数组长度是否为空");
            }

            if (value.Length % 2 != 0)
            {
                throw new ArgumentNullException("检查数组长度是否为偶数");
            }

            ushort[] array = new ushort[value.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = GetUShortFromByteArray(value, i * 2, type);
            }

            return array;
        }

        [Description("将字符串转转成16位无符号整型数组")]
        public static ushort[] GetUShortArrayFromString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<ushort> list = new List<ushort>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToUInt16(text.Trim()));
                    }
                }
                else
                {
                    list.Add(Convert.ToUInt16(value.Trim()));
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        [Description("设置字节数组某个位")]
        public static ushort SetBitValueFrom2ByteArray(byte[] value, int offset, bool bitVal, DataFormat dataFormat = DataFormat.ABCD)
        {
            if (offset >= 0 && offset <= 7)
            {
                value[1] = ByteLib.SetbitValue(value[1], offset, bitVal);
            }
            else
            {
                value[0] = ByteLib.SetbitValue(value[0], offset - 8, bitVal);
            }

            return GetUShortFromByteArray(value, 0, dataFormat);
        }

        [Description("设置16位整型某个位")]
        public static ushort SetBitValueFromUShort(ushort value, int offset, bool bitVal, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] byteArrayFromUShort = ByteArrayLib.GetByteArrayFromUShort(value, dataFormat);
            return SetBitValueFrom2ByteArray(byteArrayFromUShort, offset, bitVal, dataFormat);
        }

        [Description("通过布尔长度取整数")]
        public static ushort GetByteLengthFromBoolLength(int boolLength)
        {
            return (boolLength % 8 == 0) ? ((ushort)(boolLength / 8)) : ((ushort)(boolLength / 8 + 1));
        }
    }
}
