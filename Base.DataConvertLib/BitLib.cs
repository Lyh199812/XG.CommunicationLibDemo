using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("Bit类型数据转换类")]
    public class BitLib
    {
        [Description("返回某个字节的指定位")]
        public static bool GetBitFromByte(byte value, int offset)
        {
            return (value & (1 << offset)) != 0;
        }

        [Description("获取字节数组(长度为2)中的指定位")]
        public static bool GetBitFrom2Bytes(byte[] value, int offset, bool isLittleEndian = true)
        {
            if (value.Length < 2)
            {
                throw new ArgumentException("数组长度小于2");
            }

            if (isLittleEndian)
            {
                return GetBitFrom2Bytes(value[1], value[0], offset);
            }

            return GetBitFrom2Bytes(value[0], value[1], offset);
        }

        [Description("获取高低字节的指定位")]
        public static bool GetBitFrom2Bytes(byte high, byte low, int offset)
        {
            if (offset >= 0 && offset <= 7)
            {
                return GetBitFromByte(low, offset);
            }

            return GetBitFromByte(high, offset - 8);
        }

        [Description("返回字节数组中某个字节的指定位")]
        public static bool GetBitFromByteArray(byte[] value, int start, int offset)
        {
            if (start > value.Length - 1)
            {
                throw new ArgumentException("数组长度不够或开始索引太大");
            }

            return GetBitFromByte(value[start], offset);
        }

        [Description("返回字节数组中某2个字节的指定位")]
        public static bool GetBitFrom2BytesArray(byte[] value, int start, int offset, bool isLittleEndian = true)
        {
            if (start > value.Length - 2)
            {
                throw new ArgumentException("数组长度不够或开始索引太大");
            }

            byte[] value2 = new byte[2]
            {
                value[start],
                value[start + 1]
            };
            return GetBitFrom2Bytes(value2, offset, isLittleEndian);
        }

        [Description("根据一个Short返回指定位")]
        public static bool GetBitFromShort(short value, int offset, bool isLittleEndian = true)
        {
            return GetBitFrom2Bytes(BitConverter.GetBytes(value), offset, !isLittleEndian);
        }

        [Description("根据一个UShort返回指定位")]
        public static bool GetBitFromUShort(ushort value, int offset, bool isLittleEndian = true)
        {
            return GetBitFrom2Bytes(BitConverter.GetBytes(value), offset, !isLittleEndian);
        }

        [Description("将字节数组转换成布尔数组")]
        public static bool[] GetBitArrayFromByteArray(byte[] value, int length)
        {
            return GetBitArrayFromByteArray(value, 0, length);
        }

        [Description("将字节数组转换成布尔数组")]
        public static bool[] GetBitArrayFromByteArray(byte[] value, int start, int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("长度必须为正数");
            }

            if (start < 0)
            {
                throw new ArgumentException("开始索引必须为非负数");
            }

            if (start + length > value.Length * 8)
            {
                throw new ArgumentException("数组长度不够或长度太大");
            }

            BitArray bitArray = new BitArray(value);
            bool[] array = new bool[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = bitArray[i + start];
            }

            return array;
        }

        [Description("将字节数组转换成布尔数组")]
        public static bool[] GetBitArrayFromByteArray(byte[] value)
        {
            return GetBitArrayFromByteArray(value, value.Length * 8);
        }

        [Description("将一个字节转换成布尔数组")]
        public static bool[] GetBitArrayFromByte(byte value)
        {
            return GetBitArrayFromByteArray(new byte[1] { value });
        }

        [Description("根据位开始和长度截取布尔数组")]
        public static bool[] GetBitArrayFromBitArray(bool[] value, int start, int length)
        {
            if (start < 0)
            {
                throw new ArgumentException("开始索引不能为负数");
            }

            if (length <= 0)
            {
                throw new ArgumentException("长度必须为正数");
            }

            if (value.Length < start + length)
            {
                throw new ArgumentException("数组长度不够或开始索引太大");
            }

            bool[] array = new bool[length];
            Array.Copy(value, start, array, 0, length);
            return array;
        }

        [Description("将字符串按照指定的分隔符转换成布尔数组")]
        public static bool[] GetBitArrayFromBitArrayString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<bool> list = new List<bool>();
            if (value.Contains(spilt))
            {
                string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                string[] array2 = array;
                foreach (string value2 in array2)
                {
                    list.Add(IsBoolean(value2));
                }
            }
            else
            {
                list.Add(IsBoolean(value));
            }

            return list.ToArray();
        }

        [Description("判断是否为布尔")]
        public static bool IsBoolean(string value)
        {
            return value == "1" || value.ToLower() == "true";
        }
    }
}
