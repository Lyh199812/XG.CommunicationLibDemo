using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.CommunicationLib.Interface;
using Base.DataConvertLib;

namespace Base.CommunicationLib.Base
{
    public class NetDeviceBase : ReadWriteBase
    {
        //创建一个Socket对象
        private Socket socket;

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectTimeOut { get; set; } = 2000;

        /// <summary>
        /// 读取超时时间
        /// </summary>
        public int ReadTimeOut { get; set; } = 2000;

        /// <summary>
        /// 写入超时时间
        /// </summary>
        public int WriteTimeOut { get; set; } = 2000;

        /// <summary>
        /// 锁对象
        /// </summary>
        public SimpleHybirdLock simpleHybirdLock { get; set; } = new SimpleHybirdLock();

        /// <summary>
        /// 请求报文（存储）
        /// </summary>
        public byte[] Request { get; set; }

        /// <summary>
        /// 返回报文（存储）
        /// </summary>
        public byte[] Response { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 2000;

        /// <summary>
        /// 延时时间
        /// </summary>
        public int SleepTime { get; set; } = 5;

        /// <summary>
        /// 以太网缓冲区大小设置
        /// </summary>
        private const int SocketBufferSize = 8192;

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="iporhost"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Connect(string iporhost, int port)
        {
            //实例化Socket对象
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //设置超时时间
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, this.ReadTimeOut);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, this.WriteTimeOut);

            try
            {
                IAsyncResult asyncResult = socket.BeginConnect(iporhost, port, null, null);

                bool connectResult = asyncResult.AsyncWaitHandle.WaitOne(ConnectTimeOut, false);

                if (connectResult == false)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (socket != null)
            {
                socket.Close();            
            }       
        }

        /// <summary>
        /// 发送并接收
        /// </summary>
        /// <param name="request">字节数组</param>
        /// <param name="response">返回报文</param>
        /// <param name="message">IMessage对象</param>
        /// <returns>是否成功</returns>
        public OperateResult SendAndReceive(byte[] request, ref byte[] response, IMessage message = null)
        {
            simpleHybirdLock.Enter();

            //请求报文赋值
            Request = request;

            //定义一个内存
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                //发送报文
                socket.Send(request,  request.Length,SocketFlags.None);

                //按照时间接收
                if (message == null)
                {
                    DateTime start = DateTime.Now;

                    byte[] buffer = new byte[1024];

                    while (true)
                    {
                        Thread.Sleep(SleepTime);

                        if (socket.Available > 0)
                        {
                            int count = socket.Receive(buffer,  SocketFlags.None);

                            memoryStream.Write(buffer, 0, count);
                        }
                        else
                        {
                            //判断超时
                            if ((DateTime.Now - start).TotalMilliseconds > this.ReceiveTimeOut)
                            {
                                memoryStream.Dispose();
                                return OperateResult.CreateFailResult("读取超时");
                            }
                            else if (memoryStream.Length > 0)
                            {
                                break;
                            }
                        }
                    }
                }
                //按照消息接收
                else
                {
                    if (message.HeadDataLength > 0)
                    {
                        byte[] headData = ReceiveMessage(message.HeadDataLength);

                        if (message.CheckHeadData(headData) == false)
                        {
                            return OperateResult.CreateFailResult("包头报文验证不通过");
                        }
                        else
                        {
                            memoryStream.Write(headData, 0, headData.Length);
                        }
                    }

                    int contentLength = message.GetContentLength();

                    byte[] contentData = ReceiveMessage(contentLength);

                    memoryStream.Write(contentData, 0, contentData.Length);
                }

                //获取接收报文
                response = memoryStream.ToArray();

                Response = response;

                memoryStream.Dispose();

                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult(ex.Message);
            }
            finally
            {
                //解锁
                simpleHybirdLock.Leave();
            }
        }

        /// <summary>
        /// 从缓冲区读取指定的长度
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] ReceiveMessage(int length)
        {
            byte[] buffer = new byte[length];

            //已经读取的长度
            int numBytesRead = 0;

            while (numBytesRead < length)
            {
                //跟SocketBufferSize对比，取较小值
                int count = Math.Min(length - numBytesRead, SocketBufferSize);

                int readCount = socket.Receive(buffer, numBytesRead, count,SocketFlags.None);

                numBytesRead += readCount;

                if (readCount == 0)
                {
                    throw new Exception("无法读取到数据");
                }
            }
            return buffer;
        }

    }
}
