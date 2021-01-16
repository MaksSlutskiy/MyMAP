using System;
using Android.Content;
using MyMap.CustomViews;
using MyMap.Droid.CustomViews;
using MyMap.Droid.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRender))]
namespace MyMap.Droid.CustomViews
{
    public class ExtendedPickerRender : PickerRenderer
    {
        BorderHelper<ExtendedPicker> _helper;
        public ExtendedPickerRender(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                _helper = new BorderHelper<ExtendedPicker>(Control, (Element as ExtendedPicker));
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _helper.UpdateBorderByPropertyName(e.PropertyName);

        }

    }
}
