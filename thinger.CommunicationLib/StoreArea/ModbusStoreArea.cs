using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.CommunicationLib.StoreArea
{
    public  class ModbusStoreArea
    {
        public ModbusStoreArea(FunctionCode readFunctionCode,FunctionCode writeFunctionCode)
        {
            this.ReadFunctionCode = readFunctionCode;
            this.WriteFunctionCode = writeFunctionCode;
        }


        /// <summary>
        /// 读取功能码
        /// </summary>
        public FunctionCode ReadFunctionCode { get; set; }

        /// <summary>
        /// 写入功能码
        /// </summary>
        public FunctionCode WriteFunctionCode  { get; set; }

        /// <summary>
        /// 0区输出线圈
        /// </summary>
        public readonly static ModbusStoreArea X0 = new ModbusStoreArea(FunctionCode.ReadOutputStatus, FunctionCode.ForceMultiCoils);

        /// <summary>
        /// 1区输出线圈
        /// </summary>
        public readonly static ModbusStoreArea X1 = new ModbusStoreArea(FunctionCode.ReadInputStatus, 0x00);

        /// <summary>
        /// 3区输入寄存器
        /// </summary>
        public readonly static ModbusStoreArea X3 = new ModbusStoreArea(FunctionCode.ReadInputRegister, 0x00);

        /// <summary>
        /// 4区输出寄存器
        /// </summary>
        public readonly static ModbusStoreArea X4 = new ModbusStoreArea(FunctionCode.ReadOutputRegister, FunctionCode.PreSetMultiRegisters);

    }
}
