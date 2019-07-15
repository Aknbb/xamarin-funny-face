using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Hw3_Akin.ViewCreaters
{
    class smokeView
    {
        public static View getSmokeView(View givenView, Android.Content.Res.Resources Resources)
        {
            var gridView3 = givenView.FindViewById<GridView>(Resource.Id.gridView3);
            gridView3.Adapter = new Adapters.GridviewAdapters.smokeGridviewAdapter(givenView.Context);
            gridView3.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                int id = (int)args.Id;
                ViewCreaters.processView.setSmokes(BitmapFactory.DecodeResource(Resources, id));
                Toast.MakeText(givenView.Context, "Smoke has been selected!", ToastLength.Short).Show();
            };
            return givenView;
        }
    }
}