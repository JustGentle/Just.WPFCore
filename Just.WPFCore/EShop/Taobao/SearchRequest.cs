using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Just.WPFCore.EShop.Taobao
{
    public class SearchRequest
    {
        /// <summary>
        /// 关键字
        /// </summary>
        [JsonProperty("q")]
        public string Keywork { get; set; }

        /// <summary>
        /// 跳过数量
        /// </summary>
        [JsonProperty("s")]
        public int? SkipCount => PageIndex > 1 ? (PageIndex - 1) * Consts.DEFAULT_PAGE_SIZE : (int?)null;

        /// <summary>
        /// 页码（从1开始）
        /// </summary>
        [JsonIgnore]
        public ushort PageIndex { get; set; }
    }
}
