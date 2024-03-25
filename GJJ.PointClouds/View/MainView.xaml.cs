using Base.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Base.UI.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext =new MainViewModel();
        }



        #region 界面缩放
        private void btn_Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_Max_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;

            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定要关闭窗口吗？", "确认", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                // 关闭其他相关窗口或资源
                // 保存数据
                // 执行清理操作

                // 关闭主窗口
                this.Close();
            }
        }
        #endregion


       
    }
}
