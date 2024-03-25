using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib { 
    [Description("数值线性转换类")]
    public class MigrationLib
    {
        private static string ByteMax = byte.MaxValue.ToString();

        private static string ByteMin = ((byte)0).ToString();

        private static string ShortMax = short.MaxValue.ToString();

        private static string ShortMin = ((ushort)0).ToString();

        private static string UShortMax = ushort.MaxValue.ToString();

        private static string UShortMin = ((ushort)0).ToString();

        private static string IntMax = int.MaxValue.ToString();

        private static string IntMin = int.MinValue.ToString();

        private static string UIntMax = uint.MaxValue.ToString();

        private static string UIntMin = 0u.ToString();

        private static string FloatMax = float.MaxValue.ToString();

        private static string FloatMin = float.MinValue.ToString();

        private static string LongMax = long.MaxValue.ToString();

        private static string LongMin = long.MinValue.ToString();

        private static string ULongMax = ulong.MaxValue.ToString();

        private static string ULongMin = 0uL.ToString();

        private static string DoubleMax = double.MaxValue.ToString();

        private static string DoubleMin = double.MinValue.ToString();

        private static string GetErrorMsg(DataType type)
        {
            string empty = string.Empty;
            switch (type)
            {
                case DataType.Byte:
                    return "设置范围：" + ByteMin + "-" + ByteMax;
                case DataType.Short:
                    return "设置范围：" + ShortMin + "-" + ShortMax;
                case DataType.UShort:
                    return "设置范围：" + UShortMin + "-" + UShortMax;
                case DataType.Int:
                    return "设置范围：" + IntMin + "-" + IntMax;
                case DataType.UInt:
                    return "设置范围：" + UIntMin + "-" + UIntMax;
                case DataType.Long:
                    return "设置范围：" + LongMin + "-" + LongMax;
                case DataType.ULong:
                    return "设置范围：" + ULongMin + "-" + ULongMax;
                case DataType.Float:
                    return "设置范围：" + FloatMin + "-" + FloatMax;
                case DataType.Double:
                    return "设置范围：" + DoubleMin + "-" + DoubleMax;
                default:
                    return "非有效值类型";
            }
        }

        [Description("获取线性转换结果")]
        public static OperateResult<object> GetMigrationValue(object value, string scale, string offset)
        {
            if (scale == "1" && offset == "0")
            {
                return OperateResult.CreateSuccessResult(value);
            }

            try
            {
                string name = value.GetType().Name;
                object value2;
                switch (name.ToLower())
                {
                    case "byte":
                    case "int16":
                    case "uint16":
                    case "int32":
                    case "uint32":
                    case "single":
                        value2 = Convert.ToSingle((Convert.ToSingle(value) * Convert.ToSingle(scale) + Convert.ToSingle(offset)).ToString("N4"));
                        break;
                    case "int64":
                    case "uint64":
                    case "double":
                        value2 = Convert.ToDouble((Convert.ToDouble(value) * Convert.ToDouble(scale) + Convert.ToDouble(offset)).ToString("N4"));
                        break;
                    default:
                        value2 = value;
                        break;
                }

                return OperateResult.CreateSuccessResult(value2);
            }
            catch (Exception ex)
            {
                return new OperateResult<object>
                {
                    IsSuccess = false,
                    Message = "转换出错：" + ex.Message
                };
            }
        }

        [Description("线性转换后的设定值")]
        public static OperateResult<string> SetMigrationValue(string set, DataType type, string scale, string offset)
        {
            OperateResult<string> operateResult = new OperateResult<string>();
            if (scale == "1" && offset == "0")
            {
                try
                {
                    switch (type)
                    {
                        case DataType.Byte:
                            operateResult.Content = Convert.ToByte(set).ToString();
                            break;
                        case DataType.Short:
                            operateResult.Content = Convert.ToInt16(set).ToString();
                            break;
                        case DataType.UShort:
                            operateResult.Content = Convert.ToUInt16(set).ToString();
                            break;
                        case DataType.Int:
                            operateResult.Content = Convert.ToInt32(set).ToString();
                            break;
                        case DataType.UInt:
                            operateResult.Content = Convert.ToUInt32(set).ToString();
                            break;
                        case DataType.Long:
                            operateResult.Content = Convert.ToInt64(set).ToString();
                            break;
                        case DataType.ULong:
                            operateResult.Content = Convert.ToUInt64(set).ToString();
                            break;
                        case DataType.Float:
                            operateResult.Content = Convert.ToSingle(set).ToString();
                            break;
                        case DataType.Double:
                            operateResult.Content = Convert.ToDouble(set).ToString();
                            break;
                        default:
                            operateResult.Content = set;
                            break;
                    }

                    operateResult.IsSuccess = true;
                    return operateResult;
                }
                catch (Exception)
                {
                    operateResult.IsSuccess = false;
                    operateResult.Message = "转换出错，" + GetErrorMsg(type);
                    return operateResult;
                }
            }

            try
            {
                switch (type)
                {
                    case DataType.Byte:
                        operateResult.Content = Convert.ToByte((Convert.ToSingle(set) - Convert.ToSingle(offset)) / Convert.ToSingle(scale)).ToString();
                        break;
                    case DataType.Short:
                        operateResult.Content = Convert.ToInt16((Convert.ToSingle(set) - Convert.ToSingle(offset)) / Convert.ToSingle(scale)).ToString();
                        break;
                    case DataType.UShort:
                        operateResult.Content = Convert.ToUInt16((Convert.ToSingle(set) - Convert.ToSingle(offset)) / Convert.ToSingle(scale)).ToString();
                        break;
                    case DataType.Int:
                        operateResult.Content = Convert.ToInt32((Convert.ToSingle(set) - Convert.ToSingle(offset)) / Convert.ToSingle(scale)).ToString();
                        break;
                    case DataType.UInt:
                        operateResult.Content = Convert.ToUInt32((Convert.ToSingle(set) - Convert.ToSingle(offset)) / Convert.ToSingle(scale)).ToString();
                        break;
                    case DataType.Long:
                        operateResult.Content = Convert.ToInt64((Convert.ToDouble(set) - Convert.ToDouble(offset)) / Convert.ToDouble(scale)).ToString();
                        break;
                    case DataType.ULong:
                        operateResult.Content = Convert.ToUInt64((Convert.ToDouble(set) - Convert.ToDouble(offset)) / Convert.ToDouble(scale)).ToString();
                        break;
                    case DataType.Float:
                        operateResult.Content = Convert.ToSingle((Convert.ToSingle(set) - Convert.ToSingle(offset)) / Convert.ToSingle(scale)).ToString();
                        break;
                    case DataType.Double:
                        operateResult.Content = Convert.ToDouble((Convert.ToDouble(set) - Convert.ToDouble(offset)) / Convert.ToDouble(scale)).ToString();
                        break;
                    default:
                        operateResult.Content = set;
                        break;
                }

                operateResult.IsSuccess = true;
                return operateResult;
            }
            catch (Exception)
            {
                operateResult.IsSuccess = false;
                operateResult.Message = "转换出错，" + GetErrorMsg(type);
                return operateResult;
            }
        }
    }
}
