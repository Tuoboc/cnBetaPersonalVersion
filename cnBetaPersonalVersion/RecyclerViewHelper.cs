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

namespace cnBetaPersonalVersion
{
    public class ArticleHolder:RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Title { get; private set; }
        public TextView Summary { get; private set; }
        public TextView Other { get; private set; }

        public ArticleHolder(View itemview,Action<int>listener):base(itemview)
        {
            Image = itemview.FindViewById<ImageView>( Resource.Id.imageView);
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
        public override int ItemCount => throw new NotImplementedException();

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ArticleHolder ah = holder as ArticleHolder;
            //ah.Image.setim.SetImageURI(new Android.Net.Uri.Builder(articleList[position].ImageURL));
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ArticleCardView, parent, false);
            ArticleHolder ah = new ArticleHolder(itemView, OnClick);
            return ah;
        }
    }
}