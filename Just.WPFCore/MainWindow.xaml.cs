using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Just.Logging;
using Just.Setting;

namespace Just.WPFCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISetting
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Prop
        public string IconPrefix
        {
            get { return (string)GetValue(IconPrefixProperty); }
            set { SetValue(IconPrefixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconPrefix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconPrefixProperty =
            DependencyProperty.Register("IconPrefix", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));


        public string IconSuffix
        {
            get { return (string)GetValue(IconSuffixProperty); }
            set { SetValue(IconSuffixProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSuffix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSuffixProperty =
            DependencyProperty.Register("IconSuffix", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));


        public SolidColorBrush IconDarkBrush
        {
            get { return (SolidColorBrush)GetValue(DarkBrushProperty); }
            set { SetValue(DarkBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for solidColorBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DarkBrushProperty =
            DependencyProperty.Register("IconDarkBrush", typeof(SolidColorBrush), typeof(MainWindow), new PropertyMetadata(Brushes.Transparent));

        public SolidColorBrush IconLightBrush
        {
            get { return (SolidColorBrush)GetValue(IconLightBrushProperty); }
            set { SetValue(IconLightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for solidColorBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconLightBrushProperty =
            DependencyProperty.Register("IconLightBrush", typeof(SolidColorBrush), typeof(MainWindow), new PropertyMetadata(Brushes.Transparent));

        #endregion


        #region Setting
        public bool DelaySave { get; set; }

        public void LoadSettings()
        {
            this.ReadProperty(m => m.Title)
                .ReadProperty(m => m.IconPrefix)
                .ReadProperty(m => m.IconSuffix)
                .ReadProperty(m => m.IconDarkBrush)
                .ReadProperty(m => m.IconLightBrush);
        }

        public void SaveSettings()
        {
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings();
        }
        #endregion

        #region 窗口控制按钮
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinMenuItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 最大化/还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinStateMenuItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SwitchWindowState();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMenuItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 标题栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleZone_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount >= 2)
                SwitchWindowState();//双击切换窗口最大化
            else if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();//拖拽移动窗口
            e.Handled = true;
        }

        /// <summary>
        /// 切换窗口状态
        /// </summary>
        private void SwitchWindowState()
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
        /// <summary>
        /// 状态处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowStyle != WindowStyle.None) return;

            //无边框时最大化会全屏覆盖任务栏,需要做处理
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowStyle = WindowStyle.None;
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new FriendlyException("test");
        }

    }
}
