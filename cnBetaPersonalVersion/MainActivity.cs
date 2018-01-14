using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Content;
using Android.Support.V4.Widget;
using System.ComponentModel;

namespace cnBetaPersonalVersion
{
    [Activity(Label = "cnBeta个人版")]
    public class MainActivity : Activity
    {
        RecyclerView mRecyclerView;
        RecyclerView.LayoutManager mLayoutManager;
        ArticleAdapter mArticleAdapter;
        ArticleList mArticleList;
        SwipeRefreshLayout mSwipeRefreshLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mArticleList = new ArticleList();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mSwipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeLayout);
            mSwipeRefreshLayout.SetColorScheme(Android.Resource.Color.HoloBlueDark);
            mSwipeRefreshLayout.Refresh += MSwipeRefreshLayout_Refresh;


            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mArticleAdapter = new ArticleAdapter(mArticleList);
            mArticleAdapter.ItemClick += MArticleAdapter_ItemClick;
            mRecyclerView.SetAdapter(mArticleAdapter);
        }

        private void MSwipeRefreshLayout_Refresh(object sender, System.EventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                mSwipeRefreshLayout.Refreshing = false;

            });
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            mArticleList.GetArticle();
            //mArticleAdapter.NotifyDataSetChanged();
            mRecyclerView.Invalidate();
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

