using System;
using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using MyMap.CustomViews;
using MyMap.Droid.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(ExtendedImage), typeof(ExtendedImageRender))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace MyMap.Droid.CustomViews
{
    [Obsolete]
    public class ExtendedImageRender : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            SetTint();
            //SetBackground(Control);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedImage.TintColorProperty.PropertyName || e.PropertyName == ExtendedImage.SourceProperty.PropertyName)
                SetTint();
        }

        void SetTint()
        {
            if (Control == null || Element == null)
                return;

            if (((ExtendedImage)Element).TintColor.Equals(Xamarin.Forms.Color.Transparent))
            {
                //Turn off tinting

                if (Control.ColorFilter != null)
                    Control.ClearColorFilter();

                return;
            }

            //Apply tint color
            var colorFilter = new PorterDuffColorFilter(((ExtendedImage)Element).TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            try
            {
                Control.SetColorFilter(colorFilter);
            }
            catch { }
        }

        private void SetBackground(Android.Views.View rootLayout)
        {
            // Get the background color from Forms element
            var backgroundColor = Element.BackgroundColor.ToAndroid();

            // Create statelist to handle ripple effect
            var enabledBackground = new GradientDrawable(GradientDrawable.Orientation.LeftRight, new int[] { backgroundColor, backgroundColor });
            var stateList = new StateListDrawable();
            var rippleItem = new RippleDrawable(ColorStateList.ValueOf(Android.Graphics.Color.White),
                                                enabledBackground,
                                                null);
            stateList.AddState(new[] { Android.Resource.Attribute.StateEnabled }, rippleItem);

            // Assign background
            rootLayout.Background = stateList;
        }
    }
}
