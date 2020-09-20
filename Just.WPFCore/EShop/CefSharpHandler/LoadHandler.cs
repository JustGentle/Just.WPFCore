using System;
using System.Collections.Generic;
using System.Text;
using CefSharp;

namespace Just.WPFCore.EShop.CefSharpHandler
{
    public class LoadHandler : ILoadHandler
    {
        public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
        }

        public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {
        }

        public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
        }

        public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
        {
        }
    }
}
