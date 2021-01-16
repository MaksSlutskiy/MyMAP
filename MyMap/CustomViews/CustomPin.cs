using System;
using Xamarin.Forms.Maps;

namespace MyMap.CustomViews
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        //public ObservableCollection<Image> Images { get; set; }
        //public Category Category { get; set; }
    }
}
