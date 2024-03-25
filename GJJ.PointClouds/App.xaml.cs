using Base.Helper;
using Base.UI.View;
using Base.UI.View.Pages;
using Base.UI.ViewModel;
using MiniExcelLibs;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Base.UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            //通过容器的形式获取到需要显示的MainWindow
            return Container.Resolve<MainView>();
        }
        /// <summary>
        /// 依赖注入的实现，暂时没有，可以不用添加
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SiemensPage, SiemensViewModel>();

        }
    }
}
