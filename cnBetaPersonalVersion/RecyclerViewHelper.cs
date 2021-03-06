﻿using System;
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
using System.ComponentModel;

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
        Stream stream;
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

        private void GetImage(int position, ArticleHolder ah)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            var sender = new object[2];
            sender[0] = articleList[position].ImageURL;
            sender[1] = ah;
            backgroundWorker.RunWorkerAsync(sender);

            // var stream =  articleList.GetFileStream(articleList[position].ImageURL);
            // ah.Image.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeStream(stream));
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var receive = e.Argument as object[];
            var ah = (ArticleHolder)receive[1];
            stream = CommonFun.GetFileStream((string)receive[0]);
            ah.Image.SetImageBitmap(Android.Graphics.BitmapFactory.DecodeStream(stream));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ArticleCardView, parent, false);
            ArticleHolder ah = new ArticleHolder(itemView, OnClick);
            return ah;
        }
    }

    public class XamarinRecyclerViewOnScrollListener : RecyclerView.OnScrollListener
    {
        public delegate void LoadMoreEventHandler(object sender, EventArgs e);
        public event LoadMoreEventHandler LoadMoreEvent;
        public bool isLoading { get; set; }

        private LinearLayoutManager LayoutManager;

        public XamarinRecyclerViewOnScrollListener(LinearLayoutManager layoutManager)
        {
            LayoutManager = layoutManager;
        }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            var visibleItemCount = recyclerView.ChildCount;
            var totalItemCount = recyclerView.GetAdapter().ItemCount;
            var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();

            if ((visibleItemCount + pastVisiblesItems) >= totalItemCount && !isLoading)
            {
                isLoading = true;
                LoadMoreEvent(this, null);

            }
        }
    }
}