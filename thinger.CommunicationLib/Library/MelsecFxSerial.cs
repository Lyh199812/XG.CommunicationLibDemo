using Base.CommunicationLib.Base;
using Base.CommunicationLib;
using Base.DataConvertLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.CommunicationLib.StoreArea;
using thinger.CommunicationLib.Helper;

namespace thinger.CommunicationLib.Library
{
    public class MelsecFxSerial : SerialDeviceBase
    {
        private const byte STX = 2;

        private const byte ACK = 6;

        private const byte NAK = 21;

        private const byte ENQ = 5;

        private const byte ETX = 3;

        private const byte ReadCMD = 48;

        private const byte WriteCMD = 49;

        private const byte ForceON = 55;

        private const byte ForceOFF = 56;

        public MelsecFxSerial(DataFormat dataFormat = DataFormat.DCBA)
        {
            base.DataFormat = dataFormat;
        }

        public override OperateResult<byte[]> ReadByteArray(string address, ushort length)
        {
            OperateResult<byte[]> operateResult = BuildReadMessageFrameForWord(address, length);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            byte[] response = null;
            OperateResult operateResult2 = SendAndReceive(operateResult.Content, ref response);
            if (operateResult2.IsSuccess)
            {
                operateResult2 = CheckResponse(response);
                if (operateResult2.IsSuccess)
                {
                    return AnalysisResponseMessage(response);
                }

                return OperateResult.CreateFailResult<byte[]>(operateResult2);
            }

            return OperateResult.CreateFailResult<byte[]>(operateResult2);
        }

        public override OperateResult<bool[]> ReadBoolArray(string address, ushort length)
        {
            OperateResult<byte[], int> operateResult = BuildReadMessageFrameForBool(address, length);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<bool[]>(operateResult);
            }

            byte[] response = null;
            OperateResult operateResult2 = SendAndReceive(operateResult.Content1, ref response);
            if (operateResult2.IsSuccess)
            {
                operateResult2 = CheckResponse(response);
                if (operateResult2.IsSuccess)
                {
                    OperateResult<byte[]> operateResult3 = AnalysisResponseMessage(response);
                    if (operateResult3.IsSuccess)
                    {
                        return OperateResult.CreateSuccessResult(BitLib.GetBitArrayFromByteArray(operateResult3.Content, operateResult.Content2, length));
                    }

                    return OperateResult.CreateFailResult<bool[]>(operateResult3);
                }

                return OperateResult.CreateFailResult<bool[]>(operateResult2);
            }

            return OperateResult.CreateFailResult<bool[]>(operateResult2);
        }

        public override OperateResult WriteByteArray(string address, byte[] value)
        {
            OperateResult<byte[]> operateResult = BuildWriteMessageFrameForWord(address, value);
            if (!operateResult.IsSuccess)
            {
                return operateResult;
            }

            byte[] response = null;
            OperateResult operateResult2 = SendAndReceive(operateResult.Content, ref response);
            if (operateResult2.IsSuccess)
            {
                return CheckResponse(response, isRead: false);
            }

            return operateResult2;
        }

        public override OperateResult WriteBoolArray(string address, bool[] values)
        {
            if (values.Length == 1)
            {
                return ForceBool(address, values[0]);
            }

            OperateResult<byte[]> operateResult = BuidWriteMessageFrameForBool(address, values);
            if (!operateResult.IsSuccess)
            {
                return operateResult;
            }

            byte[] response = null;
            OperateResult operateResult2 = SendAndReceive(operateResult.Content, ref response);
            if (operateResult2.IsSuccess)
            {
                return CheckResponse(response, isRead: false);
            }

            return operateResult2;
        }

        public OperateResult ForceBool(string address, bool value)
        {
            OperateResult<byte[]> operateResult = BuidWriteMessageFrameForBool(address, value);
            if (!operateResult.IsSuccess)
            {
                return operateResult;
            }

            byte[] response = null;
            OperateResult operateResult2 = SendAndReceive(operateResult.Content, ref response);
            if (operateResult2.IsSuccess)
            {
                return CheckResponse(response, isRead: false);
            }

            return operateResult2;
        }

        private OperateResult<byte[]> BuildReadMessageFrameForWord(string address, ushort length)
        {
            OperateResult<ushort> operateResult = WordAddressHandle(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            ushort content = operateResult.Content;
            ByteArray byteArray = new ByteArray();
            byteArray.Add(2);
            byteArray.Add(48);
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue(content));
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue((byte)(length * 2)));
            byteArray.Add(3);
            byteArray.Add(ParityHelper.CalculateSUM(byteArray.array));
            return OperateResult.CreateSuccessResult(byteArray.array);
        }

        private OperateResult<byte[], int> BuildReadMessageFrameForBool(string address, ushort length)
        {
            OperateResult<ushort, ushort, ushort> operateResult = BoolAddressHandle(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[], int>(operateResult);
            }

            ushort num = (ushort)((length % 8 == 0) ? (length / 8) : (length / 8 + 1));
            ushort content = operateResult.Content1;
            ByteArray byteArray = new ByteArray();
            byteArray.Add(2);
            byteArray.Add(48);
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue(content));
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue((byte)num));
            byteArray.Add(3);
            byteArray.Add(ParityHelper.CalculateSUM(byteArray.array));
            return OperateResult.CreateSuccessResult(byteArray.array, (int)operateResult.Content3);
        }

        private OperateResult<byte[]> BuildWriteMessageFrameForWord(string address, byte[] value)
        {
            OperateResult<ushort> operateResult = WordAddressHandle(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            ushort num = (ushort)value.Length;
            if (value != null)
            {
                value = ByteArrayLib.GetAsciiBytesFromByteArray(value);
            }

            ushort content = operateResult.Content;
            ByteArray byteArray = new ByteArray();
            byteArray.Add(2);
            byteArray.Add(49);
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue(content));
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue((byte)num));
            byteArray.Add(value);
            byteArray.Add(3);
            byteArray.Add(ParityHelper.CalculateSUM(byteArray.array));
            return OperateResult.CreateSuccessResult(byteArray.array);
        }

        private OperateResult<byte[]> BuidWriteMessageFrameForBool(string address, bool value)
        {
            OperateResult<ushort> operateResult = BoolForceAddressHandle(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            ushort content = operateResult.Content;
            ByteArray byteArray = new ByteArray();
            byteArray.Add(2);
            byteArray.Add((byte)(value ? 55 : 56));
            byte[] asciiByteArrayFromValue = ByteArrayLib.GetAsciiByteArrayFromValue(content);
            byteArray.Add(asciiByteArrayFromValue[2]);
            byteArray.Add(asciiByteArrayFromValue[3]);
            byteArray.Add(asciiByteArrayFromValue[0]);
            byteArray.Add(asciiByteArrayFromValue[1]);
            byteArray.Add(3);
            byteArray.Add(ParityHelper.CalculateSUM(byteArray.array));
            return OperateResult.CreateSuccessResult(byteArray.array);
        }

        private OperateResult<byte[]> BuidWriteMessageFrameForBool(string address, bool[] value)
        {
            OperateResult<ushort, ushort, ushort> operateResult = BoolAddressHandle(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<byte[]>(operateResult);
            }

            ushort content = operateResult.Content1;
            ushort num = (ushort)((value.Length % 8 == 0) ? (value.Length / 8) : (value.Length / 8 + 1));
            ByteArray byteArray = new ByteArray();
            byteArray.Add(2);
            byteArray.Add(49);
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue(content));
            byteArray.Add(ByteArrayLib.GetAsciiByteArrayFromValue((byte)num));
            byteArray.Add(ByteArrayLib.GetAsciiBytesFromByteArray(ByteArrayLib.GetByteArrayFromBoolArray(value)));
            byteArray.Add(3);
            byteArray.Add(ParityHelper.CalculateSUM(byteArray.array));
            return OperateResult.CreateSuccessResult(byteArray.array);
        }

        private OperateResult<ushort> WordAddressHandle(string address)
        {
            OperateResult<MelsecStoreArea, ushort> operateResult = MelsecHelper.MelsecFXAnalysisAddress(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<ushort>(operateResult);
            }

            ushort content = operateResult.Content2;
            if (operateResult.Content1 != MelsecStoreArea.D)
            {
                return OperateResult.CreateFailResult<ushort>(new OperateResult("当前的类型不支持字读写"));
            }

            content = ((operateResult.Content2 < 8000) ? ((ushort)(content * 2 + 4096)) : ((ushort)((content - 8000) * 2 + 3584)));
            return OperateResult.CreateSuccessResult(content);
        }

        private OperateResult<ushort, ushort, ushort> BoolAddressHandle(string address)
        {
            OperateResult<MelsecStoreArea, ushort> operateResult = MelsecHelper.MelsecFXAnalysisAddress(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<ushort, ushort, ushort>(operateResult);
            }

            ushort content = operateResult.Content2;
            if (operateResult.Content1 == MelsecStoreArea.M)
            {
                content = ((content < 8000) ? ((ushort)(content / 8 + 256)) : ((ushort)((content - 8000) / 8 + 480)));
            }
            else if (operateResult.Content1 == MelsecStoreArea.X)
            {
                content = (ushort)(content / 8 + 128);
            }
            else
            {
                if (operateResult.Content1 != MelsecStoreArea.Y)
                {
                    return OperateResult.CreateFailResult<ushort, ushort, ushort>(new OperateResult("当前的类型不支持字读写"));
                }

                content = (ushort)(content / 8 + 160);
            }

            return OperateResult.CreateSuccessResult(content, operateResult.Content2, (ushort)(operateResult.Content2 % 8));
        }

        private OperateResult<ushort> BoolForceAddressHandle(string address)
        {
            OperateResult<MelsecStoreArea, ushort> operateResult = MelsecHelper.MelsecFXAnalysisAddress(address);
            if (!operateResult.IsSuccess)
            {
                return OperateResult.CreateFailResult<ushort>(operateResult);
            }

            ushort content = operateResult.Content2;
            if (operateResult.Content1 == MelsecStoreArea.M)
            {
                content = ((content < 8000) ? ((ushort)(content + 2048)) : ((ushort)(content - 8000 + 3840)));
            }
            else if (operateResult.Content1 == MelsecStoreArea.X)
            {
                content += 1024;
            }
            else
            {
                if (operateResult.Content1 != MelsecStoreArea.Y)
                {
                    return OperateResult.CreateFailResult<ushort>(new OperateResult("当前的类型不支持布尔强制"));
                }

                content += 1280;
            }

            return OperateResult.CreateSuccessResult(content);
        }

        private OperateResult CheckResponse(byte[] response, bool isRead = true)
        {
            if (response == null || response.Length == 0)
            {
                return new OperateResult(isSuccess: false, "返回报文为空");
            }

            if (isRead)
            {
                if (response[0] == 21)
                {
                    return new OperateResult(isSuccess: false, $"返回报文首字节为{(byte)21}");
                }

                if (response[0] != 2)
                {
                    return new OperateResult(isSuccess: false, $"返回报文首字节不为{(byte)2}");
                }

                if (!ParityHelper.CheckSUM(response))
                {
                    return new OperateResult(isSuccess: false, "返回报文校验不通过");
                }

                return OperateResult.CreateSuccessResult();
            }

            if (response[0] != 6)
            {
                return new OperateResult(isSuccess: false, $"写入失败，返回不为{(byte)6}");
            }

            return OperateResult.CreateSuccessResult();
        }

        private OperateResult<byte[]> AnalysisResponseMessage(byte[] response)
        {
            try
            {
                byte[] array = new byte[(response.Length - 4) / 2];
                for (int i = 0; i < array.Length; i++)
                {
                    byte[] bytes = new byte[2]
                    {
                    response[i * 2 + 1],
                    response[i * 2 + 2]
                    };
                    array[i] = Convert.ToByte(Encoding.ASCII.GetString(bytes), 16);
                }

                return OperateResult.CreateSuccessResult(array);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<byte[]>(new OperateResult("解析出错：" + ex.Message));
            }
        }
    }
}
