﻿using System;
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
using Just.Log;
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
        }

        #region Setting
        public bool DelaySave { get; set; }

        public void LoadSettings()
        {
            SettingManager.Instance.ReadProperty(this, m => m.Title);
        }

        public void SaveSettings()
        {
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
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