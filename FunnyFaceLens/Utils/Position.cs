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
    class Position
    {
        public Position(string memberId,int x,int y)
        {
            positionX = x;
            positionY = y;
            id = memberId;
        }
        public int positionX { get; set; }
        public string id { get; set; }
        public int positionY { get; set; }
    }
}