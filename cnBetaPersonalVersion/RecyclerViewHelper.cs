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
using Android.Support.V7.Widget;
using Java.Net;
using System.IO;
using System.Threading;

namespace cnBetaPersonalVersion
{
    public class ArticleHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Title { get; private set; }
        public TextView Summary { get; private set; }
        public TextView Other { get; private set; }

        public ArticleHolder(View itemview, Action<int> listener) : base(itemview)
        {
            Image = itemview.FindViewById<ImageView>(Resource.Id.imageView);
            Title = itemview.FindViewById<TextView>(Resource.Id.textViewTitle);
            Summary = itemview.FindViewById<TextView>(Resource.Id.textViewSummary);
            Other = itemview.FindViewById<TextView>(Resource.Id.textViewOther);
            itemview.Click += (sender, e) => listener(base.LayoutPosition);
        }
    }

    public class ArticleAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }

        public ArticleList articleList;
        public ArticleAdapter(ArticleList atl)
        {
            articleList = atl;
        }
        public override int ItemCount => articleList.ArticleNum;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ArticleHolder ah = holder as ArticleHolder;
            GetImage(position, ah);
            //ThreadPool.QueueUserWorkItem(o => GetImage(position, ah));
            ah.Title.Text = articleList[position].Title;
            ah.Summary.Text = "    " + articleList[position].Summary;
            ah.Other.Text = articleList[position].OtherInfo.Replace("&nbsp;", ""); ;
        }

        private async void GetImage(int position, ArticleHolder ah)
        {
            var stream = await articleList.GetFileStreamAsync(articleList[position].ImageURL);
            ah.Image.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeStream(stream));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ArticleCardView, parent, false);
            ArticleHolder ah = new ArticleHolder(itemView, OnClick);
            return ah;
        }
    }
}