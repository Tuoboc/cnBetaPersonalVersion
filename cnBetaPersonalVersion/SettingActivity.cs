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
        Switch AutoUpdate;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Setting);
            // Create your application here
            
            prefs = Application.Context.GetSharedPreferences("CnBeta.Tuoboc", FileCreationMode.Private);
            editor = prefs.Edit();           

            PicMode = FindViewById<Switch>(Resource.Id.PicMode);
            CheckUpdate = FindViewById<Button>(Resource.Id.CheckUpdate);
            AutoUpdate = FindViewById<Switch>(Resource.Id.AutoUpdate);
            PicMode.CheckedChange += PicMode_CheckedChange;
            CheckUpdate.Click += CheckUpdate_Click;
            AutoUpdate.CheckedChange += AutoUpdate_CheckedChange;
            InitSettingStatus();
        }

        private void AutoUpdate_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            editor.PutBoolean("AutoUpdate", e.IsChecked);
            editor.Apply();

        }

        private void CheckUpdate_Click(object sender, EventArgs e)
        {
            //http://tuoboc.tk:8800/api/cnBetaDownLoad
            Toast.MakeText(this, "正在检查更新", ToastLength.Short).Show();
        }

        private void PicMode_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            editor.PutBoolean("PicMode", e.IsChecked);
            editor.Apply();
            
        }

        private void InitSettingStatus()
        {
           PicMode.Checked= prefs.GetBoolean("PicMode", true);
            AutoUpdate.Checked= prefs.GetBoolean("AutoUpdate", false);
        }
        
    }
}