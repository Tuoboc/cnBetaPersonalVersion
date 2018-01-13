using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HtmlAgilityPack;

namespace cnBetaPersonalVersion
{
    public class Article
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string OtherInfo { get; set; }
        public string ID { get; set; }
        public string URL { get; set; }
        public string ImageURL { get; set; }

    }
    public class ArticleList
    {
        public static Article[] articleList = {
            new Article{Title="北京首条磁浮线路开始试运营",
            Summary="12月30日，北京首条中低速磁浮轨道交通S1线开始试运营，标志着中低速磁浮交通技术工程化研发成果在北京成功落地。据了解，S1线连接石景山区和门头沟区，是北京西部居民出行的重要交通线路，线路由苹果园站（暂缓开通）、金安桥站、四道桥站、桥户营站、上岸站、栗园庄站、小园站和石厂站八座车站组成。",
            Type="科技",
            OtherInfo="发布于2018-01-01 12:10  |  291次阅读  |  0个意见",
            ImageURL="https://static.cnbetacdn.com/thumb/mini/article/2017/1231/fc985fcd2610bf1.jpg"}
        };

        public void SetList(List<Article> list)
        {
            articleList = list.ToArray();
        }

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
        public async Task<Stream> GetFileStreamAsync(string url)
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

        public async Task GetArticle()
        {
            List<Article> articleList = new List<Article>();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://www.cnbeta.com/");
                var response = (HttpWebResponse)await request.GetResponseAsync();
                var responseString = response.GetResponseStream();
                HtmlDocument html = new HtmlDocument();

                html.Load(responseString);
                var res = html.DocumentNode.SelectSingleNode(@"/html/body/div[1]/div[4]/div/div[1]/div[2]");
                if (res != null)
                {

                    var list = res.SelectNodes(@"div");
                    if (list.Count > 1)
                    {
                        List<string> titleList = new List<string>();
                        List<string> urlList = new List<string>();
                        List<string> IDList = new List<string>();
                        List<string> summaryList = new List<string>();
                        List<string> typeList = new List<string>();
                        List<string> otherList = new List<string>();
                        List<string> imageList = new List<string>();
                        Regex regex;
                        Match match;
                        foreach (var item in list)
                        {
                            if (item.SelectSingleNode(@"dl") == null) continue;
                            var dd = item.SelectSingleNode(@"dl").SelectSingleNode(@"dt").SelectNodes("a");
                            foreach (var node in dd)
                            {
                                //标题
                                var text = node.InnerText;
                                titleList.Add(text);
                                //链接
                                var herf = node.Attributes["href"].Value;
                                urlList.Add(herf);
                                //ID
                                regex = new Regex(@"\d{6}");
                                match = regex.Match(herf);
                                if (match.Success)
                                {
                                    IDList.Add(match.Groups[0].Value);
                                }
                            }
                            //摘要
                            dd = item.SelectSingleNode(@"dl").SelectSingleNode(@"dd").SelectNodes("p");
                            foreach (var node in dd)
                            {
                                regex = new Regex(@"<[^>]*>");
                                string summary = regex.Replace(node.InnerHtml, "");
                                summaryList.Add(summary);
                            }
                            //图片
                            dd = item.SelectSingleNode(@"dl").SelectSingleNode(@"a").SelectNodes("img");
                            foreach (var node in dd)
                            {
                                var herf = node.Attributes["src"].Value;
                                imageList.Add(herf);
                            }
                            //类型
                            dd = item.SelectSingleNode(@"div").SelectSingleNode(@"label").SelectNodes("a");
                            foreach (var node in dd)
                            {
                                var text = node.InnerText;
                                typeList.Add(text);
                            }
                            //其他信息
                            dd = item.SelectSingleNode(@"div").SelectSingleNode(@"ul").SelectNodes("li");
                            for (int i=0;i<dd.Count;i++)
                            {
                                if(i==0)
                                {
                                    var text = dd[i].InnerText;
                                    otherList.Add(text);
                                }
                               
                            }

                            
                        }

                        for (int i = 0; i < titleList.Count; i++)
                        {
                            Article article = new Article();
                            article.Title = titleList[i];
                            article.Summary = summaryList[i];
                            article.Type = typeList[i];
                            article.OtherInfo = otherList[i];
                            article.ImageURL = imageList[i];
                            article.URL = urlList[i];
                            article.ID = IDList[i];
                            articleList.Add(article);
                        }
                    }
                    
                }

                this.SetList(articleList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}