using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Content;
using System;
using Android.Views;

namespace cnBetaPersonalVersion
{
    [Activity(Label = "cnBeta个人版")]
    public class MainActivity : Activity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        ArticleAdapter mArticleAdapter;
        ArticleList mArticleList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mArticleList = new ArticleList();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            mRecyclerView.HasFixedSize = true;
            mLayoutManager = new LinearLayoutManager(this);
            var onScrollListener = new XamarinRecyclerViewOnScrollListener(mLayoutManager as LinearLayoutManager);
            onScrollListener.LoadMoreEvent += (object sender, EventArgs e) =>
            {
                mArticleList.GetMoreArticle();
            };
            mRecyclerView.AddOnScrollListener(onScrollListener);

            mRecyclerView.SetLayoutManager(mLayoutManager);
            mArticleAdapter = new ArticleAdapter(mArticleList);
            mArticleAdapter.ItemClick += MArticleAdapter_ItemClick;
            mRecyclerView.SetAdapter(mArticleAdapter);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.mainMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuItem1:
                    {

                        Intent intent = new Intent(this, typeof(SettingActivity));
                       
                        StartActivity(intent);
                        return true;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }

        private void MArticleAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("title", mArticleList[e].Title);
            intent.PutExtra("url", mArticleList[e].URL);
            StartActivity(intent);
            //Toast.MakeText(this, mArticleList[e].Title, ToastLength.Short).Show();
        }
    }
}

