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

namespace Hw3_Akin.Adapters.GridviewAdapters
{
    class goggleGridviewAdapter : BaseAdapter
    {
            Context context;
            public goggleGridviewAdapter(Context c)
            {
                context = c;
            }
            public override int Count
            {
                get
                {
                    return thumbIds.Length;
                }
            }
            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }
            public override long GetItemId(int position)
            {
                 return long.Parse(thumbIds[position].ToString());
        }
            // create a new ImageView for each item referenced by the Adapter   
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ImageView imageView;
                if (convertView == null)
                { // if it's not recycled, initialize some attributes   
                    imageView = new ImageView(context);
                    imageView.LayoutParameters = new GridView.LayoutParams(250, 250);
                    imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                    imageView.SetPadding(8, 8, 8, 8);
                }
                else
                {
                    imageView = (ImageView)convertView;
                }
                imageView.SetImageResource(thumbIds[position]);
                return imageView;
            }
            // references to our images   
            int[] thumbIds = {
        Resource.Drawable.glass3,
        Resource.Drawable.glass4,
        Resource.Drawable.glass5,
        Resource.Drawable.glass6,
        Resource.Drawable.glass7,
        Resource.Drawable.glass8,
        Resource.Drawable.sunglass,
        Resource.Drawable.sunglass_2,
    };
        }
    }