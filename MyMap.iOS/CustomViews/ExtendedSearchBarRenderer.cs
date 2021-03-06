using System;
using System.ComponentModel;
using MyMap.CustomViews;
using MyMap.iOS.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedSearchBar), typeof(ExtendedSearchBarRenderer))]
namespace MyMap.iOS.CustomViews
{
    public class ExtendedSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.ShowsCancelButton = false;
                Control.SearchBarStyle = UIKit.UISearchBarStyle.Minimal;
            }
        }

    }
}
