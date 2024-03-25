using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Base.DataConvertLib
{
    [Description("字节数组类型数据转换类")]
    public class ByteArrayLib
    {
        [Description("根据起始地址和长度自定义截取字节数组")]
        public static byte[] GetByteArrayFromByteArray(byte[] data, int start, int length)
        {
            if (start < 0)
            {
                throw new ArgumentException("开始索引不能为负数");
            }

            if (length <= 0)
            {
                throw new ArgumentException("长度必须为正数");
            }

            if (data.Length < start + length)
            {
                throw new ArgumentException("字节数组长度不够或开始索引太大");
            }

            byte[] array = new byte[length];
            Array.Copy(data, start, array, 0, length);
            return array;
        }

        [Description("根据起始地址自定义截取字节数组")]
        public static byte[] GetByteArrayFromByteArray(byte[] data, int start)
        {
            return GetByteArrayFromByteArray(data, start, data.Length - start);
        }

        [Description("从字节数组中截取2个字节,并按指定字节序返回")]
        public static byte[] Get2BytesFromByteArray(byte[] value, int start, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] byteArrayFromByteArray = GetByteArrayFromByteArray(value, start, 2);
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    return byteArrayFromByteArray.Reverse().ToArray();
                case DataFormat.BADC:
                case DataFormat.DCBA:
                    return byteArrayFromByteArray;
                default:
                    return byteArrayFromByteArray;
            }
        }

        [Description("从字节数组中截取4个字节,并按指定字节序返回")]
        public static byte[] Get4BytesFromByteArray(byte[] value, int start, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] byteArrayFromByteArray = GetByteArrayFromByteArray(value, start, 4);
            byte[] array = new byte[4];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = byteArrayFromByteArray[3];
                    array[1] = byteArrayFromByteArray[2];
                    array[2] = byteArrayFromByteArray[1];
                    array[3] = byteArrayFromByteArray[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = byteArrayFromByteArray[1];
                    array[1] = byteArrayFromByteArray[0];
                    array[2] = byteArrayFromByteArray[3];
                    array[3] = byteArrayFromByteArray[2];
                    break;
                case DataFormat.BADC:
                    array[0] = byteArrayFromByteArray[2];
                    array[1] = byteArrayFromByteArray[3];
                    array[2] = byteArrayFromByteArray[0];
                    array[3] = byteArrayFromByteArray[1];
                    break;
                case DataFormat.DCBA:
                    array = byteArrayFromByteArray;
                    break;
            }

            return array;
        }

        [Description("从字节数组中截取8个字节,并按指定字节序返回")]
        public static byte[] Get8BytesFromByteArray(byte[] value, int start, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] array = new byte[8];
            byte[] byteArrayFromByteArray = GetByteArrayFromByteArray(value, start, 8);
            if (byteArrayFromByteArray == null)
            {
                return null;
            }

            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = byteArrayFromByteArray[7];
                    array[1] = byteArrayFromByteArray[6];
                    array[2] = byteArrayFromByteArray[5];
                    array[3] = byteArrayFromByteArray[4];
                    array[4] = byteArrayFromByteArray[3];
                    array[5] = byteArrayFromByteArray[2];
                    array[6] = byteArrayFromByteArray[1];
                    array[7] = byteArrayFromByteArray[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = byteArrayFromByteArray[1];
                    array[1] = byteArrayFromByteArray[0];
                    array[2] = byteArrayFromByteArray[3];
                    array[3] = byteArrayFromByteArray[2];
                    array[4] = byteArrayFromByteArray[5];
                    array[5] = byteArrayFromByteArray[4];
                    array[6] = byteArrayFromByteArray[7];
                    array[7] = byteArrayFromByteArray[6];
                    break;
                case DataFormat.BADC:
                    array[0] = byteArrayFromByteArray[6];
                    array[1] = byteArrayFromByteArray[7];
                    array[2] = byteArrayFromByteArray[4];
                    array[3] = byteArrayFromByteArray[5];
                    array[4] = byteArrayFromByteArray[2];
                    array[5] = byteArrayFromByteArray[3];
                    array[6] = byteArrayFromByteArray[0];
                    array[7] = byteArrayFromByteArray[1];
                    break;
                case DataFormat.DCBA:
                    array = byteArrayFromByteArray;
                    break;
            }

            return array;
        }

        [Description("比较两个字节数组是否完全相同")]
        public static bool GetByteArrayEquals(byte[] value1, byte[] value2)
        {
            if (value1 == null || value2 == null)
            {
                return false;
            }

            if (value1.Length != value2.Length)
            {
                return false;
            }

            for (int i = 0; i < value1.Length; i++)
            {
                if (value1[i] != value2[i])
                {
                    return false;
                }
            }

            return true;
        }

        [Description("将单个字节转换成字节数组")]
        public static byte[] GetByteArrayFromByte(byte value)
        {
            return new byte[1] { value };
        }

        [Description("将Short类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromShort(short value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[2];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    break;
                case DataFormat.BADC:
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将UShort类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromUShort(ushort value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[2];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    break;
                case DataFormat.BADC:
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将Int类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromInt(int value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[4];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = bytes[3];
                    array[1] = bytes[2];
                    array[2] = bytes[1];
                    array[3] = bytes[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    array[2] = bytes[3];
                    array[3] = bytes[2];
                    break;
                case DataFormat.BADC:
                    array[0] = bytes[2];
                    array[1] = bytes[3];
                    array[2] = bytes[0];
                    array[3] = bytes[1];
                    break;
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将UInt类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromUInt(uint value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[4];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = bytes[3];
                    array[1] = bytes[2];
                    array[2] = bytes[1];
                    array[3] = bytes[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    array[2] = bytes[3];
                    array[3] = bytes[2];
                    break;
                case DataFormat.BADC:
                    array[0] = bytes[2];
                    array[1] = bytes[3];
                    array[2] = bytes[0];
                    array[3] = bytes[1];
                    break;
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将Float数值转换成字节数组")]
        public static byte[] GetByteArrayFromFloat(float value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[4];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = bytes[3];
                    array[1] = bytes[2];
                    array[2] = bytes[1];
                    array[3] = bytes[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    array[2] = bytes[3];
                    array[3] = bytes[2];
                    break;
                case DataFormat.BADC:
                    array[0] = bytes[2];
                    array[1] = bytes[3];
                    array[2] = bytes[0];
                    array[3] = bytes[1];
                    break;
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将Double类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromDouble(double value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[8];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = bytes[7];
                    array[1] = bytes[6];
                    array[2] = bytes[5];
                    array[3] = bytes[4];
                    array[4] = bytes[3];
                    array[5] = bytes[2];
                    array[6] = bytes[1];
                    array[7] = bytes[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    array[2] = bytes[3];
                    array[3] = bytes[2];
                    array[4] = bytes[5];
                    array[5] = bytes[4];
                    array[6] = bytes[7];
                    array[7] = bytes[6];
                    break;
                case DataFormat.BADC:
                    array[0] = bytes[6];
                    array[1] = bytes[7];
                    array[2] = bytes[4];
                    array[3] = bytes[5];
                    array[4] = bytes[2];
                    array[5] = bytes[3];
                    array[6] = bytes[0];
                    array[7] = bytes[1];
                    break;
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将Long类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromLong(long value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[8];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = bytes[7];
                    array[1] = bytes[6];
                    array[2] = bytes[5];
                    array[3] = bytes[4];
                    array[4] = bytes[3];
                    array[5] = bytes[2];
                    array[6] = bytes[1];
                    array[7] = bytes[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    array[2] = bytes[3];
                    array[3] = bytes[2];
                    array[4] = bytes[5];
                    array[5] = bytes[4];
                    array[6] = bytes[7];
                    array[7] = bytes[6];
                    break;
                case DataFormat.BADC:
                    array[0] = bytes[6];
                    array[1] = bytes[7];
                    array[2] = bytes[4];
                    array[3] = bytes[5];
                    array[4] = bytes[2];
                    array[5] = bytes[3];
                    array[6] = bytes[0];
                    array[7] = bytes[1];
                    break;
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将ULong类型数值转换成字节数组")]
        public static byte[] GetByteArrayFromULong(ulong value, DataFormat dataFormat = DataFormat.ABCD)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            byte[] array = new byte[8];
            switch (dataFormat)
            {
                case DataFormat.ABCD:
                    array[0] = bytes[7];
                    array[1] = bytes[6];
                    array[2] = bytes[5];
                    array[3] = bytes[4];
                    array[4] = bytes[3];
                    array[5] = bytes[2];
                    array[6] = bytes[1];
                    array[7] = bytes[0];
                    break;
                case DataFormat.CDAB:
                    array[0] = bytes[1];
                    array[1] = bytes[0];
                    array[2] = bytes[3];
                    array[3] = bytes[2];
                    array[4] = bytes[5];
                    array[5] = bytes[4];
                    array[6] = bytes[7];
                    array[7] = bytes[6];
                    break;
                case DataFormat.BADC:
                    array[0] = bytes[6];
                    array[1] = bytes[7];
                    array[2] = bytes[4];
                    array[3] = bytes[5];
                    array[4] = bytes[2];
                    array[5] = bytes[3];
                    array[6] = bytes[0];
                    array[7] = bytes[1];
                    break;
                case DataFormat.DCBA:
                    array = bytes;
                    break;
            }

            return array;
        }

        [Description("将Short数组转换成字节数组")]
        public static byte[] GetByteArrayFromShortArray(short[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (short value2 in value)
            {
                byteArray.Add(GetByteArrayFromShort(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将UShort数组转换成字节数组")]
        public static byte[] GetByteArrayFromUShortArray(ushort[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (ushort value2 in value)
            {
                byteArray.Add(GetByteArrayFromUShort(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将Int类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromIntArray(int[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (int value2 in value)
            {
                byteArray.Add(GetByteArrayFromInt(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将UInt类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromUIntArray(uint[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (uint value2 in value)
            {
                byteArray.Add(GetByteArrayFromUInt(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将Float类型数组转成字节数组")]
        public static byte[] GetByteArrayFromFloatArray(float[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (float value2 in value)
            {
                byteArray.Add(GetByteArrayFromFloat(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将Double类型数组转成字节数组")]
        public static byte[] GetByteArrayFromDoubleArray(double[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (double value2 in value)
            {
                byteArray.Add(GetByteArrayFromDouble(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将Long类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromLongArray(long[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (long num in value)
            {
                byteArray.Add(GetByteArrayFromDouble(num, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将ULong类型数组转换成字节数组")]
        public static byte[] GetByteArrayFromULongArray(ulong[] value, DataFormat dataFormat = DataFormat.ABCD)
        {
            ByteArray byteArray = new ByteArray();
            foreach (ulong value2 in value)
            {
                byteArray.Add(GetByteArrayFromULong(value2, dataFormat));
            }

            return byteArray.array;
        }

        [Description("将指定编码格式的字符串转换成字节数组")]
        public static byte[] GetByteArrayFromString(string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        [Description("将16进制字符串按照空格分隔成字节数组")]
        public static byte[] GetByteArrayFromHexString(string value, string spilt = " ")
        {
            value = value.Trim();
            List<byte> list = new List<byte>();
            try
            {
                if (value.Contains(spilt))
                {
                    string[] array = value.Split(new string[1] { spilt }, StringSplitOptions.RemoveEmptyEntries);
                    string[] array2 = array;
                    foreach (string text in array2)
                    {
                        list.Add(Convert.ToByte(text.Trim(), 16));
                    }
                }
                else
                {
                    list.Add(Convert.ToByte(value.Trim(), 16));
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        [Description("将16进制字符串不用分隔符转换成字节数组（每2个字符为1个字节）")]
        public static byte[] GetByteArrayFromHexStringWithoutSpilt(string value)
        {
            if (value.Length % 2 != 0)
            {
                throw new ArgumentNullException("检查字符串长度是否为偶数");
            }

            List<byte> list = new List<byte>();
            try
            {
                for (int i = 0; i < value.Length; i += 2)
                {
                    string value2 = value.Substring(i, 2);
                    list.Add(Convert.ToByte(value2, 16));
                }

                return list.ToArray();
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("数据转换失败：" + ex.Message);
            }
        }

        [Description("将byte数据转换成一个Asii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(byte value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X2"));
        }

        [Description("将short数据转换成一个Ascii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(short value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        [Description("将ushort数据转换成一个Ascii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(ushort value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        [Description("将string数据转换成一个Ascii格式字节数组")]
        public static byte[] GetAsciiByteArrayFromValue(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        [Description("将布尔数组转换成字节数组")]
        public static byte[] GetByteArrayFromBoolArray(bool[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("检查数组长度是否正确");
            }

            byte[] array = new byte[(data.Length % 8 != 0) ? (data.Length / 8 + 1) : (data.Length / 8)];
            for (int i = 0; i < array.Length; i++)
            {
                int num = ((data.Length < 8 * (i + 1)) ? (data.Length - 8 * i) : 8);
                for (int j = 0; j < num; j++)
                {
                    array[i] = ByteLib.SetbitValue(array[i], j, data[8 * i + j]);
                }
            }

            return array;
        }

        [Description("将西门子字符串转换成字节数组")]
        public static byte[] GetByteArrayFromSiemensString(string value)
        {
            byte[] byteArrayFromString = GetByteArrayFromString(value, Encoding.GetEncoding("GBK"));
            byte[] array = new byte[byteArrayFromString.Length + 2];
            array[0] = (byte)(byteArrayFromString.Length + 2);
            array[1] = (byte)byteArrayFromString.Length;
            Array.Copy(byteArrayFromString, 0, array, 2, byteArrayFromString.Length);
            return array;
        }

        [Description("将欧姆龙CIP字符串转换成字节数组")]
        public static byte[] GetByteArrayFromOmronCIPString(string data)
        {
            byte[] byteArrayFromString = GetByteArrayFromString(data, Encoding.ASCII);
            byte[] evenByteArray = GetEvenByteArray(byteArrayFromString);
            byte[] array = new byte[evenByteArray.Length + 2];
            array[0] = BitConverter.GetBytes(array.Length - 2)[0];
            array[1] = BitConverter.GetBytes(array.Length - 2)[1];
            Array.Copy(evenByteArray, 0, array, 2, evenByteArray.Length);
            return array;
        }

        [Description("扩展为偶数长度字节数组")]
        public static byte[] GetEvenByteArray(byte[] data)
        {
            if (data == null)
            {
                return new byte[0];
            }

            if (data.Length % 2 != 0)
            {
                return GetFixedLengthByteArray(data, data.Length + 1);
            }

            return data;
        }

        [Description("扩展或压缩字节数组到指定数量")]
        public static byte[] GetFixedLengthByteArray(byte[] data, int length)
        {
            if (data == null)
            {
                return new byte[length];
            }

            if (data.Length == length)
            {
                return data;
            }

            byte[] array = new byte[length];
            Array.Copy(data, array, Math.Min(data.Length, array.Length));
            return array;
        }

        [Description("将字节数组转换成Ascii字节数组")]
        public static byte[] GetAsciiBytesFromByteArray(byte[] value, string segment = "")
        {
            return Encoding.ASCII.GetBytes(StringLib.GetHexStringFromByteArray(value, segment));
        }

        [Description("将Ascii字节数组转换成字节数组")]
        public static byte[] GetBytesArrayFromAsciiByteArray(byte[] value)
        {
            return GetByteArrayFromHexStringWithoutSpilt(Encoding.ASCII.GetString(value));
        }

        [Description("将2个字节数组进行合并")]
        public static byte[] GetByteArrayFromTwoByteArray(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == null && bytes2 == null)
            {
                return null;
            }

            if (bytes1 == null)
            {
                return bytes2;
            }

            if (bytes2 == null)
            {
                return bytes1;
            }

            byte[] array = new byte[bytes1.Length + bytes2.Length];
            bytes1.CopyTo(array, 0);
            bytes2.CopyTo(array, bytes1.Length);
            return array;
        }

        [Description("将3个字节数组进行合并")]
        public static byte[] GetByteArrayFromThreeByteArray(byte[] bytes1, byte[] bytes2, byte[] bytes3)
        {
            return GetByteArrayFromTwoByteArray(GetByteArrayFromTwoByteArray(bytes1, bytes2), bytes3);
        }

        [Description("将字节数组中的某个数据修改")]
        public static byte[] SetByteArray(byte[] sourceArray, object value, int start, int offset)
        {
            string name = value.GetType().Name;
            byte[] array = null;
            switch (name.ToLower())
            {
                case "boolean":
                    Array.Copy(GetByteArrayFromByte(ByteLib.SetbitValue(sourceArray[start], offset, Convert.ToBoolean(value))), 0, sourceArray, start, 1);
                    break;
                case "byte":
                    Array.Copy(GetByteArrayFromByte(Convert.ToByte(value)), 0, sourceArray, start, 1);
                    break;
                case "int16":
                    Array.Copy(GetByteArrayFromShort(Convert.ToInt16(value)), 0, sourceArray, start, 2);
                    break;
                case "uint16":
                    Array.Copy(GetByteArrayFromUShort(Convert.ToUInt16(value)), 0, sourceArray, start, 2);
                    break;
                case "int32":
                    Array.Copy(GetByteArrayFromInt(Convert.ToInt32(value)), 0, sourceArray, start, 4);
                    break;
                case "uint32":
                    Array.Copy(GetByteArrayFromUInt(Convert.ToUInt32(value)), 0, sourceArray, start, 4);
                    break;
                case "single":
                    Array.Copy(GetByteArrayFromFloat(Convert.ToSingle(value)), 0, sourceArray, start, 4);
                    break;
                case "double":
                    Array.Copy(GetByteArrayFromDouble(Convert.ToDouble(value)), 0, sourceArray, start, 8);
                    break;
                case "int64":
                    Array.Copy(GetByteArrayFromLong(Convert.ToInt64(value)), 0, sourceArray, start, 8);
                    break;
                case "uint64":
                    Array.Copy(GetByteArrayFromULong(Convert.ToUInt64(value)), 0, sourceArray, start, 8);
                    break;
                case "byte[]":
                    array = GetByteArrayFromHexString(value.ToString());
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "int16[]":
                    array = GetByteArrayFromShortArray(ShortLib.GetShortArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "uint16[]":
                    array = GetByteArrayFromUShortArray(UShortLib.GetUShortArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "int32[]":
                    array = GetByteArrayFromIntArray(IntLib.GetIntArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "uint32[]":
                    array = GetByteArrayFromUIntArray(UIntLib.GetUIntArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "single[]":
                    array = GetByteArrayFromFloatArray(FloatLib.GetFloatArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "double[]":
                    array = GetByteArrayFromDoubleArray(DoubleLib.GetDoubleArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "int64[]":
                    array = GetByteArrayFromLongArray(LongLib.GetLongArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
                case "uint64[]":
                    array = GetByteArrayFromULongArray(ULongLib.GetULongArrayFromString(value.ToString()));
                    Array.Copy(array, 0, sourceArray, start, array.Length);
                    break;
            }

            return sourceArray;
        }
    }
}
