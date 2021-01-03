using System;
using Xamarin.Forms;

namespace MyMap.CustomViews
{
    public class ExtendedImage : Image
    {
        public static readonly BindableProperty TintColorProperty =
           BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(ExtendedImage), Color.Transparent);
        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }
    }
}
