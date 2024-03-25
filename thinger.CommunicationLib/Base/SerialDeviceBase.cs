using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;
using Base.DataConvertLib;
using Base.CommunicationLib.Interface;
using System.Threading;
using System.IO;
using System;

namespace Base.CommunicationLib.Base
{
    public class SerialDeviceBase : ReadWriteBase
    {

        /// <summary>
        /// 串口通信对象
        /// </summary>
        private SerialPort serialPort { get; set; } = new SerialPort();

        /// <summary>
        /// 读取超时时间
        /// </summary>
        public int ReadTimeOut { get; set; } = 2000;

        /// <summary>
        /// 写入超时时间
        /// </summary>
        public int WriteTimeOut { get; set; } = 2000;

        /// <summary>
        /// DtrEnable
        /// </summary>
        public bool DtrEnable { get; set; } = false;

        /// <summary>
        /// RtsEnable
        /// </summary>
        public bool RtsEnable { get; set; } = false;

        /// <summary>
        /// 锁对象
        /// </summary>
        public SimpleHybirdLock simpleHybirdLock { get; set; } = new SimpleHybirdLock();

        /// <summary>
        /// 超时时间
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 2000;

        /// <summary>
        /// 延时时间
        /// </summary>
        public int SleepTime { get; set; } = 20;

        /// <summary>
        /// 请求报文（存储）
        /// </summary>
        public byte[] Request { get; set; }

        /// <summary>
        /// 返回报文（存储）
        /// </summary>
        public byte[] Response { get; set; }

        /// <summary>
        /// 串口缓冲区大小设置
        /// </summary>
        private const int SerialBufferSize = 1024;

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="dataBits"></param>
        /// <param name="parity"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public bool Connect(string portName, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }

            //设置串口属性
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.DataBits = dataBits;
            serialPort.Parity = parity;
            serialPort.StopBits = stopBits;

            serialPort.ReadTimeout = ReadTimeOut;
            serialPort.WriteTimeout = WriteTimeOut;
            serialPort.RtsEnable = RtsEnable;
            serialPort.DtrEnable = DtrEnable;

            try
            {
                serialPort.Open();
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
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }


        /// <summary>
        /// 发送并接收
        /// </summary>
        /// <param name="request">请求报文</param>
        /// <param name="response">接收报文</param>
        /// <param name="message">消息对象</param>
        /// <returns>操作结果</returns>
        public OperateResult SendAndReceive(byte[] request, ref byte[] response, IMessage message = null)
        {
            //加锁
            simpleHybirdLock.Enter();

            //请求报文赋值
            Request = request;

            //定义一个内存
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                //发送报文
                serialPort.Write(request, 0, request.Length);

                //按照时间接收
                if (message == null)
                {
                    DateTime start = DateTime.Now;

                    byte[] buffer = new byte[1024];

                    while (true)
                    {
                        Thread.Sleep(SleepTime);

                        if (serialPort.BytesToRead > 0)
                        {
                            int count = serialPort.Read(buffer, 0, buffer.Length);

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
                //跟SerialBufferSize对比，取较小值
                int count = Math.Min(length - numBytesRead, SerialBufferSize);

                int readCount = serialPort.Read(buffer, numBytesRead, count);

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
