using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp;
using Just.WPFCore.EShop.CefSharpHandler;
using Just.WPFCore.EShop.ViewModel;

namespace Just.WPFCore.EShop
{
    /// <summary>
    /// CrawlCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class CrawlCtrl : UserControl
    {
        private readonly CrawlVM vm = new CrawlVM();
        public CrawlCtrl()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void Browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            vm.LoadEndCmd.Execute(e);
        }
    }
}
