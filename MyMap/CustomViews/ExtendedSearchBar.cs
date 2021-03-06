using System;
using Xamarin.Forms;

namespace MyMap.CustomViews
{
    public class ExtendedSearchBar : SearchBar
    {
        public static readonly BindableProperty TintColorProperty =
            BindableProperty.Create("SelectedBackgroundColor",
                                    typeof(Color),
                                    typeof(ExtendedSearchBar),
                                    Color.Default);

        public Color TintColor
        {
            get { return (Color)GetValue(TintColorProperty); }
            set { SetValue(TintColorProperty, value); }
        }
    }
}
