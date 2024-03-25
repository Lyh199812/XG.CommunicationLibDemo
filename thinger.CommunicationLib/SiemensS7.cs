using Base.DataConvertLib;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.CommunicationLib.Base;
using Base.CommunicationLib;
using Base.CommunicationLib.StoreArea;

namespace Base.CommunicationLib
{
    public class SiemensS7 : NetDeviceBase
    {
        private Plc siemens;

        public SiemensS7()
        {
            base.DataFormat = DataFormat.ABCD;
            AreaType = AreaType.Byte;
        }

        public OperateResult Connect(string ip, int port, CpuType cpuType, int rack, int slot)
        {
            siemens = new Plc(cpuType, ip, port, (short)rack, (short)slot);
            try
            {
                siemens.Open();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return new OperateResult(isSuccess: false, ex.Message);
            }
        }

        public  void DisConnect()
        {
            siemens?.Close();
        }

        public override OperateResult<byte[]> ReadByteArray(string address, ushort length)
        {
            OperateResult<SiemensStoreArea, int> operateResult = SiemensHelper.SiemensAddressAnalysis(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            try
            {
                byte[] value = siemens.ReadBytes(operateResult.Content1.DataType, operateResult.Content1.DBNo, operateResult.Content2, length);
                return OperateResult.CreateSuccessResult(value);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>(ex.Message);
            }
        }

        public override OperateResult<bool[]> ReadBoolArray(string address, ushort length)
        {
            if (address.Contains('.'))
            {
                string empty = string.Empty;
                int num = 0;
                string[] array = address.Split('.');
                if (array.Length == 2)
                {
                    empty = array[0];
                    num = Convert.ToInt32(array[1]);
                }
                else
                {
                    if (array.Length != 3)
                    {
                        return OperateResult.CreateFailResult<bool[]>("变量地址格式不正确：" + address);
                    }

                    empty = array[0] + "." + array[1];
                    num = Convert.ToInt32(array[2]);
                }

                ushort length2 = (ushort)IntLib.GetByteLengthFromBoolLength(num + length);
                OperateResult<byte[]> operateResult = ReadByteArray(empty, length2);
                if (operateResult.IsSuccess)
                {
                    return OperateResult.CreateSuccessResult(BitLib.GetBitArrayFromByteArray(operateResult.Content, num, length));
                }

                return OperateResult.CreateFailResult<bool[]>(operateResult);
            }

            return OperateResult.CreateFailResult<bool[]>("变量地址格式不正确：" + address);
        }

        public override OperateResult WriteBoolArray(string address, bool[] value)
        {
            if (address.Contains('.'))
            {
                string empty = string.Empty;
                int num = 0;
                string[] array = address.Split('.');
                if (array.Length == 2)
                {
                    empty = array[0];
                    num = Convert.ToInt32(array[1]);
                }
                else
                {
                    if (array.Length != 3)
                    {
                        return OperateResult.CreateFailResult<bool[]>("变量地址格式不正确：" + address);
                    }

                    empty = array[0] + "." + array[1];
                    num = Convert.ToInt32(array[2]);
                }

                OperateResult<SiemensStoreArea, int> operateResult = SiemensHelper.SiemensAddressAnalysis(empty);
                if (!operateResult.IsSuccess)
                {
                    return OperateResult.CreateFailResult<bool[]>(operateResult);
                }

                try
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        siemens.WriteBit(operateResult.Content1.DataType, operateResult.Content1.DBNo, operateResult.Content2 + (num + i) / 8, (num + i) % 8, value[i]);
                    }

                    return OperateResult.CreateSuccessResult();
                }
                catch (Exception ex)
                {
                    return OperateResult.CreateFailResult(ex.Message);
                }
            }

            return OperateResult.CreateFailResult<bool[]>("变量地址格式不正确：" + address);
        }

        public override OperateResult WriteByteArray(string address, byte[] value)
        {
            OperateResult<SiemensStoreArea, int> operateResult = SiemensHelper.SiemensAddressAnalysis(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            try
            {
                siemens.WriteBytes(operateResult.Content1.DataType, operateResult.Content1.DBNo, operateResult.Content2, value);
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
        }
    }
}
