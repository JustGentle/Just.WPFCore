﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Just.Logging;
using ShowMeTheXAML;

namespace Just.WPFCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Logger.ExceptionDialog = new ExceptionDialog("RootDialog");
            XamlDisplay.Init();
            base.OnStartup(e);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var msg = "Unhandled exception !";
                msg += e.IsTerminating ? "Application is Terminating" : "";
                if (e.ExceptionObject is Exception ex)
                {
                    Logger.Fatal(msg, ex);
                }
                else
                {
                    ex = new Exception(e.ExceptionObject?.ToString());
                    Logger.Fatal(msg, ex);
                }
                ShutdownWhenNoMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                Logger.Fatal("Unobserved Task Exception", e.Exception);
                e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
                ShutdownWhenNoMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unobserved Task Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出
                Logger.Fatal("Dispatcher Unhandled Exception", e.Exception);
                ShutdownWhenNoMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dispatcher Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShutdownWhenNoMainWindow()
        {
            if (MainWindow.Visibility != Visibility.Visible)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
