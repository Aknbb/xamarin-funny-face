using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Hw3_Akin
{
    public class CustomPagerAdapter : FragmentPagerAdapter
    {
        const int PAGE_COUNT = 4;
        private string[] tabTitles = { "Process", "Flowers", "Smokes", "Glasses" };
        readonly Context context;

        public CustomPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public CustomPagerAdapter(Context context, FragmentManager fm) : base(fm)
        {
            this.context = context;
        }

        public override int Count
        {
            get { return PAGE_COUNT; }
        }

        public override Fragment GetItem(int position)
        {
            return PageFragment.newInstance(position + 1);
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return CharSequence.ArrayFromStringArray(tabTitles)[position];
        }

        public View GetTabView(int position)
        {
            var tv = (TextView)LayoutInflater.From(context).Inflate(Resource.Layout.custom_tab, null);
            tv.Text = tabTitles[position];
            tv.TextSize = 11;
            return tv;
        }
    }
}