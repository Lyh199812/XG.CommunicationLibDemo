using BS.Helper;
using Base.UI.View.Pages;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Base.Common;

namespace Base.UI.ViewModel
{
    public class MonitorViewModel: BindableBase
    {

        public MonitorViewModel() {
            CommonMethods.AddSysLog = AddSysLog;
            EquipPage =new SiemensPage();
            logList = new ObservableCollection<OperateLog>();
        }
        #region 视图属性
    
        private object equipPage;

        public object EquipPage
        {
            get { return equipPage; }
            set { equipPage = value; RaisePropertyChanged(); }
        }

      
        private ObservableCollection<OperateLog> logList;

        public ObservableCollection<OperateLog> LogList
        {
            get { return logList; }
            set
            {
                logList = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region 方法
        private void AddSysLog(string Meg, int Model = 0)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Model == 0)//成功
                {
                    logList.Add(new OperateLog { LogIcon = "\ue626", IconColor = "Green", OperateTime = DateTime.Now.ToString(), OperateInfo = Meg });
                }
                else if (Model == 1)//警告
                {
                    logList.Add(new OperateLog { LogIcon = "\ue616", IconColor = "Orange", OperateTime = DateTime.Now.ToString(), OperateInfo = Meg });
                }
                else//报警
                {
                    logList.Add(new OperateLog { LogIcon = "\ue62a", IconColor = "Red", OperateTime = DateTime.Now.ToString(), OperateInfo = Meg });

                }
            });
            TxtHelper.WriteToTxt($"SysLog\\{DateTime.Now.ToString("yyyy_MM_dd")}_MESLog.txt", "\r\n" + DateTime.Now.ToString() + "_" + Meg);

        }

        #endregion
    }
}
