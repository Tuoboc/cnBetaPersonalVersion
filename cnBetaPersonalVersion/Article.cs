using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cnBetaPersonalVersion
{
    public class Article
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime PulishTime { get; set; }
        public int ReadTimes { get; set; }
        public string ID { get; set; }
        public string URL { get; set; }
        public string ImageURL { get; set; }

    }
    public class ArticleList
    {
        static Article[] articleList = {
            new Article{Title="北京首条磁浮线路开始试运营",
            Summary="12月30日，北京首条中低速磁浮轨道交通S1线开始试运营，标志着中低速磁浮交通技术工程化研发成果在北京成功落地。据了解，S1线连接石景山区和门头沟区，是北京西部居民出行的重要交通线路，线路由苹果园站（暂缓开通）、金安桥站、四道桥站、桥户营站、上岸站、栗园庄站、小园站和石厂站八座车站组成。",
            Type="科技",
            PulishTime=new DateTime(2017,12,31,14,22,31),
            ReadTimes=910,
            ImageURL="https://static.cnbetacdn.com/thumb/mini/article/2017/1231/fc985fcd2610bf1.jpg"}
        };

        public Article this[int i]
        {
            get { return articleList[i]; }
        }

        public int ArticleNum
        {
            get { return articleList.Length; }
        }

        /// <summary>
        /// 异步获取文件流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public  async Task<Stream> GetFileStreamAsync(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)await request.GetResponseAsync();
                var responseString = response.GetResponseStream();
                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}