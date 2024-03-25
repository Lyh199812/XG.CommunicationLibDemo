using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.CommunicationLib.Interface
{
    /// <summary>
    /// 消息接口
    /// </summary>
    public  interface IMessage
    {
        /// <summary>
        /// 包头长度
        /// </summary>
        int HeadDataLength { get; set; }

        /// <summary>
        /// 包头报文
        /// </summary>
        byte[] HeadData { get; set; }

        /// <summary>
        /// 数据报文
        /// </summary>
        byte[] ContentData { get; set; }

        /// <summary>
        /// 发送报文
        /// </summary>
        byte[] SendData { get; set; }


        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns>返回长度</returns>
        int GetContentLength();

        /// <summary>
        /// 验证包头是否正确
        /// </summary>
        /// <param name="headData">包头报文</param>
        /// <returns>是否正确</returns>
        bool CheckHeadData(byte[] headData);

    }
}
