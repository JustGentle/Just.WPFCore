using System;
using System.Threading.Tasks;

namespace Just.Logging
{
    public interface IMessageDialog
    {
        Task<object> Show(string message, Exception ex = null);
    }
}
