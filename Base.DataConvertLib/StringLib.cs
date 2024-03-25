using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("字符串类型数据转换类")]
    public class StringLib
    {
        [Description("将字节数组转换成字符串")]
        public static string GetStringFromByteArrayByBitConvert(byte[] value, int start, int count)
        {
            return BitConverter.ToString(value, start, count);
        }

        [Description("将字节数组转换成字符串")]
        public static string GetStringFromByteArrayByBitConvert(byte[] value)
        {
            return BitConverter.ToString(value, 0, value.Length);
        }

        [Description("将字节数组转换成带编码格式字符串")]
        public static string GetStringFromByteArrayByEncoding(byte[] value, int start, int count, Encoding encoding)
        {
            return encoding.GetString(ByteArrayLib.GetByteArrayFromByteArray(value, start, count));
        }

        [Description("将字节数组转换成带编码格式字符串")]
        public static string GetStringFromByteArrayByEncoding(byte[] value, Encoding encoding)
        {
            return encoding.GetString(ByteArrayLib.GetByteArrayFromByteArray(value, 0, value.Length));
        }

        [Description("根据起始地址和长度将字节数组转换成带16进制字符串")]
        public static string GetHexStringFromByteArray(byte[] value, int start, int count, string segment = " ")
        {
            byte[] byteArrayFromByteArray = ByteArrayLib.GetByteArrayFromByteArray(value, start, count);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array = byteArrayFromByteArray;
            foreach (byte b in array)
            {
                if (segment.Length == 0)
                {
                    stringBuilder.Append($"{b:X2}");
                }
                else
                {
                    stringBuilder.Append($"{b:X2}{segment}");
                }
            }

            if (segment.Length != 0 && stringBuilder.Length > 1 && stringBuilder.ToString().Substring(stringBuilder.Length - segment.Length) == segment)
            {
                stringBuilder.Remove(stringBuilder.Length - segment.Length, segment.Length);
            }

            return stringBuilder.ToString();
        }

        [Description("将整个字节数组转换成带16进制字符串")]
        public static string GetHexStringFromByteArray(byte[] source, string segment = " ")
        {
            return GetHexStringFromByteArray(source, 0, source.Length, segment);
        }

        [Description("将字节数组转换成西门子字符串")]
        public static string GetSiemensStringFromByteArray(byte[] source, int start, int length, string emptyStr = "empty")
        {
            byte[] byteArrayFromByteArray = ByteArrayLib.GetByteArrayFromByteArray(source, start, length + 2);
            int num = byteArrayFromByteArray[1];
            if (num > 0)
            {
                return Encoding.GetEncoding("GBK").GetString(ByteArrayLib.GetByteArrayFromByteArray(byteArrayFromByteArray, 2, num));
            }

            return emptyStr;
        }

        [Description("根据起始地址和长度将各种类型数组转换成字符串")]
        public static string GetStringFromValueArray<T>(T[] value, int start, int length, string segment = " ")
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
                throw new ArgumentException("字节数组长度不够或开始索引太大");
            }

            T[] array = new T[length];
            Array.Copy(value, start, array, 0, length);
            return GetStringFromValueArray(array, segment);
        }

        [Description("各种类型数组转换成字符串")]
        public static string GetStringFromValueArray<T>(T[] value, string segment = " ")
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (value.Length != 0)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    T val = value[i];
                    if (segment.Length == 0)
                    {
                        stringBuilder.Append(val.ToString());
                    }
                    else
                    {
                        stringBuilder.Append(val.ToString() + segment.ToString());
                    }
                }
            }

            if (segment.Length != 0 && stringBuilder.Length > 1 && stringBuilder.ToString().Substring(stringBuilder.Length - segment.Length) == segment)
            {
                stringBuilder.Remove(stringBuilder.Length - segment.Length, segment.Length);
            }

            return stringBuilder.ToString();
        }
    }
}
