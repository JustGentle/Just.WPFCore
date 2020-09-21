using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace Just.WPFCore.EShop.Taobao
{
    public class Auction
    {
        public string nid { get; set; }
        public string category { get; set; }
        public string pid { get; set; }
        public string raw_title { get; set; }
        public string pic_url { get; set; }
        public string detail_url { get; set; }
        public decimal? view_price { get; set; }
        public decimal? view_fee { get; set; }
        public string item_loc { get; set; }
        public string view_sales { get; set; }
        public long? comment_count { get; set; }
        public string user_id { get; set; }
        public string nick { get; set; }
        public List<Icon> icon { get; set; }
        public string comment_url { get; set; }
        public string shopLink { get; set; }
        public string risk { get; set; }
    }
    public class Icon
    {
        public string icon_key { get; set; }
        public string innerText { get; set; }
        public string url { get; set; }
    }
}
