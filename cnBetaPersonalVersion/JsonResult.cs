using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cnBetaPersonalVersion
{
    public class Label
    {
        /// <summary>
        /// 科技
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string link { get; set; }
    }

    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string sid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string catid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string aid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// Cherry宣布推出机械键盘新轴体：有望标配高端笔记本
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string keywords { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hometext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string comments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string counter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mview { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string collectnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string good { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bad { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ratings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string score_story { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ratings_story { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pollid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string queueid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inputtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updatetime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thumb { get; set; }
        /// <summary>
        /// 快科技@http://www.kkj.cn/
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url_show { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Label label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> relation { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> list_sub { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int auto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
    }

    public class JsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }
    }
}