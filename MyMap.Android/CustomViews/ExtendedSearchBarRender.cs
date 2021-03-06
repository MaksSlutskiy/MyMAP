using System;
using Android.Graphics;
using Android.Widget;
using MyMap.CustomViews;
using MyMap.Droid.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using G = Android.Graphics;

[assembly: ExportRenderer(typeof(ExtendedSearchBar), typeof(ExtendedSearchBarRender))]
namespace MyMap.Droid.CustomViews
{
    [Obsolete]
    public class ExtendedSearchBarRender : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var color = global::Xamarin.Forms.Color.White;
            var searchView = Control as SearchView;

            //UpdateSearchButtonColor();
            //UpdateCancelButtonColor();
            //UpdateSearchPlateColor();
            //UpdateTextColor(searchView);
        }

        private void UpdateTextColor(SearchView searchView)
        {
            var textViewId = searchView.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
            EditText textView = (searchView.FindViewById(textViewId) as EditText);
            textView.SetTextColor(G.Color.Rgb(255, 255, 255));
            textView.SetHintTextColor(G.Color.Rgb(125, 206, 160));
            textView.SetHighlightColor(G.Color.Rgb(125, 206, 160));
        }

        private void UpdateSearchPlateColor()
        {
            var plateId = Control.Resources.GetIdentifier("android:id/search_plate", null, null);
            if (plateId != 0)
            {
                var plate = Control.FindViewById(plateId);
                if (plate != null)
                    plate.Background.SetColorFilter(G.Color.Rgb(255, 255, 255), PorterDuff.Mode.SrcAtop);
            }
        }

        private void UpdateSearchButtonColor()
        {
            int searchIconId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
            if (searchIconId != 0)
            {
                var image = FindViewById<ImageView>(searchIconId);
                if (image != null && image.Drawable != null)
                    image.Drawable.SetColorFilter(G.Color.Rgb(255, 255, 255), PorterDuff.Mode.SrcIn);
            }
        }

        private void UpdateCancelButtonColor()
        {
            int searchViewCloseButtonId = Control.Resources.GetIdentifier("android:id/search_close_btn", null, null);
            if (searchViewCloseButtonId != 0)
            {
                var image = FindViewById<ImageView>(searchViewCloseButtonId);
                if (image != null && image.Drawable != null)
                {
                    if (Element.CancelButtonColor == Color.Default)
                        image.Drawable.SetColorFilter(G.Color.Rgb(255, 255, 255), PorterDuff.Mode.SrcIn);
                    else
                        image.Drawable.ClearColorFilter();
                }
            }
        }
    }
}
