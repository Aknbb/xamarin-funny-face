using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Lang;

namespace Hw3_Akin
{
    [Activity(Label = "Login", Theme = "@style/AppTheme",WindowSoftInputMode =SoftInput.AdjustPan,MainLauncher = true)]
    public class LoginActivity : Activity
    {
        WebView loginWebView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
            loginWebView = (WebView)FindViewById(Resource.Id.webView1);
            WebSettings webSettings = loginWebView.Settings;
            webSettings.JavaScriptEnabled = true;
            loginWebView.Focusable = true;
            loginWebView.SetWebChromeClient(new CustomWebChromeClient());
            loginWebView.LoadUrl("file:///android_asset/index.html");
        }
    }
}