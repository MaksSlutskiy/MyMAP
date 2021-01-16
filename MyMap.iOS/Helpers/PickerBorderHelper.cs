using System;
using MyMap.CustomViews;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace MyMap.iOS.Helpers
{
    public class PickerBorderHelper<T>
    {
        UITextField _control;
        T _picker;

        public PickerBorderHelper(UITextField control, T picker)
        {
            _control = control;
            _picker = picker;

            _control.BorderStyle = UITextBorderStyle.RoundedRect;
            _control.ClipsToBounds = true;
            _control.Layer.MasksToBounds = true;
            UpdateBorder();
        }

        public void UpdateBorder()
        {
            if (_picker is ExtendedPicker)
            {
                _control.Layer.CornerRadius = (nfloat)(_picker as ExtendedPicker).BorderRadius;
                _control.Layer.BorderColor = (_picker as ExtendedPicker).BorderColor.ToCGColor();
                _control.Layer.BorderWidth = (nfloat)(_picker as ExtendedPicker).BorderWidth;
            }
            //else
            //{
            //    _control.Layer.CornerRadius = (nfloat)(_picker as ExtendedDatePicker).BorderRadius;
            //    _control.Layer.BorderColor = (_picker as ExtendedDatePicker).BorderColor.ToCGColor();
            //    _control.Layer.BorderWidth = (nfloat)(_picker as ExtendedDatePicker).BorderWidth;
            //}

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
