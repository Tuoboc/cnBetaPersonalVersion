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
    public class SettingActivity : Activity
    {
        Button CheckUpdate;
        Switch PicMode;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Setting);
            // Create your application here
            PicMode = FindViewById<Switch>(Resource.Id.PicMode);
            CheckUpdate = FindViewById<Button>(Resource.Id.CheckUpdate);

            PicMode.CheckedChange += PicMode_CheckedChange;
            CheckUpdate.Click += CheckUpdate_Click;
        }

        private void CheckUpdate_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "正在检查更新", ToastLength.Short).Show();
        }

        private void PicMode_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Toast.MakeText(this, e.IsChecked.ToString(), ToastLength.Short).Show();
        }
        
    }
}