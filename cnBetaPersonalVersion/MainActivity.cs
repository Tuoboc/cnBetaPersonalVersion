using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Content;

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
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mArticleAdapter = new ArticleAdapter(mArticleList);
            mArticleAdapter.ItemClick += MArticleAdapter_ItemClick;
            mRecyclerView.SetAdapter(mArticleAdapter);
        }

        private void MArticleAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("title", mArticleList[e].Title);
            intent.PutExtra("url",mArticleList[e].URL);
            StartActivity(intent);
            //Toast.MakeText(this, mArticleList[e].Title, ToastLength.Short).Show();
        }
    }
}

