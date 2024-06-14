using GJJ.Model;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using Base.DataConvertLib;
using Base.Helper;
using System.IO;
using MiniExcelLibs;
using Prism.Commands;
using HalconDotNet;
using Base.Common;

namespace Base.UI.ViewModel
{
    public class MainViewModel: BindableBase
    {
       

        public MainViewModel() 
        {
            // 主窗口数据
            #region  菜单
            Menus = new List<MenuModel>();
            Menus.Add(new MenuModel
            {
                IsSelected = false,
                MenuHeader = "西门子\r\nS7协议",
                MenuIcon = "\ue620",
                TargetView = "SiemensPage"
            });
            Menus.Add(new MenuModel
            {
                IsSelected = false,
                MenuHeader = "三菱串口\r\nMC协议",
                MenuIcon = "\ue620",
                TargetView = "MCSerialPage"
            });

            #endregion
            ShowPage(Menus[1]);
            SwitchPageCommand= new DelegateCommand<object>(ShowPage);
            //软件版本
            SoftVersion = "V"+CommonMethods.SoftVersion;
         

        }
        #region 成员变量
        private Dictionary<string, object> viewCache = new Dictionary<string, object>();
        private readonly IRegionManager _regionManager;
        private DispatcherTimer _timer = new DispatcherTimer();
        //private CommonDataMethods dataMethods = new CommonDataMethods();
        //设备参数路径
        private string devicePath = Environment.CurrentDirectory + "\\Config\\Device.ini";

        //通信组参数路径
        private string groupPath = Environment.CurrentDirectory + "\\Config\\Group.xlsx";

        //变量路径
        private string variablePath = Environment.CurrentDirectory + "\\Config\\Variable.xlsx";

        //大小端
        private Base.DataConvertLib.DataFormat dataFormat = Base.DataConvertLib.DataFormat.ABCD;
        //取消线程源
        private CancellationTokenSource cts;

        #endregion


        #region 视图属性

        private string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        private string softVersion;
        public string SoftVersion
        {
            get { return softVersion; }
            set { softVersion = value; RaisePropertyChanged(); }
        }
        private List<MenuModel> menus;
        public List<MenuModel> Menus
        {
            get
            {
                return menus;
            }
            set
            {
                menus = value;
                RaisePropertyChanged(nameof(Menus));
            }
        }

        private object _viewContent;

        public object ViewContent
        {
            get { return _viewContent; }
            set { _viewContent = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 命令属性
        public DelegateCommand<object> SwitchPageCommand { get; set; }

        #endregion

        #region 方法


        private void OpenView(string obj)
        {
            //通过区域去设置需要显示的内容
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj);
        }

        #endregion
        private void ShowPage(object obj)
        {
            var model = obj as MenuModel;
            if (model != null)
            {
                if (ViewContent != null && ViewContent.GetType().Name == model.TargetView) return;

                if (viewCache.ContainsKey(model.TargetView))
                {
                    ViewContent = viewCache[model.TargetView];
                }
                else
                {
                    Type type = Assembly.Load("Base.UI").GetType("Base.UI.View.Pages." + model.TargetView);
                    ViewContent = Activator.CreateInstance(type);
                    viewCache[model.TargetView] = ViewContent;
                }
            }
        }
    }
}
