using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyMap.Model
{
    public class MapObjectPin 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        //public ObservableCollection<Image> Images { get; set; }
        //public Category Category { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool IsVisible { get; set; }
    }
}
