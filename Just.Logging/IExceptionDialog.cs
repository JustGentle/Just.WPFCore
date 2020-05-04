using System;

namespace Just.Logging
{
    public interface IExceptionDialog
    {
        dynamic Show(string message, Exception ex);
    }
}
