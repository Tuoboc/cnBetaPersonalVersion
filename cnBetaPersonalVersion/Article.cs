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
using Newtonsoft.Json;

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
        public string _csrf { get; set; }

        public static Article[] articleList = {
           
        };

        public void SetList(List<Article> list)
        {
            articleList = list.ToArray();
        }

        public void AddList(List<Article>list)
        {
           articleList= articleList.Union(list.ToArray()).ToArray();            
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
                var responseString = await GetFileStreamAsync("http://www.cnbeta.com/");
                HtmlDocument html = new HtmlDocument();

                html.Load(responseString);
                var _csrf = html.DocumentNode.SelectSingleNode(@"/html/head/meta[18]");
                if (_csrf != null)
                {
                    GlobalVariables._csrf = _csrf.Attributes["content"].Value;
                }
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

        public async Task GetMoreArticle()
        {
            List<Article> articleList = new List<Article>();
            try
            {
                var responseString = await GetFileStreamAsync("http://www.cnbeta.com/");
                responseString.Position = 0;
                StreamReader reader = new StreamReader(responseString);
                string text = reader.ReadToEnd();
                JsonResult m = JsonConvert.DeserializeObject<JsonResult>(text);
                foreach (ListItem item in m.result.list)
                {

                }
            }
            catch
            {

            }
        }
    }
}