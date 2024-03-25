using Base.Common.Config;
using Base.DataConvertLib;
using Base.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.Common
{
    public class CommonMethods: INotifyPropertyChanged
    {
        #region 成员变量
        #region 配置变量
        public static string DevicePath = Environment.CurrentDirectory + "\\Config\\Device.ini";
        public static string VariablePath = Environment.CurrentDirectory + "\\Config\\Variable.xlsx";

        public static List<Variable> variables { get; set; }

        public static Action<string> PrintAction { get; set; }
         #endregion

        public static string CurCode;
        #endregion



        //系统日志添加
        public static Action<string, int> AddSysLog;


        public static string SoftVersion {  get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public  event PropertyChangedEventHandler PropertyChanged;

        protected  void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
