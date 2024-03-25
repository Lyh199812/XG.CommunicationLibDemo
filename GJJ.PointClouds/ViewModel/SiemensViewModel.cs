using Base.DataConvertLib;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.CommunicationLib;
using Base.Common.Config;
using Base.Common;

namespace Base.UI.ViewModel
{
    public class SiemensViewModel:BindableBase
    {
        public SiemensViewModel() 
        {
  
            CPUType = Enum.GetNames(typeof(S7.Net.CpuType)).ToList();
            SiemensDataType= Enum.GetNames(typeof(DataType)).ToList();
            Variables = CommonMethods.variables; 

            //Command
            ConnectCommand = new DelegateCommand(Connect, CanConnect);
            DisConnCommand = new DelegateCommand(DisConn, CanDisConn);
            ReadCommand = new DelegateCommand(Read, CanRead);
            WriteCommand=new DelegateCommand(Write, CanWrite);
        }
        #region 成员变量
        private SiemensS7 device =null;
        private CancellationTokenSource cts;
        static object lockObject = new object();

        #endregion

        #region 视图属性
        private bool isConnected;
        public bool IsConnected { get { return isConnected; } set { isConnected = value;RaisePropertyChanged(); } }
        
        private List<Variable> variables;
        public  List<Variable> Variables { get { return variables; } set { variables = value; RaisePropertyChanged(); } }
        private Variable curVariable;
        public Variable CurVariable { get { return curVariable; } set { curVariable = value; this.Variable = curVariable.VarAddress; CurSiemensDataType = curVariable.DataType;Count = curVariable.OffsetOrLength.ToString(); RaisePropertyChanged(); } }



        private string iPAddress;
        public string IPAddress { get { return iPAddress; } set { iPAddress = value; RaisePropertyChanged(); } }


        private string port;
        public string Port { get { return port; } set { port = value; RaisePropertyChanged(); }}

        private List<string> cPUType;
        public List<string> CPUType { get { return cPUType; } set { cPUType = value;RaisePropertyChanged(); } }

        private string curCPUType;
        public string CurCPUType { get { return curCPUType; }set { curCPUType = value; RaisePropertyChanged(); } }

        private string rackSlot;
        public string RackSlot { get {  return rackSlot; } set {  rackSlot = value; RaisePropertyChanged();} }

        private List<string> siemensDataType;
        public List <string> SiemensDataType { get {  return siemensDataType; } set {  siemensDataType = value; RaisePropertyChanged(); } }

        private string curSiemensDataType;
        public string CurSiemensDataType { get { return curSiemensDataType; } set { curSiemensDataType = value; RaisePropertyChanged(); } }

        private string variable;
        public string Variable { get { return variable; } set { variable = value; RaisePropertyChanged(); } }

        private string count;
        public string Count { get { return count; } set {  count = value; RaisePropertyChanged(); } }


        private string setValue;
        public string SetValue
        {
            get { return setValue; }
            set { setValue = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 方法
        #region 连接
        public DelegateCommand ConnectCommand { get; set; }
        private bool CanConnect()
        {
            return true;

        }

        private void Connect()
        {
            if (IsConnected)
            {
                CommonMethods.AddSysLog($"PLC已连接,请先断开", 1);
                return;
            }
            device = new SiemensS7();
            int port = Convert.ToInt32(Port);
            S7.Net.CpuType cpuType = (S7.Net.CpuType)Enum.Parse(typeof(S7.Net.CpuType), curCPUType);
            int rack = Convert.ToInt32(this.RackSlot.Split('/')[0]);
            int slot = Convert.ToInt32(this.RackSlot.Split('/')[1]);

            var result = device.Connect(this.IPAddress, port, cpuType, rack, slot);

            if (result.IsSuccess)
            {
                IsConnected = true;
                CommonMethods.AddSysLog("PLC连接成功", 0);
            }
            else
            {
                IsConnected = false;
                CommonMethods.AddSysLog("PLC连接失败：" + result.Message, 0);
            }
        }
        #endregion

        #region 断开连接
        public DelegateCommand DisConnCommand { get; set; }

        private bool CanDisConn()
        {
            return true;

           
        }
        private void DisConn()
        {
            if (!IsConnected)
            {

                CommonMethods.AddSysLog($"PLC未连接", 1);
                return;
            }
            device?.DisConnect();
            IsConnected = false;
            CommonMethods.AddSysLog("PLC断开连接", 1);
        }
        #endregion

        #region 读取

        public DelegateCommand ReadCommand { get; set; }
        private bool CanRead()
        {
            // return isConnected; 
             return true; 
        }
        private void Read()
        {
            CommonRead(Variable, Count, CurSiemensDataType, out string result,true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_variable"></param>
        /// <param name="_count"></param>
        /// <param name="_dataType"></param>
        /// <param name="result"></param>
        /// <param name="IsButton">是否是按钮触发，不是的话读取成功不需要记录</param>
        public void CommonRead(string _variable,string _count,string _dataType,out string result,bool IsButton=false)
        {
            result = null;
            if (!IsConnected)
            {
                CommonMethods.AddSysLog("PLC未连接",2);
                return;
            }
            {
                DataType dataType = (DataType)Enum.Parse(typeof(DataType), _dataType);

                switch (dataType)
                {
                    case DataType.Bool:

                        var result1 = device.ReadCommon<bool[]>(_variable, Convert.ToUInt16(_count));

                        if (result1.IsSuccess)
                        {
                            result = StringLib.GetStringFromValueArray(result1.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result1.Content), 0);
                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result1.Message, 1);
                        }

                        break;
                    case DataType.Short:

                        var result3 = device.ReadCommon<short[]>(_variable, Convert.ToUInt16(_count));

                        if (result3.IsSuccess)
                        {
                            result = StringLib.GetStringFromValueArray(result3.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result3.Content), 0);

                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result3.Message, 1);
                        }

                        break;
                    case DataType.UShort:
                        var result4 = device.ReadCommon<ushort[]>(_variable, Convert.ToUInt16(_count));

                        if (result4.IsSuccess)
                        {
                            result = StringLib.GetStringFromValueArray(result4.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result4.Content), 0);
                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result4.Message, 1);
                        }

                        break;
                    case DataType.Int:
                        var result5 = device.ReadCommon<int[]>(_variable, Convert.ToUInt16(_count));

                        if (result5.IsSuccess)
                        {
                            result = StringLib.GetStringFromValueArray(result5.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result5.Content), 0);

                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result5.Message, 0);
                        }
                        break;
                    case DataType.UInt:
                        var result6 = device.ReadCommon<uint[]>(_variable, Convert.ToUInt16(_count));

                        if (result6.IsSuccess)
                        {
                            result= StringLib.GetStringFromValueArray(result6.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result6.Content), 0);

                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result6.Message, 1);
                        }
                        break;

                    case DataType.Long:
                        var result9 = device.ReadCommon<long[]>(_variable, Convert.ToUInt16(_count));

                        if (result9.IsSuccess)
                        {
                            result = StringLib.GetStringFromValueArray(result9.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result9.Content), 0);

                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result9.Message, 1);
                        }
                        break;
                    case DataType.ULong:
                        var result10 = device.ReadCommon<ulong[]>(_variable, Convert.ToUInt16(_count));

                        if (result10.IsSuccess)
                        {
                            result = StringLib.GetStringFromValueArray(result10.Content);
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + StringLib.GetStringFromValueArray(result10.Content), 0);

                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result10.Message, 1);
                        }
                        break;
                    case DataType.String:
                        var result11 = device.ReadCommon<string>(_variable, Convert.ToUInt16(_count));

                        if (result11.IsSuccess)
                        {
                            result = result11.Content;
                            if(IsButton)
                            {
                                CommonMethods.AddSysLog($"读取{_variable}[{_count}]成功:" + result11.Content, 0);

                            }
                        }
                        else
                        {
                            CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:" + result11.Message, 1);
                        }
                        break;
                    default:
                        CommonMethods.AddSysLog($"读取{_variable}[{_count}]失败:不支持的数据类型", 1);
                        break;
                }
            }
        }
        #endregion


        #region 写入
        public DelegateCommand WriteCommand { get; set; }
        private bool CanWrite()
        {
            return true;
        }
        private void Write()
        {
            CommonWrite(Variable,  CurSiemensDataType,  SetValue);
        }

        public void CommonWrite(string _Variable, string _CurSiemensDataType,string _SetValue,bool Log=true)
        {
            DataType dataType = (DataType)Enum.Parse(typeof(DataType), _CurSiemensDataType);

            var result = Base.DataConvertLib.OperateResult.CreateFailResult();

            switch (dataType)
            {
                case DataType.Bool:
                    result = device.WriteCommon(_Variable, BitLib.GetBitArrayFromBitArrayString(_SetValue));
                    break;
                case DataType.Short:
                    result = device.WriteCommon(_Variable, ShortLib.GetShortArrayFromString(_SetValue));
                    break;
                case DataType.UShort:
                    result = device.WriteCommon(_Variable, UShortLib.GetUShortArrayFromString(_SetValue));
                    break;
                case DataType.Int:
                    result = device.WriteCommon(_Variable, IntLib.GetIntArrayFromString(_SetValue));
                    break;
                case DataType.UInt:
                    result = device.WriteCommon(_Variable, UIntLib.GetUIntArrayFromString(_SetValue));
                    break;
                case DataType.Float:
                    result = device.WriteCommon(_Variable, FloatLib.GetFloatArrayFromString(_SetValue));
                    break;
                case DataType.Double:
                    result = device.WriteCommon(_Variable, DoubleLib.GetDoubleArrayFromString(_SetValue));
                    break;
                case DataType.Long:
                    result = device.WriteCommon(_Variable, LongLib.GetLongArrayFromString(_SetValue));
                    break;
                case DataType.ULong:
                    result = device.WriteCommon(_Variable, ULongLib.GetULongArrayFromString(_SetValue));
                    break;
                case DataType.String:
                    result = device.WriteCommon(_Variable, _SetValue);
                    break;
                default:
                    break;
            }

            if (result.IsSuccess)
            {
                if(Log)
                {
                    CommonMethods.AddSysLog($"[{_Variable}:[{_SetValue}]]写入成功", 0);

                }
            }
            else
            {
                CommonMethods.AddSysLog($"[{_Variable}:[{_SetValue}]]写入失败", 1);
            }
        }
        #endregion

        #region 通信
        private void PLCCommunication()
        {
            while (!cts.IsCancellationRequested)
            {
                lock (lockObject)
                {
                    if (IsConnected)
                    {
                        foreach (var item in Variables)
                        {
                            CommonRead(item.VarAddress, item.OffsetOrLength.ToString(), item.DataType, out string result);
                            if (result == null)
                            {
                                DisConn();
                                IsConnected = false;
                                Thread.Sleep(200);
                                return;
                            }
                            else
                            {
                                if (item.CurValue != result)
                                {
                                    item.CurValue = result;
                                    switch (item.VarName)
                                    {
                                        case "PLC_HeartBeat":
                                            {
                                                if (item.CurValue == "0")
                                                {
                                                    Task.Run(() =>
                                                    {
                                                        Thread.Sleep(1000);
                                                        CommonWrite(item.VarAddress, item.DataType, "1");

                                                    });
                                                }
                                                break;
                                            }

                                        case "PLC_RFIDValue":
                                            {
                                                CommonMethods.CurCode = item.CurValue;
                                                break;
                                            }
                                        case "PLC_RFIDReadTarger":
                                            {
                                                if (item.CurValue == "1")
                                                {
                                                    CommonMethods.AddSysLog($"收到读码触发,当前条码{CommonMethods.CurCode},进行打印", 0);
                                                    CommonMethods.PrintAction.Invoke(CommonMethods.CurCode);
                                                    CommonWrite(item.VarAddress, item.DataType, "2");//复位
                                                  
                                                }
                                                break;
                                            }
                                    }
                                }
                                else
                                {

                                }
                            }

                        }
                    }
                    else
                    {
                        Thread.Sleep (3000);
                        Connect();
                    }
                }
         
            }
        }
        #endregion


        #endregion

    }
}
