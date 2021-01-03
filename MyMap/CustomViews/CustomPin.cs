using System;
using Xamarin.Forms.Maps;

namespace MyMap.CustomViews
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
    }
}
