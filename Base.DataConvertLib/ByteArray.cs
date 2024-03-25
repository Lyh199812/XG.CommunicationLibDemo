using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    [Description("非常好用的字节集合类")]
    public class ByteArray
    {
        private List<byte> list = new List<byte>();

        public byte this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public int Length => list.Count;

        public byte[] array => list.ToArray();

        [Description("清空字节数组")]
        public void Clear()
        {
            list = new List<byte>();
        }

        [Description("添加一个字节")]
        public void Add(byte item)
        {
            Add(new byte[1] { item });
        }

        [Description("添加一个字节数组")]
        public void Add(byte[] items)
        {
            list.AddRange(items);
        }

        [Description("添加二个字节")]
        public void Add(byte item1, byte item2)
        {
            Add(new byte[2] { item1, item2 });
        }

        [Description("添加三个字节")]
        public void Add(byte item1, byte item2, byte item3)
        {
            Add(new byte[3] { item1, item2, item3 });
        }

        [Description("添加四个字节")]
        public void Add(byte item1, byte item2, byte item3, byte item4)
        {
            Add(new byte[4] { item1, item2, item3, item4 });
        }

        [Description("添加五个字节")]
        public void Add(byte item1, byte item2, byte item3, byte item4, byte item5)
        {
            Add(new byte[5] { item1, item2, item3, item4, item5 });
        }

        [Description("添加一个ByteArray对象")]
        public void Add(ByteArray byteArray)
        {
            Add(byteArray.array);
        }

        [Description("添加一个ushort类型数值")]
        public void Add(ushort value)
        {
            list.Add((byte)(value >> 8));
            list.Add((byte)value);
        }

        [Description("添加一个short类型数值")]
        public void Add(short value)
        {
            list.Add((byte)(value >> 8));
            list.Add((byte)value);
        }
    }
}
