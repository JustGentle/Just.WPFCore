﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using GalaSoft.MvvmLight.Command;
using Just.Logging;
using Just.WPFCore.EShop.CefSharpHandler;
using Just.WPFCore.EShop.Taobao;
using Newtonsoft.Json.Linq;

namespace Just.WPFCore.EShop.ViewModel
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CrawlVM
    {
        #region public
        public string InputAddress { get; set; }
        private string _BrowserAddress = "https://login.m.taobao.com/";

        public string BrowserAddress
        {
            get
            {
                return _BrowserAddress;
            }
            set
            {
                _BrowserAddress = value;
                if (!string.Equals(InputAddress, value))
                {
                    InputAddress = value;
                }
            }
        }

        public RequestHandler RequestHandler { get; set; }
        public Dictionary<string, Cookie> Cookies { get; set; } = new Dictionary<string, Cookie>();
        #endregion

        #region private
        private readonly CookieVisitor visitor = new CookieVisitor();
        private readonly TaobaoApi taobaoApi = new TaobaoApi();
        #endregion

        #region ctor
        public CrawlVM()
        {
            InitCmd();
        }

        public void InitCmd()
        {
            GoAddressCmd = new RelayCommand(GoAddress);
            LoadEndCmd = new RelayCommand<FrameLoadEndEventArgs>(LoadEnd);
            TestCmd = new RelayCommand(Test);
        }
        #endregion

        public RelayCommand GoAddressCmd { get; private set; }
        private void GoAddress()
        {
            BrowserAddress = InputAddress;
        }

        public RelayCommand<FrameLoadEndEventArgs> LoadEndCmd { get; private set; }
        private void LoadEnd(FrameLoadEndEventArgs e)
        {
            visitor.SendCookie += (args) =>
            {
                if (args.IsDelete)
                {
                    Cookies.Remove(args.Cookie.Name);
                }
                else
                {
                    Cookies.AddOrUpdate(args.Cookie.Name, args.Cookie);
                }
            };
            Cef.GetGlobalCookieManager().VisitAllCookies(visitor);
        }

        public RelayCommand TestCmd { get; private set; }
        private void Test()
        {
            taobaoApi.SetCookies(Cookies.Values);
            taobaoApi.SearchAsync(new SearchRequest
            {
                Keywork = "连衣裙",
                PageIndex = 2
            }).ContinueWith(task => App.RootDialog.Show(string.Join(',', GetSearchData(task.Result).Select(a => a.raw_title))));
        }
        private IEnumerable<Auction> GetSearchData(string html)
        {
            var json = System.Text.RegularExpressions.Regex.Match(html, "(?<=g_page_config = ).+(?=;)").Value;
            if (string.IsNullOrEmpty(json))
            {
                Logger.Warn(html, new FriendlyException("搜索失败"));
            }
            try
            {
                var obj = JObject.Parse(json);
                var items = obj["mods"]["itemlist"]["data"]["auctions"] as JArray;
                var auctions = items.Select(i=>i.ToObject<Auction>());
                return auctions;
            }
            catch (Exception ex)
            {
                Logger.Warn(json, new FriendlyException("找不到商品数据", ex));
            }
            return Enumerable.Empty<Auction>();
        }
    }
}
