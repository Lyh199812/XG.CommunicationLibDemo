











using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.CommunicationLib
{
    /// <summary>
    /// 存储区最小单位
    /// </summary>
    public enum AreaType
    {
        /// <summary>
        /// 字节
        /// </summary>
        Byte = 1,
        /// <summary>
        /// 字
        /// </summary>
        Word = 2,
    }


    public enum StringType
    {
        /// <summary>
        /// 十进制字符串
        /// </summary>
        DecString,
        /// <summary>
        /// 十六进制字符串
        /// </summary>
        HexString,
        /// <summary>
        /// ASCII字符串
        /// </summary>
        ASCIIString,
        /// <summary>
        /// BitConvert字符串
        /// </summary>
        BitConvertString,
        /// <summary>
        /// 西门子字符串
        /// </summary>
        SiemensString

    }

    /// <summary>
    /// 功能码
    /// </summary>
    public enum FunctionCode
    {
        /// <summary>
        /// 读取输出线圈
        /// </summary>
        ReadOutputStatus = 0x01,

        /// <summary>
        /// 读取输入线圈
        /// </summary>
        ReadInputStatus = 0x02,

        /// <summary>
        /// 读取输出寄存器
        /// </summary>
        ReadOutputRegister = 0x03,

        /// <summary>
        /// 读取输入寄存器
        /// </summary>
        ReadInputRegister = 0x04,

        /// <summary>
        /// 写入单个线圈
        /// </summary>
        ForceCoil = 0x05,

        /// <summary>
        /// 写入单个寄存器
        /// </summary>
        PreSetRegister = 0x06,

        /// <summary>
        /// 写入多个线圈
        /// </summary>
        ForceMultiCoils = 0x0F,

        /// <summary>
        /// 写入多个寄存器
        /// </summary>
        PreSetMultiRegisters = 0x10,


    }
}
