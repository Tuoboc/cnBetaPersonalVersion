using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HtmlAgilityPack;

namespace cnBetaPersonalVersion
{
    [Activity(Label = "cnBeta个人版",NoHistory =true)]
    public class DetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ArticleDetail);
            // Create your application here
            string titleString = Intent.GetStringExtra("title");
            string url = Intent.GetStringExtra("url");
            TextView Title = FindViewById<TextView>(Resource.Id.textTitle);
            TextView articleContent = FindViewById<TextView>(Resource.Id.ArticleContent);
            Title.Text = titleString;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = response.GetResponseStream();
                HtmlDocument html = new HtmlDocument();

                html.Load(responseString);
                var res = html.DocumentNode.SelectSingleNode(@"/html/body/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/p");
                if (res != null)
                {
                    var list = res.InnerHtml;
                    if (!string.IsNullOrEmpty(list))
                    {
                        var regex = new Regex(@"<[^>]*>");
                        string summary = regex.Replace(list, "");
                        articleContent.Text = "    "+summary;
                    }
                }
                res = html.DocumentNode.SelectSingleNode("//*[@id=\"artibody\"]");
                if (res != null)
                {
                    var node = res.SelectNodes("p");
                    foreach (var item in  node)
                    {
                        var regex = new Regex(@"<[^>]*>");
                        string summary = regex.Replace(item.InnerHtml, "");
                        articleContent.Text +=  "\n"+ "    " + summary;
                    }
                }
            }
            catch
            {

            }
        }
    }
}