using System;
using MyMap.CustomViews;
using MyMap.iOS.CustomViews;
using MyMap.iOS.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(ExtendedPickerRenderer))]
namespace MyMap.iOS.CustomViews
{
    public class ExtendedPickerRenderer : PickerRenderer
    {
        PickerBorderHelper<ExtendedPicker> _helper;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                _helper = new PickerBorderHelper<ExtendedPicker>(Control, (Element as ExtendedPicker));
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _helper.UpdateBorderByPropertyName(e.PropertyName);
        }
    }
}
