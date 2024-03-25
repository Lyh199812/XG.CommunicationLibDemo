using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.CommunicationLib.StoreArea;
using Base.DataConvertLib;

namespace Base.CommunicationLib
{
    public class ModbusHelper
    {

        /// <summary>
        /// Modbus地址解析方法
        /// </summary>
        /// <param name="address">Modbus地址</param>
        /// <param name="defaultDevAddress">默认站地址</param>
        /// <param name="isShortAddress">是否为短地址模型</param>
        /// <returns>返回结果</returns>
        /// <remarks>第一个值表示存储区信息，存储区信息中包含功能码</remarks>
        /// <remarks>第二个值表示站地址</remarks>
        /// <remarks>第三个值表示相对地址，直接参与报文</remarks>
        public static OperateResult<ModbusStoreArea, byte, ushort> ModbusAddressAnalysis(string address, byte defaultDevAddress, bool isShortAddress)
        {
            var result = new OperateResult<ModbusStoreArea, byte, ushort>()
            {
                IsSuccess = true
            };

            try
            {
                //判断是否包含站地址

                string analysisAdd = string.Empty;

                if (address.Contains('.'))
                {
                    string[] values = address.Split('.');

                    if (values.Length == 2)
                    {
                        //获取从站地址
                        result.Content2 = Convert.ToByte(values[0]);

                        analysisAdd = values[1];
                    }
                    else
                    {
                        return OperateResult.CreateFailResult<ModbusStoreArea, byte, ushort>("变量地址格式不正确:" + address);
                    }
                }
                else
                {
                    //获取从站地址
                    result.Content2 = defaultDevAddress;
                    analysisAdd = address;
                }

                //到这里，从站地址已经确认

                //同时，已经确定好后续解析的地址   40001  10001

                //补全地址,主要是针对0区

                analysisAdd = analysisAdd.PadLeft(isShortAddress ? 5 : 6, '0');

                switch (analysisAdd[0].ToString())
                {
                    case "4":
                        result.Content1 = ModbusStoreArea.X4;
                        result.Content3 = Convert.ToUInt16(Convert.ToUInt32(analysisAdd.Substring(1)) - 1);
                        break;
                    case "3":
                        result.Content1 = ModbusStoreArea.X3;
                        result.Content3 = Convert.ToUInt16(Convert.ToUInt32(analysisAdd.Substring(1)) - 1);
                        break;
                    case "1":
                        result.Content1 = ModbusStoreArea.X1;
                        result.Content3 = Convert.ToUInt16(Convert.ToUInt32(analysisAdd.Substring(1)) - 1);
                        break;
                    case "0":
                        result.Content1 = ModbusStoreArea.X0;
                        result.Content3 = Convert.ToUInt16(Convert.ToUInt32(analysisAdd.Substring(1)) - 1);
                        break;
                    default:
                        result.IsSuccess = false;
                        result.Message = "变量地址格式不正确：" + analysisAdd;
                        break;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;

        }

    }
}
