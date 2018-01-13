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

namespace cnBetaPersonalVersion
{
   public static class GlobalVariables
    {
        public static string _csrf { get; set; }

        public static int _page { get; set; }
    }
}