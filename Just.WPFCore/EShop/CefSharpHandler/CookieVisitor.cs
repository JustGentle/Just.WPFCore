using System;
using System.Collections.Generic;
using CefSharp;

namespace Just.WPFCore.EShop.CefSharpHandler
{
    public class CookieVisitor : ICookieVisitor
    {
        public void Dispose()
        {
        }

        public event Action<CookieVisitEventArgs> SendCookie;
        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            SendCookie?.Invoke(new CookieVisitEventArgs 
            { 
                Cookie = cookie,
                Index = count,
                Total = total,
                IsDelete = deleteCookie
            });

            return true;
        }
    }

    public class CookieVisitEventArgs
    {
        public Cookie Cookie { get; set; }
        public int Index { get; set; }
        public int Total { get; set; }
        public bool IsDelete { get; set; }
    }
}
