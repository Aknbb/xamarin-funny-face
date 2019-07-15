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
    class goggleView
    {
        public static View getGoggleView(View givenView, Android.Content.Res.Resources Resources)
        {
            var gridView1 = givenView.FindViewById<GridView>(Resource.Id.gridView);
            gridView1.Adapter = new Adapters.GridviewAdapters.goggleGridviewAdapter(givenView.Context);
            gridView1.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                int id = (int)args.Id;
                ViewCreaters.processView.setGoogles(BitmapFactory.DecodeResource(Resources, id));
                Toast.MakeText(givenView.Context, "Goggles has been selected!", ToastLength.Short).Show();
            };

            return givenView;
        }
    }
}