using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using MyMap.CustomViews;
using MyMap.Droid.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(ExtendedImageButton), typeof(ExtendedImageButtonRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace MyMap.Droid.CustomViews
{
    [Obsolete]
    public class ExtendedImageButtonRenderer : ImageButtonRenderer
    {
        public ExtendedImageButtonRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ImageButton> e)
        {
            base.OnElementChanged(e);

            SetTint();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedImageButton.TintColorProperty.PropertyName || e.PropertyName == ExtendedImageButton.SourceProperty.PropertyName)
                SetTint();
        }

        void SetTint()
        {
            if (this == null || Element == null)
                return;

            if (((ExtendedImageButton)Element).TintColor != Xamarin.Forms.Color.Transparent)
            {
                if (this.ColorFilter != null)
                    this.ClearColorFilter();
            }

            //Apply tint color
            var colorFilter = new PorterDuffColorFilter(((ExtendedImageButton)Element).TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            this.SetColorFilter(colorFilter);
        }
    }
}
