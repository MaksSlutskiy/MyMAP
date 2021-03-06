using System;
using System.Collections.Generic;
using MyMap.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyMap.CustomViews
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
        public CustomMap()
        {
            CustomPins = new List<CustomPin>();
        }


        public static readonly BindableProperty IsMakeSnapshotProperty =
           BindableProperty.Create(nameof(IsMakeSnapshot), typeof(bool), typeof(CustomMap), false);
        public bool IsMakeSnapshot
        {
            get { return (bool)GetValue(IsMakeSnapshotProperty); }
            set { SetValue(IsMakeSnapshotProperty, value); }
        }
        public static readonly BindableProperty IsUpdateProperty =
           BindableProperty.Create(nameof(IsMakeSnapshot), typeof(bool), typeof(CustomMap), false);
        public bool IsUpdate
        {
            get { return (bool)GetValue(IsUpdateProperty); }
            set { SetValue(IsUpdateProperty, value); }
        }

        public static readonly BindableProperty CheckThemeProperty =
          BindableProperty.Create(nameof(CheckTheme), typeof(bool), typeof(CustomMap), false);
        public bool CheckTheme
        {
            get { return (bool)GetValue(CheckThemeProperty); }
            set { SetValue(CheckThemeProperty, value); }
        }
    }
}
