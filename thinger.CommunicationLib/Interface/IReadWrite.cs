using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DataConvertLib;

namespace Base.CommunicationLib.Interface
{
    /// <summary>
    /// 读写的接口
    /// </summary>
    public interface IReadWrite
    {
        /// <summary>
        /// 读取布尔数组
        /// </summary>
        /// <param name="address">设备地址</param>
        /// <param name="length">长度</param>
        /// <returns>返回操作结果，数据为bool[]</returns>
        OperateResult<bool[]> ReadBoolArray(string address, ushort length);

        /// <summary>
        /// 读取字节数组
        /// </summary>
        /// <param name="address">设备地址</param>
        /// <param name="length">长度</param>
        /// <returns>返回操作结果，数据为byte[]</returns>
        OperateResult<byte[]> ReadByteArray(string address, ushort length);


        /// <summary>
        /// 写入布尔数组
        /// </summary>
        /// <param name="address">设备地址</param>
        /// <param name="value">写入布尔数组数据</param>
        /// <returns>返回操作值</returns>
        OperateResult WriteBoolArray(string address, bool[] value);

        /// <summary>
        /// 写入字节数组
        /// </summary>
        /// <param name="address">设备地址</param>
        /// <param name="value">写入字节数组数据</param>
        /// <returns>返回操作值</returns>
        OperateResult WriteByteArray(string address, byte[] value);


    }
}
