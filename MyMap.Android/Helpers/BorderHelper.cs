using System;
using Android.Graphics.Drawables;
using Android.Views;
using MyMap.CustomViews;
using Xamarin.Forms.Platform.Android;

namespace MyMap.Droid.Helpers
{
    public class BorderHelper<T>
    {
        View _control;
        T _element;

        public BorderHelper(View control, T element)
        {
            _control = control;
            _element = element;
            UpdateBorder();
        }

        public void UpdateBorder()
        {
            if (/*_element is ExtendedDatePicker ||*/ _element is ExtendedPicker)
            {
                //if (_element as ExtendedDatePicker != null)
                //{
                //    GradientDrawable gd = new GradientDrawable();
                //    gd.SetColor((_element as ExtendedDatePicker).BackgroundColor.ToAndroid());
                //    gd.SetStroke((int)(_element as ExtendedDatePicker).BorderWidth * 2, (_element as ExtendedDatePicker).BorderColor.ToAndroid());
                //    gd.SetCornerRadius((float)(_element as ExtendedDatePicker).BorderRadius);
                //    _control.SetPadding(50, 0, 50, 0);
                //    _control.SetBackground(gd);
                //}
                if (_element as ExtendedPicker != null)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor((_element as ExtendedPicker).BackgroundColor.ToAndroid());
                    gd.SetStroke((int)(_element as ExtendedPicker).BorderWidth * 2, (_element as ExtendedPicker).BorderColor.ToAndroid());
                    gd.SetCornerRadius((float)(_element as ExtendedPicker).BorderRadius);
                    _control.SetPadding(50, 0, 50, 0);
                    _control.SetBackground(gd);
                }
            }
        }

        public void UpdateBorderByPropertyName(string propertyName)
        {
            switch (propertyName)
            {
                case "BorderColor":
                case "BorderRadius":
                case "BorderWidth":
                    UpdateBorder();
                    break;
                default:
                    return;
            }
        }
    }
}
