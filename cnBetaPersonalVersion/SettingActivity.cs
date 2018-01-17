using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace cnBetaPersonalVersion
{
    [Activity(Label = "cnBeta个人版", NoHistory = true)]
    public class SettingActivity : Activity
    {
        Button CheckUpdate;
        Switch PicMode;
        Switch AutoUpdate;
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;

        private VersionEntity version;
        private string versionName;
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
            Context context = this.ApplicationContext;
            versionName = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            Toast.MakeText(this, "正在检查版本", ToastLength.Short).Show();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (version.version > Convert.ToDouble(versionName))
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("版本更新");
                alert.SetMessage("发现新版本" + version.version + "是否更新");
                alert.SetPositiveButton("下载", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "开始下载", ToastLength.Short).Show();
                    using (WebClient client = new WebClient())
                    {
                        client.UploadDataCompleted += Client_UploadDataCompleted;
                        client.UploadDataAsync(new Uri("http://tuoboc.tk:8800/api/cnBetaDownLoad" + "?filename=" + version.downLoadUrl), "POST", new byte[] { });
                    }
                });
                alert.SetNegativeButton("取消", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "取消更新", ToastLength.Short).Show();
                });
                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {
                Toast.MakeText(this, "已经是最新版本", ToastLength.Short).Show();
            }

        }

        private void Client_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(sdCardPath, version.downLoadUrl);
            if (!System.IO.File.Exists(filePath))
            {
                File.WriteAllBytes(filePath, e.Result);
            }
            Toast.MakeText(this, "下载完成,已保存到"+filePath, ToastLength.Short).Show();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = "http://tuoboc.tk:8800/api/cnBetaDownLoad";
            var responseString = CommonFun.GetFileStream(url);

            StreamReader reader = new StreamReader(responseString);
            string text = reader.ReadToEnd();
            version = JsonConvert.DeserializeObject<VersionEntity>(text);
        }

        private void PicMode_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            editor.PutBoolean("PicMode", e.IsChecked);
            editor.Apply();

        }

        private void InitSettingStatus()
        {
            PicMode.Checked = prefs.GetBoolean("PicMode", true);
            AutoUpdate.Checked = prefs.GetBoolean("AutoUpdate", false);
        }


    }

    public class VersionEntity
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string downLoadUrl { get; set; }
        public double version { get; set; }
    }
}