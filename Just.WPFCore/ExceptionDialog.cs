using System;
using Just.Log;

namespace Just.WPFCore
{
    public class ExceptionDialog : IExceptionDialog
    {
        private object _dialogIdentifier;
        public ExceptionDialog(object dialogIdentifier)
        {
            _dialogIdentifier = dialogIdentifier;
        }
        public dynamic Show(string message, Exception ex)
        {
            if(!(ex is FriendlyException))
            {
                ex = new Exception(message, ex);
            }
            return MaterialDesignThemes.Wpf.DialogHost.Show(ex, _dialogIdentifier);
        }
    }
}
