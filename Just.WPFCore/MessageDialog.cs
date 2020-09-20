using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Just.Logging;

namespace Just.WPFCore
{
    public class MessageDialog : IMessageDialog
    {
        private object _dialogIdentifier;
        private readonly ConcurrentQueue<Task> ts = new ConcurrentQueue<Task>();
        //队列是否正在处理数据
        private int _isRunning = -1;
        private bool IsRunning
        {
            get
            {
                return !(Interlocked.CompareExchange(ref _isRunning, 1, -1) == -1);
            }
            set
            {
                var processing = value ? 1 : -1;
                Interlocked.CompareExchange(ref _isRunning, processing, -processing);
            }
        }

        public MessageDialog(object dialogIdentifier)
        {
            _dialogIdentifier = dialogIdentifier;
        }

        public Task<object> Show(string message, Exception ex = null)
        {
            var task = new Task<object>(() => ShowNow(message, ex).Result);
            ts.Enqueue(task);
            if (!IsRunning)
                Run();
            return task;
        }

        //队列处理
        private void Run()
        {
            IsRunning = true;
            Task.Run(() =>
            {
                while (!ts.IsEmpty)
                {
                    if (!ts.TryPeek(out Task current))
                        continue;
                    if (current.Status != TaskStatus.Created)
                        continue;
                    current.ContinueWith(result =>
                    {
                        ts.TryDequeue(out Task current);
                    });
                    current.Start();
                }
            }).ContinueWith(task => IsRunning = false);
        }
        private Task<object> ShowNow(string message, Exception ex)
        {
            object content = ex;
            if (ex == null)
            {
                content = new StringInfo(message);
            }
            else if (!(ex is FriendlyException))
            {
                content = new Exception(message, ex);
            }
            Task<object> result = null;
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = MaterialDesignThemes.Wpf.DialogHost.Show(content, _dialogIdentifier);
                });
            }
            catch (Exception e)
            {
                Logger.Warn("显示提示框失败", e, false);
            }

            return result ?? Task.FromResult(null as object);
        }
    }
}
