using Base.Common;
using Base.DataConvertLib;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using thinger.CommunicationLib.Library;

namespace Base.UI.ViewModel
{
    public class MCSerialViewModel : BindableBase
    {
        public MCSerialViewModel() 
        {
            Init();
        }


        #region Fields
        //创建通信对象
        private MelsecFxSerial device = null;


        #endregion

        #region Property
        //创建连接正常标志位
        private bool isConnected=false;

        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }


        //Port
        private string selectedPort;

        public string SelectedPort
        {
            get { return selectedPort; }
            set { selectedPort = value; RaisePropertyChanged(); }
        }

        private List<string> port;

        public List<string> Port
        {
            get { return port; }
            set { port = value; RaisePropertyChanged(); }
        }

        //BaudRate
        private string selectedBaudRate;

        public string SelectedBaudRate
        {
            get { return selectedBaudRate; }
            set { selectedBaudRate = value; RaisePropertyChanged(); }
        }

        private List<string> baudRate;

        public List<string> BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; RaisePropertyChanged(); }
        }

        //Parity
        private string selectedParity;

        public string SelectedParity
        {
            get { return selectedParity; }
            set { selectedParity = value; RaisePropertyChanged(); }
        }

        private List<string> parity;

        public List<string> Parity
        {
            get { return parity; }
            set { parity = value; RaisePropertyChanged(); }
        }

        //DataBits
        private string dataBits;

        public string DataBits
        {
            get { return dataBits; }
            set { dataBits = value; RaisePropertyChanged(); }
        }

        //StopBits
        private string selectedStopBits;

        public string SelectedStopBits
        {
            get { return selectedStopBits; }
            set { selectedStopBits = value; RaisePropertyChanged(); }
        }

        private List<string> stopBits;

        public List<string> StopBits
        {
            get { return stopBits; }
            set { stopBits = value; RaisePropertyChanged(); }
        }


        //VariableAddress
        private string variableAddress;

        public string VariableAddress
        {
            get { return variableAddress; }
            set { variableAddress = value; }
        }
        //SelectedDataType
        private string selectedDataType;

        public string SelectedDataType
        {
            get { return selectedDataType; }
            set { selectedDataType = value; RaisePropertyChanged(); }
        }

        private List<string> commmonDataType;

        public List<string> CommonDataType
        {
            get { return commmonDataType; }
            set { commmonDataType = value; RaisePropertyChanged(); }
        }

        //SetValue
        private string setValue;

        public string SetValue
        {
            get { return setValue; }
            set { setValue = value; }
        }
        //Count
        private string count;

        public string Count
        {
            get { return count; }
            set { count = value; }
        }


        #endregion

        #region Commmand
        public DelegateCommand ConnectCommand {  get; set; }
        public DelegateCommand DisConnectCommand {  get; set; }
        
        #endregion

        #region Methods
        #region InitMethods
        public void Init()
        {
            PortInit();
            BaudRateInit();
            ParityInit();
            DataBitsInit();
            StopBitsInit();

            ConnectCommand=new DelegateCommand(Connect);
            DisConnectCommand=new DelegateCommand(DisConnect);
        }
        private void PortInit()
        {
            Port = SerialPort.GetPortNames().ToList();
        }

        private void BaudRateInit()
        {
            BaudRate = new List<string> { "2400", "4800", "9600", "19200", "38400" };

        }

        private void ParityInit()
        {
            Parity = Enum.GetNames(typeof(Parity)).ToList();
        }

        private void DataBitsInit()
        {
            DataBits = "7";

        }

        private void StopBitsInit()
        {
            StopBits = Enum.GetNames(typeof(StopBits)).ToList();
        }

        private void CommonDataTypeInit()
        {

        }
        #endregion


        private void Connect()
        {
            device = new MelsecFxSerial();

            Parity parity = (Parity)Enum.Parse(typeof(Parity), SelectedParity);
            StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), SelectedStopBits);

            int baudRate = Convert.ToInt32(SelectedBaudRate);
            int dataBits = Convert.ToInt32(DataBits);

            var result = device.Connect(SelectedPort, baudRate, dataBits, parity, stopBits);

            if (result)
            {
                isConnected = true;
                CommonMethods.AddSysLog?.Invoke( 0, "设备连接成功");
            }
            else
            {
                isConnected = false;
                CommonMethods.AddSysLog?.Invoke( 0, "设备连接失败");
            }
        }

        private void DisConnect()
        {
            if (isConnected)
            {
                device?.DisConnect();
                CommonMethods.AddSysLog?.Invoke( 0, "设备断开连接");
            }
            else
            {
                CommonMethods.AddSysLog?.Invoke(0, "设备未连接");
            }
        }


        /// <summary>
        /// 通用验证
        /// </summary>
        /// <returns></returns>
        private bool CommonValidate()
        {
            if (isConnected == false)
            {
                CommonMethods.AddSysLog( 1, "设备未连接，请检查");
                return false;
            }

            return true;
        }
        private void Read()

        {
            if (CommonValidate())
            {
                DataType dataType = (DataType)Enum.Parse(typeof(DataType), SelectedDataType);

                switch (dataType)
                {
                    case DataType.Bool:

                        //var result1 = device.ReadBoolArray(VariableAddress, Convert.ToUInt16(Count));

                        //if (result1.IsSuccess)
                        //{
                        //    CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result1.Content));
                        //}
                        //else
                        //{
                        //    CommonMethods.AddSysLog( 1, "读取失败:" + result1.Message);
                        //}

                        var result1 = device.ReadCommon<bool[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result1.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result1.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result1.Message);
                        }

                        break;
                    case DataType.Byte:
                    case DataType.SByte:
                        var result2 = device.ReadCommon<byte[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result2.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result2.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result2.Message);
                        }

                        break;
                    case DataType.Short:

                        var result3 = device.ReadCommon<short[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result3.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result3.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result3.Message);
                        }

                        break;
                    case DataType.UShort:
                        var result4 = device.ReadCommon<ushort[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result4.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result4.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result4.Message);
                        }

                        break;
                    case DataType.Int:
                        var result5 = device.ReadCommon<int[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result5.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result5.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result5.Message);
                        }
                        break;
                    case DataType.UInt:
                        var result6 = device.ReadCommon<uint[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result6.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result6.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result6.Message);
                        }
                        break;
                    case DataType.Float:
                        var result7 = device.ReadCommon<float[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result7.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result7.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result7.Message);
                        }
                        break;
                    case DataType.Double:
                        var result8 = device.ReadCommon<float[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result8.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result8.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result8.Message);
                        }
                        break;
                    case DataType.Long:
                        var result9 = device.ReadCommon<long[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result9.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result9.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result9.Message);
                        }
                        break;
                    case DataType.ULong:
                        var result10 = device.ReadCommon<ulong[]>(VariableAddress, Convert.ToUInt16(Count));

                        if (result10.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + StringLib.GetStringFromValueArray(result10.Content));
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result10.Message);
                        }
                        break;
                    case DataType.String:
                        var result11 = device.ReadCommon<string>(VariableAddress, Convert.ToUInt16(Count));

                        if (result11.IsSuccess)
                        {
                            CommonMethods.AddSysLog( 0, "读取成功:" + result11.Content);
                        }
                        else
                        {
                            CommonMethods.AddSysLog( 1, "读取失败:" + result11.Message);
                        }
                        break;
                    default:
                        CommonMethods.AddSysLog( 1, "读取失败:不支持的数据类型");
                        break;
                }

            }
        }

        private void Write()

        {
            if (CommonValidate())
            {
                DataType dataType = (DataType)Enum.Parse(typeof(DataType), SelectedDataType);

                var result = OperateResult.CreateFailResult();

                switch (dataType)
                {
                    case DataType.Bool:
                        result = device.WriteCommon(VariableAddress, BitLib.GetBitArrayFromBitArrayString(SetValue.Trim()));
                        break;
                    case DataType.Byte:
                    case DataType.SByte:
                        result = device.WriteCommon(VariableAddress, ByteArrayLib.GetByteArrayFromHexString(SetValue.Trim()));
                        break;
                    case DataType.Short:
                        result = device.WriteCommon(VariableAddress, ShortLib.GetShortArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.UShort:
                        result = device.WriteCommon(VariableAddress, UShortLib.GetUShortArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.Int:
                        result = device.WriteCommon(VariableAddress, IntLib.GetIntArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.UInt:
                        result = device.WriteCommon(VariableAddress, UIntLib.GetUIntArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.Float:
                        result = device.WriteCommon(VariableAddress, FloatLib.GetFloatArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.Double:
                        result = device.WriteCommon(VariableAddress, DoubleLib.GetDoubleArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.Long:
                        result = device.WriteCommon(VariableAddress, LongLib.GetLongArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.ULong:
                        result = device.WriteCommon(VariableAddress, ULongLib.GetULongArrayFromString(SetValue.Trim()));
                        break;
                    case DataType.String:
                        result = device.WriteCommon(VariableAddress, SetValue.Trim());
                        break;
                    default:
                        break;
                }

                if (result.IsSuccess)
                {
                    CommonMethods.AddSysLog( 0, "写入成功");
                }
                else
                {
                    CommonMethods.AddSysLog( 0, "写入失败");
                }
            }
        }
        #endregion
    }
}
