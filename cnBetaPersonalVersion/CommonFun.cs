using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cnBetaPersonalVersion
{
   public static class CommonFun
    {
        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Stream GetFileStream(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = response.GetResponseStream();
                return responseString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}