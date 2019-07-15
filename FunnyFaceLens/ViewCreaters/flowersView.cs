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
    class flowersView
    {
        public static View getFlowersView(View givenView, Android.Content.Res.Resources Resources)
        {
            var gridView2 = givenView.FindViewById<GridView>(Resource.Id.gridView2);
            gridView2.Adapter = new Adapters.GridviewAdapters.flowerGridviewAdapter(givenView.Context);
            gridView2.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                int id = (int)args.Id;
                ViewCreaters.processView.setFlowers(BitmapFactory.DecodeResource(Resources, id));
                Toast.MakeText(givenView.Context, "Flower has been selected!", ToastLength.Short).Show();
            };
            return givenView;
        }
    }
}