











using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.CommunicationLib.Interface;
using Base.DataConvertLib;

namespace Base.CommunicationLib.Message
{
    public class ModbusTCPMessage : IMessage
    {
        /// <summary>
        /// 包头长度
        /// </summary>
        public int HeadDataLength { get; set; } = 0;
        public byte[] HeadData { get; set; }
        public byte[] ContentData { get; set; }
        public byte[] SendData { get; set; }

        /// <summary>
        /// 数据点数量
        /// </summary>
        public int NumberOfPoints { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        public FunctionCode FunctionCode { get; set; }


        public bool CheckHeadData(byte[] headData)
        {
            return true;
        }

        public int GetContentLength()
        {
            switch (FunctionCode)
            {
                case FunctionCode.ReadOutputStatus:
                case FunctionCode.ReadInputStatus:
                    return IntLib.GetByteLengthFromBoolLength(NumberOfPoints) + 9;
                case FunctionCode.ReadOutputRegister:
                case FunctionCode.ReadInputRegister:
                    return NumberOfPoints * 2 + 9;
                case FunctionCode.ForceCoil:
                case FunctionCode.PreSetRegister:
                case FunctionCode.ForceMultiCoils:
                case FunctionCode.PreSetMultiRegisters:
                    return 12;
                default:
                    return 0;
            }
        }
    }
}
