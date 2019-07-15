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
using Android.Webkit;

namespace Hw3_Akin
{
    public class CustomWebChromeClient : WebChromeClient
    {
        public CustomWebChromeClient() { }
        public CustomWebChromeClient(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }
        public override bool OnJsAlert(WebView view, string url, string message, JsResult result)
        {
            string userName = message.Split(new string[] { "~_*_~" }, StringSplitOptions.None)[0];
            string password = message.Split(new string[] { "~_*_~" }, StringSplitOptions.None)[1];
            Dictionary<string, string> data = new UserPasswordData().getUserPasswordDatas();
            foreach(KeyValuePair<string,string> value in data)
            {
                if(value.Key == userName && value.Value == password)
                {
                    Toast.MakeText(Application.Context, "Welcome!", ToastLength.Short).Show();
                    result.Confirm();
                    Intent myIntent;
                    myIntent = new Intent(Application.Context, typeof(MainActivity));
                    myIntent.SetFlags(ActivityFlags.NewTask);
                    Application.Context.StartActivity(myIntent);
                    return true;
                }
            }

            Toast.MakeText(Application.Context, "Login failed.", ToastLength.Short).Show();
            result.Confirm();
            return true;
        }

    }
}