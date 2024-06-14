using Base.DataConvertLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.CommunicationLib
{
    public class ParityHelper
    {
        private static readonly byte[] crcHigh = new byte[256]
        {
        0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
        128, 65, 0, 193, 129, 64, 1, 192, 128, 65,
        0, 193, 129, 64, 0, 193, 129, 64, 1, 192,
        128, 65, 1, 192, 128, 65, 0, 193, 129, 64,
        0, 193, 129, 64, 1, 192, 128, 65, 0, 193,
        129, 64, 1, 192, 128, 65, 1, 192, 128, 65,
        0, 193, 129, 64, 1, 192, 128, 65, 0, 193,
        129, 64, 0, 193, 129, 64, 1, 192, 128, 65,
        0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
        128, 65, 0, 193, 129, 64, 0, 193, 129, 64,
        1, 192, 128, 65, 1, 192, 128, 65, 0, 193,
        129, 64, 1, 192, 128, 65, 0, 193, 129, 64,
        0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
        128, 65, 0, 193, 129, 64, 0, 193, 129, 64,
        1, 192, 128, 65, 0, 193, 129, 64, 1, 192,
        128, 65, 1, 192, 128, 65, 0, 193, 129, 64,
        0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
        128, 65, 0, 193, 129, 64, 1, 192, 128, 65,
        0, 193, 129, 64, 0, 193, 129, 64, 1, 192,
        128, 65, 0, 193, 129, 64, 1, 192, 128, 65,
        1, 192, 128, 65, 0, 193, 129, 64, 1, 192,
        128, 65, 0, 193, 129, 64, 0, 193, 129, 64,
        1, 192, 128, 65, 1, 192, 128, 65, 0, 193,
        129, 64, 0, 193, 129, 64, 1, 192, 128, 65,
        0, 193, 129, 64, 1, 192, 128, 65, 1, 192,
        128, 65, 0, 193, 129, 64
        };

        private static readonly byte[] crcLow = new byte[256]
        {
        0, 192, 193, 1, 195, 3, 2, 194, 198, 6,
        7, 199, 5, 197, 196, 4, 204, 12, 13, 205,
        15, 207, 206, 14, 10, 202, 203, 11, 201, 9,
        8, 200, 216, 24, 25, 217, 27, 219, 218, 26,
        30, 222, 223, 31, 221, 29, 28, 220, 20, 212,
        213, 21, 215, 23, 22, 214, 210, 18, 19, 211,
        17, 209, 208, 16, 240, 48, 49, 241, 51, 243,
        242, 50, 54, 246, 247, 55, 245, 53, 52, 244,
        60, 252, 253, 61, 255, 63, 62, 254, 250, 58,
        59, 251, 57, 249, 248, 56, 40, 232, 233, 41,
        235, 43, 42, 234, 238, 46, 47, 239, 45, 237,
        236, 44, 228, 36, 37, 229, 39, 231, 230, 38,
        34, 226, 227, 35, 225, 33, 32, 224, 160, 96,
        97, 161, 99, 163, 162, 98, 102, 166, 167, 103,
        165, 101, 100, 164, 108, 172, 173, 109, 175, 111,
        110, 174, 170, 106, 107, 171, 105, 169, 168, 104,
        120, 184, 185, 121, 187, 123, 122, 186, 190, 126,
        127, 191, 125, 189, 188, 124, 180, 116, 117, 181,
        119, 183, 182, 118, 114, 178, 179, 115, 177, 113,
        112, 176, 80, 144, 145, 81, 147, 83, 82, 146,
        150, 86, 87, 151, 85, 149, 148, 84, 156, 92,
        93, 157, 95, 159, 158, 94, 90, 154, 155, 91,
        153, 89, 88, 152, 136, 72, 73, 137, 75, 139,
        138, 74, 78, 142, 143, 79, 141, 77, 76, 140,
        68, 132, 133, 69, 135, 71, 70, 134, 130, 66,
        67, 131, 65, 129, 128, 64
        };

        public static byte[] CalculateCRC(byte[] data, int length)
        {
            int num = 0;
            byte[] array = new byte[2] { 255, 255 };
            while (length-- > 0)
            {
                ushort num2 = (ushort)(array[0] ^ data[num++]);
                array[0] = (byte)(array[1] ^ crcHigh[num2]);
                array[1] = crcLow[num2];
            }

            return array;
        }

        public static bool CheckCRC(byte[] data)
        {
            if (data == null)
            {
                return false;
            }

            if (data.Length <= 2)
            {
                return false;
            }

            int num = data.Length;
            byte[] array = new byte[num - 2];
            Array.Copy(data, 0, array, 0, array.Length);
            byte[] array2 = CalculateCRC(array, array.Length);
            return array2[0] == data[num - 2] && array2[1] == data[num - 1];
        }

        public static byte[] CaculateLRC(byte[] data, int start = 0, int len = 0)
        {
            if (data == null || data.Length == 0)
            {
                return null;
            }

            if (start < 0)
            {
                return null;
            }

            if (len == 0)
            {
                len = data.Length - start;
            }

            int num = start + len;
            if (num > data.Length)
            {
                return null;
            }

            byte b = 0;
            for (int i = start; i < num; i++)
            {
                b += data[i];
            }

            b = (byte)((b ^ 0xFF) + 1);
            return new byte[1] { b };
        }

        public static bool CheckLRC(byte[] data)
        {
            if (data == null)
            {
                return false;
            }

            if (data.Length <= 1)
            {
                return false;
            }

            int num = data.Length;
            byte[] array = new byte[num - 1];
            Array.Copy(data, 0, array, 0, array.Length);
            byte[] array2 = CaculateLRC(array, 0, array.Length);
            return array2[0] == data[num - 1];
        }

        public static byte[] CalculateSUM(byte[] data)
        {
            int num = 0;
            for (int i = 1; i < data.Length; i++)
            {
                num += data[i];
            }

            return ByteArrayLib.GetAsciiByteArrayFromValue((byte)num);
        }

        public static bool CheckSUM(byte[] data)
        {
            if (data.Length <= 2)
            {
                return false;
            }

            byte[] value = CalculateSUM(ByteArrayLib.GetByteArrayFromByteArray(data, 0, data.Length - 2));
            return ByteArrayLib.GetByteArrayEquals(value, ByteArrayLib.GetByteArrayFromByteArray(data, data.Length - 2, 2));
        }

        public static string CalculateACC(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num += bytes[i];
            }

            return data + num.ToString("X4").Substring(2);
        }

        public static byte FCS(byte[] data)
        {
            int num = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                num ^= data[i];
            }

            return (byte)num;
        }
    }
}
