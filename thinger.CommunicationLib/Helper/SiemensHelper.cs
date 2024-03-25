using Base.DataConvertLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.CommunicationLib.StoreArea;

namespace Base.CommunicationLib
{
    public class SiemensHelper
    {
        public static OperateResult<SiemensStoreArea, int> SiemensAddressAnalysis(string address)
        {
            OperateResult<SiemensStoreArea, int> operateResult = new OperateResult<SiemensStoreArea, int>();
            try
            {
                switch (address[0].ToString().ToUpper())
                {
                    case "I":
                        operateResult.Content1 = SiemensStoreArea.I;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1));
                        break;
                    case "Q":
                        operateResult.Content1 = SiemensStoreArea.Q;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1));
                        break;
                    case "M":
                        operateResult.Content1 = SiemensStoreArea.M;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1));
                        break;
                    case "T":
                        operateResult.Content1 = SiemensStoreArea.T;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1));
                        break;
                    case "C":
                        operateResult.Content1 = SiemensStoreArea.C;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1));
                        break;
                    case "D":
                        if (address.Substring(0, 2).ToUpper() == "DB")
                        {
                            operateResult.Content1 = SiemensStoreArea.DB;
                            operateResult.Content1.DBNo = Convert.ToUInt16(address.Substring(2).Split('.')[0]);
                            operateResult.Content2 = Convert.ToUInt16(address.Substring(2).Split('.')[1]);
                        }
                        else
                        {
                            operateResult.IsSuccess = false;
                            operateResult.Message = "非有效地址";
                        }

                        break;
                    default:
                        operateResult.IsSuccess = false;
                        operateResult.Message = "非有效地址";
                        break;
                }
            }
            catch (Exception ex)
            {
                operateResult.IsSuccess = false;
                operateResult.Message = ex.Message;
                return operateResult;
            }

            operateResult.IsSuccess = true;
            operateResult.Message = "Success";
            return operateResult;
        }
    }
}
