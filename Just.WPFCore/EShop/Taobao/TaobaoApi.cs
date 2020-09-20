using System;
using System.Net.Http;
using System.Net;
using System.Net.Security;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using Just.Logging;
using System.Threading.Tasks;

namespace Just.WPFCore.EShop.Taobao
{
    public class TaobaoApi
    {
        public string BaseAddress { get; set; } = "https://s.taobao.com/";

        private static readonly object LockObj = new object();
        private static HttpClient _Http = null;
        public static HttpClient GetHttpInstance()
        {

            if (_Http == null)
            {
                lock (LockObj)
                {
                    if (_Http == null)
                    {
                        _Http = new HttpClient(new HttpClientHandler { UseCookies = true });
                    }
                }
            }
            return _Http;
        }
        public static HttpClient Http => GetHttpInstance();

        private JsonSerializerSettings defSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public TaobaoApi(IEnumerable<CefSharp.Cookie> cookies = null)
        {
            //Https安全协议和证书验证
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;//总是接受
            //预热
            Http.BaseAddress = new Uri(BaseAddress);
            Http.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod(HttpMethod.Head.Method)
            }).Result.EnsureSuccessStatusCode();
            Http.DefaultRequestHeaders.Add("user-agent", Consts.DEFAULT_USER_AGENT);
            if (cookies?.Any() == true)
                Http.DefaultRequestHeaders.Add("cookie", string.Join(';', cookies.Select(c => $"{c.Name}={c.Value}")));
        }

        public void SetCookies(IEnumerable<CefSharp.Cookie> cookies)
        {
            if (cookies == null)
                Http.DefaultRequestHeaders.Remove("cookie");
            else
            {
                Http.DefaultRequestHeaders.Remove("cookie");
                Http.DefaultRequestHeaders.Add("cookie", string.Join(';', cookies.Select(c => $"{c.Name}={c.Value}")));
            }
        }

        public Task<string> SearchAsync(SearchRequest data)
        {
            var query = GetUrlQuery(data);
            if (string.IsNullOrEmpty(query))
                throw new FriendlyException("请输入搜索条件");
            return Http.GetStringAsync("/Search?" + query);
        }

        private string GetUrlQuery(object data)
        {
            if (data == null)
                return string.Empty;

            //借用Newtonsoft转换属性
            var json = data.SerializeObject(defSettings);
            var obj = json.DeserializeObject<JObject>();
            var query = string.Join("&", obj.Properties().Select(p 
                => $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.Value.ToString())}"));

            return query;
        }
    }
}
