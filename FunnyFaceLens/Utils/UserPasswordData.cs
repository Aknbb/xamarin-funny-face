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

namespace Hw3_Akin
{
    class UserPasswordData
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();
        public UserPasswordData()
        {
            initializeUserPasswordDatas();
        }
        public Dictionary<string, string> getUserPasswordDatas()
        {
            return data;
        }
        private void initializeUserPasswordDatas()
        {
            data.Add("gaudi","1852");
            data.Add("admin","admin");
            data.Add("akin","123");
            data.Add("muro","123");
        }
    }
}