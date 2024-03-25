using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HalconDotNet;
using System.Security.Cryptography;
using Base.Common;
using Base.Common.Extensions;

namespace Base.UI.ViewModel
{
    public class TCPClientViewModel : BindableBase
    {
        public TCPClientViewModel() 
        {
            ConnectionCommand = new DelegateCommand(Connect);
            SendCommand = new DelegateCommand(SendMeg);
            AddLog = CommonMethods.AddSysLog;
        }
        #region 成员变量
        //1、创建一个Socket
        Socket socketClient = null;
        //2、创建一个线程处理从服务端接收到的数据
        Thread thrClient = null;
        const int timeout = 1500; // 设置连接超时时间为2秒
        CancellationTokenSource cts_conn = new CancellationTokenSource(timeout);
        #endregion


        #region 视图属性
        private bool isConnect;
        public bool IsConnect { get { return isConnect; } set { isConnect = value; RaisePropertyChanged(); } }
        private string communicationRecord;
        public string CommunicationRecord { get { return communicationRecord; } set { communicationRecord = value; RaisePropertyChanged(); } }

        private string serverIPAddress;
        public string ServerIPAddress { get { return serverIPAddress; } set { serverIPAddress = value; RaisePropertyChanged(); } }

        private string port;
        public string Port { get { return port; } set { port = value; RaisePropertyChanged(); } }

        private string clientName;
        public string ClientName { get { return clientName; } set { clientName = value; RaisePropertyChanged(); } }

        private string sendMsg;
        public string SendMsg { get { return sendMsg; } set { sendMsg = value; RaisePropertyChanged(); } }
        #endregion


        #region 方法
        public Action<string, int> AddLog;
        #region 连接
        public DelegateCommand ConnectionCommand { get; set; }
        public async void Connect()
        {
            IPAddress address = IPAddress.Parse(ServerIPAddress);
            IPEndPoint ipe = new IPEndPoint(address, int.Parse(Port));
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
            try
            {
                AddLog?.Invoke($"{ServerIPAddress}:{Port} 与服务器连接中.......", 0);
                await socketClient.ConnectAsync(ipe).WithCancellation(cts_conn.Token); ;//WithCancellation
                AddLog?.Invoke($"{ServerIPAddress}:{Port} 连接成功", 0);
                IsConnect = true;
                thrClient = new Thread(ReceiceMeg);
                thrClient.IsBackground = true;
                thrClient.Start();
            }
            catch (Exception ex)
            {
                AddLog?.Invoke($"{ServerIPAddress}:{Port} 连接失败" + ex.Message, 1);
            }

        }

       

        #endregion
        private void ReceiceMeg()
        {
            const int MaxRecordLength = 10 * 1024 * 1024; // 设置最大长度为 10MB

            while (IsConnect)
            {
                byte[] arrMsgRec = new byte[1024]; // 每次接收 1024 字节的数据
                int totalLength = 0;
                int bytesRead = 0;

                do
                {
                    bytesRead = socketClient.Receive(arrMsgRec, 0, arrMsgRec.Length, SocketFlags.None);
                    if (bytesRead > 0)
                    {
                        totalLength += bytesRead;
                        string strMsg = Encoding.UTF8.GetString(arrMsgRec, 0, bytesRead);

                        string Msg = "[接收] " + strMsg + " " + DateTime.Now.ToString() + Environment.NewLine;
                        (new Action(() => CommunicationRecord += Msg)).Invoke();

                        if (CommunicationRecord.Length > MaxRecordLength)
                        {
                            CommunicationRecord = Msg; // 清空 CommunicationRecord
                        }
                    }
                } while (bytesRead > 0 && totalLength < 1024 * 1024 * 2); // 继续接收直到达到总长度限制或没有数据可读
            }
        }

        public DelegateCommand SendCommand { get; set; }

        private void SendMeg()
        {
            SendMsg= SendMsg.Replace("\\r\\n","\r\n");
            CommonSendMeg(SendMsg);
        }
        public void CommonSendMeg(string Code)
        {
            byte[] arrMsg = Encoding.UTF8.GetBytes( Code);
            socketClient.Send(arrMsg);
            AddLog?.Invoke($"{ServerIPAddress}:{Port} 发送：{Code}", 0);
            string Msg = "[发送] " + Code + " " + DateTime.Now.ToString() + Environment.NewLine;
            (new Action(() => CommunicationRecord += Msg)).Invoke();
        }
        #endregion
       
    }
}
