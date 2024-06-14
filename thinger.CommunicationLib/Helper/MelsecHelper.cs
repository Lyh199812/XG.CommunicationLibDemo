using Base.DataConvertLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.CommunicationLib.StoreArea;

namespace thinger.CommunicationLib.Helper
{
    public class MelsecHelper
    {
        public static OperateResult<MelsecStoreArea, int> MelsecAddressAnalysis(string address, bool IsFx5U)
        {
            OperateResult<MelsecStoreArea, int> operateResult = new OperateResult<MelsecStoreArea, int>();
            try
            {
                switch (address[0].ToString().ToUpper())
                {
                    case "X":
                        operateResult.Content1 = MelsecStoreArea.X;
                        operateResult.Content2 = (IsFx5U ? Convert.ToUInt16(address.Substring(1), MelsecStoreArea.X8.FromBase) : Convert.ToUInt16(address.Substring(1), MelsecStoreArea.X.FromBase));
                        break;
                    case "Y":
                        operateResult.Content1 = MelsecStoreArea.Y;
                        operateResult.Content2 = (IsFx5U ? Convert.ToUInt16(address.Substring(1), MelsecStoreArea.X8.FromBase) : Convert.ToUInt16(address.Substring(1), MelsecStoreArea.Y.FromBase));
                        break;
                    case "M":
                        operateResult.Content1 = MelsecStoreArea.M;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.M.FromBase);
                        break;
                    case "L":
                        operateResult.Content1 = MelsecStoreArea.L;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.L.FromBase);
                        break;
                    case "F":
                        operateResult.Content1 = MelsecStoreArea.F;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.F.FromBase);
                        break;
                    case "V":
                        operateResult.Content1 = MelsecStoreArea.V;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.V.FromBase);
                        break;
                    case "B":
                        operateResult.Content1 = MelsecStoreArea.B;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.B.FromBase);
                        break;
                    case "D":
                        operateResult.Content1 = MelsecStoreArea.D;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.D.FromBase);
                        break;
                    case "W":
                        operateResult.Content1 = MelsecStoreArea.W;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.W.FromBase);
                        break;
                    case "Z":
                        operateResult.Content1 = MelsecStoreArea.Z;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.Z.FromBase);
                        break;
                    case "T":
                        if (address.Substring(0, 2).ToUpper() == "TN")
                        {
                            operateResult.Content1 = MelsecStoreArea.TN;
                            operateResult.Content2 = Convert.ToInt32(address.Substring(2), MelsecStoreArea.TN.FromBase);
                        }

                        break;
                    case "S":
                        if (address.Substring(0, 2).ToUpper() == "SN")
                        {
                            operateResult.Content1 = MelsecStoreArea.SN;
                            operateResult.Content2 = Convert.ToInt32(address.Substring(2), MelsecStoreArea.SN.FromBase);
                        }

                        break;
                    case "C":
                        if (address.Substring(0, 2).ToUpper() == "CN")
                        {
                            operateResult.Content1 = MelsecStoreArea.CN;
                            operateResult.Content2 = Convert.ToInt32(address.Substring(2), MelsecStoreArea.CN.FromBase);
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

        public static OperateResult<MelsecA1EStoreArea, int> MelsecA1EAddressAnalysis(string address)
        {
            OperateResult<MelsecA1EStoreArea, int> operateResult = new OperateResult<MelsecA1EStoreArea, int>();
            try
            {
                switch (address[0].ToString().ToUpper())
                {
                    case "X":
                        operateResult.Content1 = MelsecA1EStoreArea.X;
                        operateResult.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EStoreArea.X.FromBase);
                        break;
                    case "Y":
                        operateResult.Content1 = MelsecA1EStoreArea.Y;
                        operateResult.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EStoreArea.Y.FromBase);
                        break;
                    case "M":
                        operateResult.Content1 = MelsecA1EStoreArea.M;
                        operateResult.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EStoreArea.M.FromBase);
                        break;
                    case "D":
                        operateResult.Content1 = MelsecA1EStoreArea.D;
                        operateResult.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EStoreArea.D.FromBase);
                        break;
                    case "W":
                        operateResult.Content1 = MelsecA1EStoreArea.W;
                        operateResult.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EStoreArea.W.FromBase);
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

        public static OperateResult<MelsecStoreArea, ushort> MelsecFXAnalysisAddress(string address)
        {
            OperateResult<MelsecStoreArea, ushort> operateResult = new OperateResult<MelsecStoreArea, ushort>();
            try
            {
                switch (address[0].ToString().ToUpper())
                {
                    case "X":
                        operateResult.Content1 = MelsecStoreArea.X;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.X8.FromBase);
                        break;
                    case "Y":
                        operateResult.Content1 = MelsecStoreArea.Y;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.X8.FromBase);
                        break;
                    case "M":
                        operateResult.Content1 = MelsecStoreArea.M;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.M.FromBase);
                        break;
                    case "D":
                        operateResult.Content1 = MelsecStoreArea.D;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.D.FromBase);
                        break;
                    case "W":
                        operateResult.Content1 = MelsecStoreArea.W;
                        operateResult.Content2 = Convert.ToUInt16(address.Substring(1), MelsecStoreArea.W.FromBase);
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

        public static OperateResult<string> MelsecFXLinkAnalysisAddress(string address)
        {
            OperateResult<string> operateResult = new OperateResult<string>();
            try
            {
                switch (address[0].ToString().ToUpper())
                {
                    case "X":
                        operateResult.Content = "X" + Convert.ToUInt16(address.Substring(1), 8).ToString("D4");
                        break;
                    case "Y":
                        operateResult.Content = "Y" + Convert.ToUInt16(address.Substring(1), 8).ToString("D4");
                        break;
                    case "M":
                        operateResult.Content = "M" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        break;
                    case "S":
                        operateResult.Content = "S" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        break;
                    case "T":
                        if (address[1].ToString().ToUpper() == "S")
                        {
                            operateResult.Content = "TS" + Convert.ToUInt16(address.Substring(2), 10).ToString("D3");
                            break;
                        }

                        if (address[1].ToString().ToUpper() == "N")
                        {
                            operateResult.Content = "TN" + Convert.ToUInt16(address.Substring(2), 10).ToString("D3");
                            break;
                        }

                        throw new Exception("不支持的变量类型");
                    case "C":
                        if (address[1].ToString().ToUpper() == "S")
                        {
                            operateResult.Content = "CS" + Convert.ToUInt16(address.Substring(2), 10).ToString("D3");
                            break;
                        }

                        if (address[1].ToString().ToUpper() == "N")
                        {
                            operateResult.Content = "CN" + Convert.ToUInt16(address.Substring(2), 10).ToString("D3");
                            break;
                        }

                        throw new Exception("不支持的变量类型");
                    case "D":
                        operateResult.Content = "D" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        break;
                    case "R":
                        operateResult.Content = "R" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
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
